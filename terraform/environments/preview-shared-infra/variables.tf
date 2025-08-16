# AWS Provider Configuration
variable "aws_region" {
  description = "The AWS region where resources will be deployed."
  type        = string
  default     = "ap-northeast-2"
}

variable "aws_profile" {
  description = "The AWS CLI profile to use for authentication."
  type        = string
}

# IAM Configuration
variable "iam_role_environment" {
  description = "The environment name for constructing the IAM Role ARN."
  type        = string
  default     = "dev"
}

variable "iam_role_project_prefix" {
  description = "The base project name for constructing the IAM Role ARN."
  type        = string
  default     = "mep"
}

# Project and Environment Variables
variable "project_name" {
  description = "A prefix for all resource names to ensure uniqueness."
  type        = string
  default     = "mep-shared"
}

variable "environment" {
  description = "The environment name (e.g., 'preview')."
  type        = string
  default     = "preview"
}

# Network Configuration Variables
variable "vpc_cidr_block" {
  description = "The CIDR block for the VPC."
  type        = string
  default     = "10.123.0.0/16"
}

variable "availability_zones" {
  description = "List of Availability Zones to use for subnets."
  type        = list(string)
  default     = ["ap-northeast-2a", "ap-northeast-2b"]
}

variable "public_subnet_cidr_blocks" {
  description = "List of public subnet CIDR blocks. Cannot exceed the number of availability zones."
  type        = list(string)
  default     = ["10.123.1.0/24", "10.123.3.0/24"]
}

variable "private_subnet_cidr_blocks" {
  description = "List of private subnet CIDR blocks. Cannot exceed the number of availability zones."
  type        = list(string)
  default     = ["10.123.2.0/24", "10.123.4.0/24"]
}

# ECS Configuration Variables
variable "ecs_availability_zones" {
  description = "List of availability zones where ECS instances can be deployed. Should be a subset of availability_zones."
  type        = list(string)
  default     = ["ap-northeast-2a", "ap-northeast-2b"]
}

variable "az_instance_type_map" {
  description = "Map of Availability Zone to EC2 instance type for ECS."
  type        = map(string)
  default = {
    "ap-northeast-2a" = "t2.micro"
    "ap-northeast-2b" = "t3.micro"
    "ap-northeast-2c" = "t2.micro"
  }
}

variable "ecs_instance_type" {
  description = "Instance type for the ECS EC2 instances."
  type        = string
  default     = "t3.medium"
}

variable "ecs_desired_capacity_total" {
  description = "Total desired capacity for the ECS Auto Scaling Group across all availability zones"
  type        = number
  default     = 1
}

variable "ecs_min_size_total" {
  description = "Total minimum size for the ECS Auto Scaling Group across all availability zones"
  type        = number
  default     = 0
}

variable "ecs_max_size_total" {
  description = "Total maximum size for the ECS Auto Scaling Group across all availability zones"
  type        = number
  default     = 2
}

# Security and Certificate Variables
variable "acm_certificate_arn" {
  description = "The ARN of the ACM certificate for the shared ALB."
  type        = string
}
