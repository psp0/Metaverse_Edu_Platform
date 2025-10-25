-- MySQL 테이블 생성 스크립트 (GitHub Actions Flyway용)

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

-- 과목 테이블
CREATE TABLE `subject` (
    `subject_id` INT NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL,
    PRIMARY KEY (`subject_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 학습 단원 테이블
CREATE TABLE `unit` (
    `unit_id` INT NOT NULL AUTO_INCREMENT,
    `subject_id` INT NOT NULL,
    `chapter_no` INT NULL,
    `title` VARCHAR(255) NOT NULL,
    PRIMARY KEY (`unit_id`),
    CONSTRAINT `fk_unit_subject` FOREIGN KEY (`subject_id`) REFERENCES `subject` (`subject_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- 소단원 그룹 테이블
CREATE TABLE `sub_unit_group` (
    `sub_unit_group_id` INT NOT NULL AUTO_INCREMENT,
    `unit_id` INT NOT NULL,
    `subunit_title` VARCHAR(255) NOT NULL,
    PRIMARY KEY (`sub_unit_group_id`),
    CONSTRAINT `fk_sub_unit_group_to_unit` FOREIGN KEY (`unit_id`) REFERENCES `unit` (`unit_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- 소단원 테이블
CREATE TABLE `sub_unit` (
    `sub_unit_id` INT NOT NULL AUTO_INCREMENT,
    `sub_unit_group_id` INT NOT NULL,
    `detail` VARCHAR(255) NULL,
    PRIMARY KEY (`sub_unit_id`),
    CONSTRAINT `fk_sub_unit_to_group` FOREIGN KEY (`sub_unit_group_id`) REFERENCES `sub_unit_group` (`sub_unit_group_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- 강의 테이블
CREATE TABLE `lecture` (
    `lecture_id` INT NOT NULL AUTO_INCREMENT,
    `sub_unit_id` INT NOT NULL,
    `title` VARCHAR(255) NOT NULL,
    `content_url` TEXT NULL,
    `duration_sec` INT NOT NULL,
    PRIMARY KEY (`lecture_id`),
    CONSTRAINT `fk_lecture_sub_unit` FOREIGN KEY (`sub_unit_id`) REFERENCES `sub_unit` (`sub_unit_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- 퀴즈 테이블
CREATE TABLE `quiz` (
    `quiz_id` INT NOT NULL AUTO_INCREMENT,
    `sub_unit_id` INT NOT NULL,
    `content` TEXT NOT NULL,
    PRIMARY KEY (`quiz_id`),
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
    CONSTRAINT `fk_quiz_response_quiz` FOREIGN KEY (`quiz_id`) REFERENCES `quiz` (`quiz_id`) ON DELETE CASCADE,
    CONSTRAINT `fk_quiz_response_option` FOREIGN KEY (`option_id`) REFERENCES `quiz_option` (`option_id`),
    CONSTRAINT `fk_quiz_response_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- 문제 응답 테이블
CREATE TABLE `problem_response` (
    `response_id` BIGINT NOT NULL AUTO_INCREMENT,
    `user_no` BIGINT NOT NULL,
    `problem_id` VARCHAR(50) NULL,
    `answer` TEXT NULL,
    `is_correct` BOOLEAN NULL,
    PRIMARY KEY (`response_id`),
    CONSTRAINT `fk_problem_response_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


-- 사용자 단원 진행상황 테이블
CREATE TABLE `user_unit_progress` (
    `uup_id` BIGINT NOT NULL AUTO_INCREMENT,
    `user_no` BIGINT NOT NULL,
    `sub_unit_id` INT NOT NULL,
    `progress_percentage` DECIMAL(5,2) DEFAULT 0.00,
    `lecture_progress` DECIMAL(5,2) NOT NULL DEFAULT 0.00,
    `quiz_progress` DECIMAL(5,2) NOT NULL DEFAULT 0.00,
    `lecture_last_timestamp_sec` INT NOT NULL DEFAULT 0,
    `updated_at` DATETIME NULL,
    PRIMARY KEY (`uup_id`),
    UNIQUE KEY `idx_uup_user_subunit` (`user_no`, `sub_unit_id`),
    CONSTRAINT `fk_uup_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE,
    CONSTRAINT `fk_uup_sub_unit` FOREIGN KEY (`sub_unit_id`) REFERENCES `sub_unit` (`sub_unit_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 사용자 스킬 숙달도 테이블
CREATE TABLE `user_skill_proficiency` (
    `proficiency_id` BIGINT NOT NULL AUTO_INCREMENT,
    `user_no` BIGINT NOT NULL,
    `sub_unit_group_id` INT NOT NULL,
    `proficiency_percentage` DECIMAL(5,2) NOT NULL,
    `updated_at` DATETIME NOT NULL,
    PRIMARY KEY (`proficiency_id`),
    UNIQUE KEY `idx_usp_user_subunit_group` (`user_no`, `sub_unit_group_id`),
    CONSTRAINT `fk_usp_user` FOREIGN KEY (`user_no`) REFERENCES `user` (`user_no`) ON DELETE CASCADE,
    CONSTRAINT `fk_usp_sub_unit_group` FOREIGN KEY (`sub_unit_group_id`) REFERENCES `sub_unit_group` (`sub_unit_group_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;