# VPC Outputs
output "vpc_id" {
  description = "The ID of the VPC"
  value       = module.preview_shared_infra.vpc_id
}

# Subnet Outputs
output "public_subnet_ids" {
  description = "List of public subnet IDs"
  value       = module.preview_shared_infra.public_subnet_ids
}

output "private_subnet_ids" {
  description = "List of private subnet IDs"
  value       = module.preview_shared_infra.private_subnet_ids
}

# ECS Cluster Outputs
output "ecs_cluster_name" {
  description = "The name of the ECS cluster"
  value       = module.preview_shared_infra.ecs_cluster_name
}

# ALB Outputs
output "alb_arn" {
  description = "The ARN of the Application Load Balancer"
  value       = module.preview_shared_infra.alb_arn
}

output "alb_listener_https_arn" {
  description = "The ARN of the HTTPS listener"
  value       = module.preview_shared_infra.alb_listener_https_arn
}

output "alb_security_group_id" {
  description = "The ID of the ALB security group"
  value       = module.preview_shared_infra.alb_security_group_id
}

# Database Subnet Group
output "db_subnet_group_name" {
  description = "The name of the database subnet group"
  value       = module.preview_shared_infra.db_subnet_group_name
}
