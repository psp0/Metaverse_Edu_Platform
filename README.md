# MEP

## Overview

MEP(Metaverse Edu Platform)는 가상환경에서 교육 경험을 제공하는 웹 애플리케이션입니다.

## Prerequisites

- Docker
- Docker Compose

## 설치 및 실행 가이드

1. **환경 설정**

   ```bash
   cp .env.example .env
   ```

   `.env` 파일에서 다음 항목을 설정하세요:

   - Database 접속 정보
   - MySQL root 비밀번호
   - JWT secret key
   - 도메인 이름

2. **SSL 설정**

   - SSL 인증서 설정:

     ```bash
     # nginx/ssl 디렉토리 생성
     mkdir -p nginx/ssl

     # SSL 인증서 파일 복사
     cp 발급받은_인증서.crt nginx/ssl/certificate.crt
     cp 발급받은_개인키.key nginx/ssl/private.key
     cp 발급받은_중간인증서.crt nginx/ssl/ca_bundle.crt

     # SSL 인증서 파일 권한 설정
     chmod 600 nginx/ssl/private.key
     chmod 644 nginx/ssl/certificate.crt nginx/ssl/ca_bundle.crt
     ```

   - 포트 개방:
     ```bash
     sudo ufw allow 80,8443/tcp
     ```

3. **애플리케이션 실행**
   ```bash
   # 컨테이너 실행
   docker-compose up --build -d
   ```

## API Documentation

Swagger UI: `https://${DOMAIN_NAME}/swagger-ui.html`

## 로컬 개발 환경 설정

로컬 개발 환경에서 프론트엔트 애플리케이션을 실행하려면 다음 환경 변수를 설정하세요:

```bash
# 벡엔드서버의 url과 포트 설정
export BACK_SERVER_URL=https://example.com
export BACK_SERVER_PORT=5000