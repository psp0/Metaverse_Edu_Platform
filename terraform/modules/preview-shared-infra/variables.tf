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
variable "acm_certificate_arn" {
  description = "The ARN of the ACM certificate for the shared ALB."
  type        = string
}
