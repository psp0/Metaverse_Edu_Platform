module "preview_shared_infra" {
  source = "../../modules/preview-shared-infra"

  # Project and Environment Variables
  shared_project_name = var.shared_project_name
  environment         = var.environment

  # Network Configuration Variables
  vpc_cidr_block              = var.vpc_cidr_block
  availability_zones          = var.availability_zones
  public_subnet_cidr_blocks   = var.public_subnet_cidr_blocks
  private_subnet_cidr_blocks  = var.private_subnet_cidr_blocks

  # ECS Configuration Variables
  ecs_availability_zones      = var.ecs_availability_zones
  az_instance_type_map        = var.az_instance_type_map
  ecs_instance_type           = var.ecs_instance_type
  ecs_desired_capacity_total  = var.ecs_desired_capacity_total
  ecs_min_size_total          = var.ecs_min_size_total
  ecs_max_size_total          = var.ecs_max_size_total

  # Security and Certificate Variables
  acm_certificate_domain = var.acm_certificate_domain

  # RDS Configuration Variables
  shared_rds_instance_class         = var.shared_rds_instance_class
  shared_rds_allocated_storage      = var.shared_rds_allocated_storage
  shared_rds_admin_username         = var.shared_rds_admin_username
  shared_rds_multi_az              = var.shared_rds_multi_az
  shared_rds_backup_retention_period = var.shared_rds_backup_retention_period
  shared_rds_backup_window         = var.shared_rds_backup_window
  shared_rds_maintenance_window    = var.shared_rds_maintenance_window
  shared_rds_skip_final_snapshot   = var.shared_rds_skip_final_snapshot
}