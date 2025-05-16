-- MySQL 데이터베이스 및 테이블 생성 스크립트
CREATE DATABASE IF NOT EXISTS mep_db;
USE mep_db;

-- 사용자 테이블
CREATE TABLE `user` (
    `user_no` BIGINT(20) NOT NULL AUTO_INCREMENT,
    `user_id` VARCHAR(50) NOT NULL,
    `created_at` DATETIME NOT NULL,
    `updated_at` DATETIME NULL,
    PRIMARY KEY (`user_no`),
    UNIQUE KEY `idx_user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 사용자 인증 테이블
CREATE TABLE `user_auth` (
    `user_no` BIGINT(20) NOT NULL,
    `password_hash` VARCHAR(255) NOT NULL,
    `pw_changed` DATETIME NULL,
    PRIMARY KEY (`user_no`),
    CONSTRAINT `fk_user_auth_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 사용자 프로필 테이블
CREATE TABLE `user_profile` (
    `user_no` BIGINT(20) NOT NULL,
    `email` VARCHAR(100) NULL,
    `phone` VARCHAR(20) NULL,
    `name` VARCHAR(20) NULL,
    `nickname` VARCHAR(50) NOT NULL,
    `birth_date` DATE NOT NULL,
    `gender` CHAR(1) NULL,
    `updated_at` DATETIME NULL,
    PRIMARY KEY (`user_no`),
    CONSTRAINT `fk_user_profile_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 사용자 리프레시 토큰 테이블
CREATE TABLE `user_refresh_token` (
    `token_id` BIGINT NOT NULL AUTO_INCREMENT,
    `user_no` BIGINT(20) NOT NULL,
    `refresh_token` VARCHAR(512) NULL,
    `issued_at` DATETIME NULL,
    `expires_at` DATETIME NULL,
    `revoked` BOOLEAN DEFAULT FALSE NULL,
    PRIMARY KEY (`token_id`),
    INDEX `idx_user_refresh_token_user_no` (`user_no`),
    CONSTRAINT `fk_user_refresh_token_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 학습 단원 테이블
CREATE TABLE `unit` (
    `unit_id` INT NOT NULL AUTO_INCREMENT,
    `chapter_no` INT NULL,
    `title` VARCHAR(255) NOT NULL,
    `description` TEXT NULL,
    PRIMARY KEY (`unit_id`),
    INDEX `idx_unit_chapter_no` (`chapter_no`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 소단원 테이블
CREATE TABLE `sub_unit` (
    `sub_unit_id` INT NOT NULL AUTO_INCREMENT,
    `unit_id` INT NOT NULL,
    `sub_chapter_no` INT NULL,
    `sub_title` VARCHAR(255) NOT NULL,
    `description` TEXT NULL,
    PRIMARY KEY (`sub_unit_id`),
    INDEX `idx_sub_unit_unit_id` (`unit_id`),
    CONSTRAINT `fk_sub_unit_unit` FOREIGN KEY (`unit_id`) REFERENCES `unit` (`unit_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 강의 테이블
CREATE TABLE `lecture` (
    `lecture_id` INT NOT NULL AUTO_INCREMENT,
    `sub_unit_id` INT NOT NULL,
    `title` VARCHAR(255) NOT NULL,
    `content_url` TEXT NULL,
    `duration_sec` INT NOT NULL,
    PRIMARY KEY (`lecture_id`),
    INDEX `idx_lecture_sub_unit_id` (`sub_unit_id`),
    CONSTRAINT `fk_lecture_sub_unit` FOREIGN KEY (`sub_unit_id`) REFERENCES `sub_unit` (`sub_unit_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 퀴즈 테이블
CREATE TABLE `quiz` (
    `quiz_id` INT NOT NULL AUTO_INCREMENT,
    `sub_unit_id` INT NOT NULL,
    `content` TEXT NOT NULL,
    PRIMARY KEY (`quiz_id`),
    INDEX `idx_quiz_sub_unit_id` (`sub_unit_id`),
    CONSTRAINT `fk_quiz_sub_unit` FOREIGN KEY (`sub_unit_id`) REFERENCES `sub_unit` (`sub_unit_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 퀴즈 선택지 테이블
CREATE TABLE `quiz_option` (
    `option_id` INT NOT NULL AUTO_INCREMENT,
    `quiz_id` INT NOT NULL,
    `label` CHAR(1) NOT NULL,
    `option_text` TEXT NOT NULL,
    `is_answer` BOOLEAN NOT NULL,
    PRIMARY KEY (`option_id`),
    INDEX `idx_quiz_option_quiz_id` (`quiz_id`),
    CONSTRAINT `fk_quiz_option_quiz` FOREIGN KEY (`quiz_id`) REFERENCES `quiz` (`quiz_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 퀴즈 해설 테이블
CREATE TABLE `explanation` (
    `explanation_id` INT NOT NULL AUTO_INCREMENT,
    `quiz_id` INT NOT NULL,
    `commentary` TEXT NOT NULL,
    PRIMARY KEY (`explanation_id`),
    UNIQUE KEY `idx_explanation_quiz_id` (`quiz_id`),
    CONSTRAINT `fk_explanation_quiz` FOREIGN KEY (`quiz_id`) REFERENCES `quiz` (`quiz_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 퀴즈 응답 테이블
CREATE TABLE `quiz_response` (
    `response_id` INT NOT NULL AUTO_INCREMENT,
    `quiz_id` INT NOT NULL,
    `option_id` INT NOT NULL,
    `user_no` BIGINT NOT NULL,
    `is_correct` BOOLEAN NOT NULL,
    `answered_at` DATETIME NOT NULL,
    PRIMARY KEY (`response_id`),
    INDEX `idx_quiz_response_quiz_id` (`quiz_id`),
    INDEX `idx_quiz_response_option_id` (`option_id`),
    INDEX `idx_quiz_response_user_no` (`user_no`),
    CONSTRAINT `fk_quiz_response_quiz` FOREIGN KEY (`quiz_id`) REFERENCES `quiz` (`quiz_id`) ON DELETE CASCADE,
    CONSTRAINT `fk_quiz_response_option` FOREIGN KEY (`option_id`) REFERENCES `quiz_option` (`option_id`),
    CONSTRAINT `fk_quiz_response_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 문제 응답 테이블
CREATE TABLE `problem_response` (
    `response_id` BIGINT NOT NULL AUTO_INCREMENT,
    `user_no` BIGINT(20) NOT NULL,
    `problem_id` VARCHAR(50) NULL,
    `answer` TEXT NULL,
    `is_correct` BOOLEAN NULL,
    PRIMARY KEY (`response_id`),
    INDEX `idx_problem_response_user_no` (`user_no`),
    INDEX `idx_problem_response_problem_id` (`problem_id`),
    CONSTRAINT `fk_problem_response_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 사용자 단원 진행상황 테이블
CREATE TABLE `user_unit_progress` (
    `uup_id` BIGINT NOT NULL AUTO_INCREMENT,
    `user_no` BIGINT NOT NULL,
    `sub_unit_id` INT NOT NULL,
    `progress_percentage` DECIMAL(5,2) NULL,
    PRIMARY KEY (`uup_id`),
    UNIQUE KEY `idx_uup_user_subunit` (`user_no`, `sub_unit_id`),
    CONSTRAINT `fk_uup_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE,
    CONSTRAINT `fk_uup_sub_unit` FOREIGN KEY (`sub_unit_id`) REFERENCES `sub_unit` (`sub_unit_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

