FROM nginx:latest

# openssl 설치 (self-signed 인증서 생성용)
RUN apt-get update && \
    apt-get install -y openssl && \
    rm -rf /var/lib/apt/lists/*

# 스크립트 실행 권한 설정을 위해 entrypoint에서 처리
COPY start-nginx.sh /start-nginx.sh
RUN chmod +x /start-nginx.sh

# 기본 entrypoint 사용
ENTRYPOINT ["/start-nginx.sh"]