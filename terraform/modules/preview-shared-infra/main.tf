locals {
  common_tags = {
    Environment = var.environment
    ManagedBy   = "terraform"
  }

  ecs_subnet_ids = [
    for subnet in aws_subnet.private : subnet.id
    if contains(var.ecs_availability_zones, subnet.availability_zone)
  ]

  nat_instances_by_az = { for inst in aws_instance.nat : inst.availability_zone => inst.primary_network_interface_id }
}

# VPC
resource "aws_vpc" "main" {
  cidr_block           = var.vpc_cidr_block
  enable_dns_support   = true
  enable_dns_hostnames = true

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-vpc"
    }
  )
}

# Public Subnets
resource "aws_subnet" "public" {
  count = min(length(var.public_subnet_cidr_blocks), length(var.availability_zones))

  vpc_id                  = aws_vpc.main.id
  cidr_block              = var.public_subnet_cidr_blocks[count.index]
  availability_zone       = var.availability_zones[count.index]
  map_public_ip_on_launch = true

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-public-subnet-${count.index + 1}"
      AZ   = var.availability_zones[count.index]
    }
  )
}

# Private Subnets
resource "aws_subnet" "private" {
  count = min(length(var.private_subnet_cidr_blocks), length(var.availability_zones))

  vpc_id            = aws_vpc.main.id
  cidr_block        = var.private_subnet_cidr_blocks[count.index]
  availability_zone = var.availability_zones[count.index]

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-private-subnet-${count.index + 1}"
      AZ   = var.availability_zones[count.index]
    }
  )
}

# Internet Gateway
resource "aws_internet_gateway" "main" {
  vpc_id = aws_vpc.main.id

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-igw"
    }
  )
}

# NAT Instance Security Group
resource "aws_security_group" "nat" {
  name_prefix = "${var.shared_project_name}-nat-sg-"
  description = "Security group for NAT instance"
  vpc_id      = aws_vpc.main.id

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = var.private_subnet_cidr_blocks
    description = "Allow HTTP from Private Subnets"
  }

  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = var.private_subnet_cidr_blocks
    description = "Allow HTTPS from Private Subnets"
  }

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-nat-sg"
    }
  )

  lifecycle {
    create_before_destroy = true
  }
}

# IAM Role for SSM Session Manager
resource "aws_iam_role" "ssm_role" {
  name_prefix = "${var.shared_project_name}-nat-ssm-role-"
  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = "ec2.amazonaws.com"
        }
      },
    ]
  })

  tags = local.common_tags
}

resource "aws_iam_role_policy_attachment" "ssm_policy" {
  role       = aws_iam_role.ssm_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonSSMManagedInstanceCore"
}

resource "aws_iam_instance_profile" "nat_instance_profile" {
  name_prefix = "${var.shared_project_name}-nat-ssm-profile-"
  role        = aws_iam_role.ssm_role.name

  lifecycle {
    create_before_destroy = true
  }
}

data "aws_ssm_parameter" "latest_amazon_linux_2" {
  name = "/aws/service/ami-amazon-linux-latest/amzn2-ami-hvm-x86_64-gp2"
}

# NAT Instance in each public subnet
resource "aws_instance" "nat" {
  count = length(aws_subnet.public)

  ami                         = data.aws_ssm_parameter.latest_amazon_linux_2.value
  instance_type               = lookup(var.az_instance_type_map, aws_subnet.public[count.index].availability_zone)
  subnet_id                   = aws_subnet.public[count.index].id
  vpc_security_group_ids      = [aws_security_group.nat.id]
  iam_instance_profile        = aws_iam_instance_profile.nat_instance_profile.name
  source_dest_check           = false
  associate_public_ip_address = true

  root_block_device {
    volume_size           = 8
    volume_type           = "gp3"
    encrypted             = true
    delete_on_termination = true
  }

  metadata_options {
    http_endpoint = "enabled"
    http_tokens   = "required"
  }
  user_data = templatefile("${path.module}/nat_user_data.sh.tpl", {
    private_subnet_cidrs = join(",", var.private_subnet_cidr_blocks)
  })

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-nat-instance-${count.index + 1}"
      AZ   = aws_subnet.public[count.index].availability_zone
    }
  )

  lifecycle {
    ignore_changes        = [ami]
    create_before_destroy = true
  }
}

# resource "aws_eip" "nat" {
#   for_each = { for i, inst in aws_instance.nat : i => inst }

#   instance = each.value.id
#   domain   = "vpc"

#   tags = merge(
#     local.common_tags,
#     {
#       Name = "${var.shared_project_name}-nat-eip-${each.key}"
#       AZ   = each.value.availability_zone
#     }
#   )
# }

# Create one Private Route Table per AZ.
resource "aws_route_table" "private" {
  for_each = toset([
    for subnet in aws_subnet.private : subnet.availability_zone
  ])

  vpc_id = aws_vpc.main.id

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-private-rt-${each.key}"
      AZ   = each.key
    }
  )
}

# Route from each private route table to the NAT instance in the same AZ.
resource "aws_route" "private_nat" {
  for_each = aws_route_table.private

  route_table_id         = each.value.id
  destination_cidr_block = "0.0.0.0/0"
  # Use the network interface ID for routing to NAT instances
  network_interface_id = local.nat_instances_by_az[each.key]

  # Ensure the route is created only after the NAT instance it depends on is fully up.
  depends_on = [aws_instance.nat]
}

# Public Route Table
resource "aws_route_table" "public" {
  vpc_id = aws_vpc.main.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.main.id
  }

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-public-rt"
    }
  )
}


# Route Table Associations
resource "aws_route_table_association" "public" {
  count = length(aws_subnet.public)

  subnet_id      = aws_subnet.public[count.index].id
  route_table_id = aws_route_table.public.id
}

resource "aws_route_table_association" "private" {
  count = length(aws_subnet.private)

  subnet_id      = aws_subnet.private[count.index].id
  route_table_id = aws_route_table.private[aws_subnet.private[count.index].availability_zone].id
}

# ALB Security Group
resource "aws_security_group" "alb" {
  name        = "${var.shared_project_name}-alb-sg"
  description = "Security group for the shared ALB"
  vpc_id      = aws_vpc.main.id

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    description = "Allow HTTP from anywhere"
  }

  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    description = "Allow HTTPS from anywhere"
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
    description = "Allow all outbound traffic"
  }

  tags = merge(local.common_tags, { Name = "${var.shared_project_name}-alb-sg" })
}

# Application Load Balancer
resource "aws_lb" "main" {
  name               = "${var.shared_project_name}-alb"
  internal           = false
  load_balancer_type = "application"
  security_groups    = [aws_security_group.alb.id]
  subnets            = [for s in aws_subnet.public : s.id]

  enable_deletion_protection = false
  idle_timeout               = 60

  tags = merge(local.common_tags, { Name = "${var.shared_project_name}-alb" })
}

# ALB Listener (HTTPS)
resource "aws_lb_listener" "https" {
  load_balancer_arn = aws_lb.main.arn
  port              = 443
  protocol          = "HTTPS"
  ssl_policy        = "ELBSecurityPolicy-TLS13-1-2-2021-06"
  certificate_arn   = var.acm_certificate_arn

  default_action {
    type = "fixed-response"
    fixed_response {
      content_type = "text/plain"
      message_body = "Cannot route request"
      status_code  = "404"
    }
  }
}

# ALB Listener (HTTP to HTTPS Redirect)
resource "aws_lb_listener" "http" {
  load_balancer_arn = aws_lb.main.arn
  port              = 80
  protocol          = "HTTP"

  default_action {
    type = "redirect"
    redirect {
      port        = "443"
      protocol    = "HTTPS"
      status_code = "HTTP_301"
    }
  }
}

# ECS Cluster
resource "aws_ecs_cluster" "main" {
  name = "${var.shared_project_name}-cluster"

  setting {
    name  = "containerInsights"
    value = "disabled"
  }

  tags = merge(local.common_tags, { Name = "${var.shared_project_name}-cluster" })
}

# IAM Role for ECS EC2 Instances
resource "aws_iam_role" "ecs_instance_role" {
  name_prefix = "${var.shared_project_name}-ecs-instance-role-"
  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [{
      Action    = "sts:AssumeRole"
      Effect    = "Allow"
      Principal = { Service = "ec2.amazonaws.com" }
    }]
  })
  tags = merge(local.common_tags, { Name = "${var.shared_project_name}-ecs-instance-role" })
}

resource "aws_iam_role_policy_attachment" "ecs_instance_role_policy" {
  role       = aws_iam_role.ecs_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonEC2ContainerServiceforEC2Role"
}

resource "aws_iam_role_policy_attachment" "ecs_instance_role_ssm_policy" {
  role       = aws_iam_role.ecs_instance_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonSSMManagedInstanceCore"
}

resource "aws_iam_instance_profile" "ecs_instance_profile" {
  name_prefix = "${var.shared_project_name}-ecs-instance-profile-"
  role        = aws_iam_role.ecs_instance_role.name
}

# Security Group for ECS EC2 Instances
resource "aws_security_group" "ecs_instance" {
  name        = "${var.shared_project_name}-ecs-instance-sg"
  description = "Security group for ECS container instances"
  vpc_id      = aws_vpc.main.id

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }


  tags = merge(local.common_tags, { Name = "${var.shared_project_name}-ecs-instance-sg" })
}

data "aws_ssm_parameter" "latest_ecs_ami" {
  name = "/aws/service/ecs/optimized-ami/amazon-linux-2/recommended/image_id"
}

# Launch Template for ECS EC2 Instances
resource "aws_launch_template" "ecs" {
  name_prefix   = "${var.shared_project_name}-ecs-lt-"
  image_id      = data.aws_ssm_parameter.latest_ecs_ami.value
  instance_type = var.ecs_instance_type

  iam_instance_profile {
    name = aws_iam_instance_profile.ecs_instance_profile.name
  }

  network_interfaces {
    associate_public_ip_address = false
    security_groups             = [aws_security_group.ecs_instance.id]
    delete_on_termination       = true
  }

  block_device_mappings {
    device_name = "/dev/xvda"
    ebs {
      volume_size           = 30
      volume_type           = "gp3"
      encrypted             = true
      delete_on_termination = true
    }
  }

  user_data = base64encode(<<-EOF
    #!/bin/bash
    echo ECS_CLUSTER=${aws_ecs_cluster.main.name} >> /etc/ecs/ecs.config
    echo ECS_ENABLE_TASK_IAM_ROLE=true >> /etc/ecs/ecs.config
    echo ECS_ENABLE_CONTAINER_METADATA=true >> /etc/ecs/ecs.config
    echo ECS_AVAILABLE_LOGGING_DRIVERS='["json-file","awslogs"]' >> /etc/ecs/ecs.config
    
    yum update -y
    systemctl restart ecs
    EOF
  )

  metadata_options {
    http_endpoint = "enabled"
    http_tokens   = "required" # Enable IMDSv2
  }

  tag_specifications {
    resource_type = "instance"
    tags = merge(
      local.common_tags,
      {
        Name          = "${var.shared_project_name}-ecs-instance"
        AWSECSManaged = "true"
      }
    )
  }

  lifecycle {
    create_before_destroy = true
  }
}

resource "aws_autoscaling_group" "ecs" {
  name                = "${var.shared_project_name}-ecs-asg"
  desired_capacity    = var.ecs_desired_capacity_total
  min_size            = var.ecs_min_size_total
  max_size            = var.ecs_max_size_total
  vpc_zone_identifier = local.ecs_subnet_ids # Deploy across all selected private subnets

  launch_template {
    id      = aws_launch_template.ecs.id
    version = "$Latest"
  }

  lifecycle {
    create_before_destroy = true
  }
}

# Database Subnet Group
resource "aws_db_subnet_group" "main" {
  name       = "${var.shared_project_name}-db-subnet-group"
  subnet_ids = [for s in aws_subnet.private : s.id]

  tags = merge(
    local.common_tags,
    {
      Name = "${var.shared_project_name}-db-subnet-group"
    }
  )
}
# Security Group for all PR ECS Services (shared among all PRs)
resource "aws_security_group" "pr_ecs_services" {
  name        = "${var.shared_project_name}-pr-ecs-services-sg"
  description = "Shared security group for all PR ECS services to access RDS"
  vpc_id      = aws_vpc.main.id

  # No inbound rules needed - this is for outbound DB access only
  egress {
    from_port   = 3306
    to_port     = 3306
    protocol    = "tcp"
    cidr_blocks = [var.vpc_cidr_block]
    description = "Allow MySQL traffic to shared RDS within VPC"
  }

  tags = merge(local.common_tags, { Name = "${var.shared_project_name}-pr-ecs-services-sg" })
}

# RDS Security Group with static ingress rule for PR ECS services
resource "aws_security_group" "shared_rds" {
  name        = "${var.shared_project_name}-rds-sg"
  description = "Security group for shared RDS instance"
  vpc_id      = aws_vpc.main.id

  # Allow MySQL access from PR ECS services security group
  ingress {
    from_port       = 3306
    to_port         = 3306
    protocol        = "tcp"
    security_groups = [aws_security_group.pr_ecs_services.id]
    description     = "Allow MySQL access from PR ECS services"
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = merge(local.common_tags, { Name = "${var.shared_project_name}-shared-rds-sg" })
}

# Generate shared RDS admin password
resource "random_password" "shared_rds_admin_password" {
  length  = 32
  special = true
  keepers = {
    project     = var.shared_project_name
    environment = var.environment
  }
}

# Store shared RDS admin username in Parameter Store
resource "aws_ssm_parameter" "shared_rds_admin_username" {
  name        = "/${var.shared_project_name}/database/admin/username"
  type        = "String"
  value       = var.shared_rds_admin_username
  description = "Admin username for shared RDS instance"

  tags = merge(local.common_tags, {
    Name       = "${var.shared_project_name}-rds-admin-username"
    SecretType = "database-admin-username"
  })
}

# Store shared RDS admin password in Parameter Store
resource "aws_ssm_parameter" "shared_rds_admin_password" {
  name        = "/${var.shared_project_name}/database/admin/password"
  type        = "SecureString"
  value       = random_password.shared_rds_admin_password.result
  description = "Admin password for shared RDS instance"

  tags = merge(local.common_tags, {
    Name       = "${var.shared_project_name}-rds-admin-password"
    SecretType = "database-admin-password"
  })
}

# Shared RDS MySQL instance
resource "aws_db_instance" "shared" {
  identifier     = "${var.shared_project_name}-rds"
  engine         = "mysql"
  engine_version = "8.4.5"
  instance_class = var.shared_rds_instance_class

  allocated_storage = var.shared_rds_allocated_storage
  storage_type      = "gp3"
  storage_encrypted = true

  username = var.shared_rds_admin_username
  password = random_password.shared_rds_admin_password.result

  db_subnet_group_name   = aws_db_subnet_group.main.name
  vpc_security_group_ids = [aws_security_group.shared_rds.id]

  multi_az            = var.shared_rds_multi_az
  publicly_accessible = false

  backup_retention_period = var.shared_rds_backup_retention_period
  backup_window           = var.shared_rds_backup_window
  maintenance_window      = var.shared_rds_maintenance_window

  skip_final_snapshot = var.shared_rds_skip_final_snapshot

  tags = merge(local.common_tags, {
    Name    = "${var.shared_project_name}-rds"
    Purpose = "shared-database-for-pr-environments"
  })
}
