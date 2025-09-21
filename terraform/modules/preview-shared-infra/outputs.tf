# VPC Outputs
output "vpc_id" {
  description = "The ID of the VPC"
  value       = aws_vpc.main.id
}

output "vpc_cidr_block" {
  description = "The CIDR block of the VPC"
  value       = aws_vpc.main.cidr_block
}

# Subnet Outputs
output "public_subnet_ids" {
  description = "List of public subnet IDs"
  value       = [for subnet in aws_subnet.public : subnet.id]
}

output "private_subnet_ids" {
  description = "List of private subnet IDs"
  value       = [for subnet in aws_subnet.private : subnet.id]
}

output "public_subnet_cidr_blocks" {
  description = "List of public subnet CIDR blocks"
  value       = [for subnet in aws_subnet.public : subnet.cidr_block]
}

output "private_subnet_cidr_blocks" {
  description = "List of private subnet CIDR blocks"
  value       = [for subnet in aws_subnet.private : subnet.cidr_block]
}

# Internet Gateway Output
output "internet_gateway_id" {
  description = "The ID of the Internet Gateway"
  value       = aws_internet_gateway.main.id
}


# Route Table Outputs
output "public_route_table_id" {
  description = "The ID of the public route table"
  value       = aws_route_table.public.id
}

output "private_route_table_ids" {
  description = "Map of availability zone to private route table ID"
  value       = { for k, v in aws_route_table.private : k => v.id }
}

# ALB Outputs
output "alb_arn" {
  description = "The ARN of the Application Load Balancer"
  value       = aws_lb.main.arn
}

output "alb_dns_name" {
  description = "The DNS name of the Application Load Balancer"
  value       = aws_lb.main.dns_name
}

output "alb_zone_id" {
  description = "The canonical hosted zone ID of the load balancer"
  value       = aws_lb.main.zone_id
}

output "alb_security_group_id" {
  description = "The ID of the ALB security group"
  value       = aws_security_group.alb.id
}

output "alb_listener_https_arn" {
  description = "The ARN of the HTTPS listener"
  value       = aws_lb_listener.https.arn
}

output "alb_listener_http_arn" {
  description = "The ARN of the HTTP listener"
  value       = aws_lb_listener.http.arn
}

# ECS Cluster Outputs
output "ecs_cluster_id" {
  description = "The ID of the ECS cluster"
  value       = aws_ecs_cluster.main.id
}

output "ecs_cluster_arn" {
  description = "The ARN of the ECS cluster"
  value       = aws_ecs_cluster.main.arn
}

output "ecs_cluster_name" {
  description = "The name of the ECS cluster"
  value       = aws_ecs_cluster.main.name
}

# ECS Instance Security Group
output "ecs_instance_security_group_id" {
  description = "The ID of the ECS instance security group"
  value       = aws_security_group.ecs_instance.id
}

# ECS IAM Outputs
output "ecs_instance_role_arn" {
  description = "The ARN of the ECS instance IAM role"
  value       = aws_iam_role.ecs_instance_role.arn
}

output "ecs_instance_profile_name" {
  description = "The name of the ECS instance profile"
  value       = aws_iam_instance_profile.ecs_instance_profile.name
}

# Launch Template (single template)
output "ecs_launch_template_id" {
  description = "ECS launch template ID"
  value       = aws_launch_template.ecs.id
}

output "ecs_autoscaling_group_name" {
  description = "The name of the ECS Auto Scaling Group"
  value       = aws_autoscaling_group.ecs.name
}

output "ecs_autoscaling_group_arn" {
  description = "The ARN of the ECS Auto Scaling Group"
  value       = aws_autoscaling_group.ecs.arn
}

# Database Subnet Group
output "db_subnet_group_name" {
  description = "The name of the database subnet group"
  value       = aws_db_subnet_group.main.name
}

output "db_subnet_group_arn" {
  description = "The ARN of the database subnet group"
  value       = aws_db_subnet_group.main.arn
}

# Common Tags
output "common_tags" {
  description = "Common tags applied to all resources"
  value       = local.common_tags
}

output "shared_rds_endpoint" {
  description = "The RDS instance endpoint for connecting to the shared database"
  value       = aws_db_instance.shared.endpoint
}

output "shared_rds_address" {
  description = "The RDS instance hostname"
  value       = aws_db_instance.shared.address
}

output "shared_rds_port" {
  description = "The RDS instance port"
  value       = aws_db_instance.shared.port
}

output "shared_rds_security_group_id" {
  description = "The security group ID for the shared RDS instance"
  value       = aws_security_group.shared_rds.id
}

output "pr_ecs_services_security_group_id" {
  description = "The shared security group ID for all PR ECS services to access RDS"
  value       = aws_security_group.pr_ecs_services.id
}

output "shared_rds_admin_username" {
  description = "The admin username for the shared RDS instance"
  value       = aws_db_instance.shared.username
  sensitive   = true
}

output "shared_rds_parameter_store_password_name" {
  description = "The Parameter Store parameter name containing the RDS admin password"
  value       = aws_ssm_parameter.shared_rds_admin_password.name
}