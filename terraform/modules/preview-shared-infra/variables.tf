# Project and Environment Variables
variable "shared_project_name" {
  description = "A prefix for all resource names to ensure uniqueness."
  type        = string
}

variable "environment" {
  description = "The environment name (e.g., 'preview')."
  type        = string
}

# Network Configuration Variables
variable "vpc_cidr_block" {
  description = "The CIDR block for the VPC."
  type        = string
}

variable "availability_zones" {
  description = "List of Availability Zones to use for subnets in order of preference."
  type        = list(string)

  validation {
    condition     = length(var.availability_zones) >= 1
    error_message = "At least one availability zone must be specified."
  }
}

variable "public_subnet_cidr_blocks" {
  description = "List of public subnet CIDR blocks. Cannot exceed the number of availability zones."
  type        = list(string)

  validation {
    condition     = length(var.public_subnet_cidr_blocks) >= 1
    error_message = "At least one public subnet CIDR block must be provided."
  }

  validation {
    condition     = length(var.public_subnet_cidr_blocks) <= length(var.availability_zones)
    error_message = "Number of public subnet CIDR blocks cannot exceed the number of availability zones."
  }
}

variable "private_subnet_cidr_blocks" {
  description = "List of private subnet CIDR blocks. Cannot exceed the number of availability zones."
  type        = list(string)

  validation {
    condition     = length(var.private_subnet_cidr_blocks) >= 1
    error_message = "At least one private subnet CIDR block must be provided."
  }

  validation {
    condition     = length(var.private_subnet_cidr_blocks) <= length(var.availability_zones)
    error_message = "Number of private subnet CIDR blocks cannot exceed the number of availability zones."
  }

  validation {
    condition     = length(var.private_subnet_cidr_blocks) == length(var.public_subnet_cidr_blocks)
    error_message = "Private subnet CIDR blocks and public subnet CIDR blocks must have the same length."
  }
}

# ECS Configuration Variables
variable "ecs_availability_zones" {
  description = "List of availability zones where ECS instances can be deployed. Should be a subset of availability_zones."
  type        = list(string)

  validation {
    condition     = length(var.ecs_availability_zones) >= 1
    error_message = "At least one availability zone must be specified for ECS."
  }
}

variable "az_instance_type_map" {
  description = "Map of Availability Zone to EC2 instance type for ECS."
  type        = map(string)
}

variable "ecs_instance_type" {
  description = "Instance type for the ECS EC2 instances."
  type        = string
}

variable "ecs_desired_capacity_total" {
  description = "Total desired capacity for the ECS Auto Scaling Group across all availability zones"
  type        = number

  validation {
    condition     = var.ecs_desired_capacity_total >= 0
    error_message = "ECS desired capacity total must be non-negative."
  }
}

variable "ecs_min_size_total" {
  description = "Total minimum size for the ECS Auto Scaling Group across all availability zones"
  type        = number

  validation {
    condition     = var.ecs_min_size_total >= 0
    error_message = "ECS minimum size total must be non-negative."
  }
}

variable "ecs_max_size_total" {
  description = "Total maximum size for the ECS Auto Scaling Group across all availability zones"
  type        = number

  validation {
    condition     = var.ecs_max_size_total >= 1
    error_message = "ECS maximum size total must be at least 1."
  }
}

# Security and Certificate Variables
variable "acm_certificate_domain" {
  description = "The domain name for which to find the ACM certificate (e.g., example.com or *.example.com)"
  type        = string
}

variable "shared_rds_instance_class" {
  description = "The instance class for the shared RDS instance"
  type        = string
}

variable "shared_rds_allocated_storage" {
  description = "The allocated storage for the shared RDS instance (GB)"
  type        = number
}

variable "shared_rds_admin_username" {
  description = "The admin username for the shared RDS instance"
  type        = string
  sensitive   = true
}

variable "shared_rds_multi_az" {
  description = "Whether to enable Multi-AZ deployment for high availability"
  type        = bool
}

variable "shared_rds_backup_retention_period" {
  description = "The backup retention period (days)"
  type        = number

  validation {
    condition     = var.shared_rds_backup_retention_period >= 0 && var.shared_rds_backup_retention_period <= 35
    error_message = "Backup retention period must be between 0 and 35 days."
  }
}

variable "shared_rds_backup_window" {
  description = "The preferred backup window (UTC)"
  type        = string
}

variable "shared_rds_maintenance_window" {
  description = "The preferred maintenance window (UTC)"
  type        = string
}

variable "shared_rds_skip_final_snapshot" {
  description = "Whether to skip final snapshot when deleting"
  type        = bool
}
