module "preview_shared_infra" {
  source = "../../modules/preview-shared-infra"

  # Project and Environment Variables
  project_name = var.project_name
  environment  = var.environment

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
  acm_certificate_arn = var.acm_certificate_arn
}