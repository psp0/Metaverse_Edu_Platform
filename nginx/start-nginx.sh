#!/bin/bash

echo "Environment: ${ENVIRONMENT}"
echo "Domain: ${DOMAIN_NAME}"

# 인증서 디렉토리 생성
mkdir -p /etc/letsencrypt/live/${DOMAIN_NAME}

if [ "${ENVIRONMENT}" = "production" ]; then
    echo "Setting up production environment with HTTPS"
    
    # 프로덕션 환경에서는 실제 인증서가 필수
    if [ ! -f "/etc/letsencrypt/live/${DOMAIN_NAME}/fullchain.pem" ] || \
       [ ! -f "/etc/letsencrypt/live/${DOMAIN_NAME}/privkey.pem" ] || \
       [ ! -f "/etc/letsencrypt/live/${DOMAIN_NAME}/chain.pem" ]; then
        
        echo "ERROR: SSL certificates not found in production environment!"
        echo "Required certificates:"
        echo "  - /etc/letsencrypt/live/${DOMAIN_NAME}/fullchain.pem"
        echo "  - /etc/letsencrypt/live/${DOMAIN_NAME}/privkey.pem"
        echo "  - /etc/letsencrypt/live/${DOMAIN_NAME}/chain.pem"
        echo "Please ensure SSL certificates are properly mounted or generated."
        exit 1
    else
        echo "SSL certificates found"
    fi
    
    export REDIRECT_TO_HTTPS='return 301 https://$server_name$request_uri;'
    export HTTPS_SERVER_BLOCK="$(envsubst '$DOMAIN_NAME' < /etc/nginx/https-server.conf.template)"
elif [ "${ENVIRONMENT}" = "development-https" ]; then
    echo "Setting up development environment with self-signed HTTPS"
    
    # 개발환경에서 HTTPS 테스트를 위한 self-signed 인증서 생성
    if [ ! -f "/etc/letsencrypt/live/${DOMAIN_NAME}/fullchain.pem" ] || \
       [ ! -f "/etc/letsencrypt/live/${DOMAIN_NAME}/privkey.pem" ] || \
       [ ! -f "/etc/letsencrypt/live/${DOMAIN_NAME}/chain.pem" ]; then
        
        echo "Generating self-signed certificates for development HTTPS testing..."
        
        # self-signed 인증서 생성
        openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
            -keyout /etc/letsencrypt/live/${DOMAIN_NAME}/privkey.pem \
            -out /etc/letsencrypt/live/${DOMAIN_NAME}/fullchain.pem \
            -subj "/C=KR/ST=Seoul/L=Seoul/O=Development/OU=IT/CN=${DOMAIN_NAME}"
        
        # chain.pem은 fullchain.pem과 동일하게 설정 (self-signed의 경우)
        cp /etc/letsencrypt/live/${DOMAIN_NAME}/fullchain.pem /etc/letsencrypt/live/${DOMAIN_NAME}/chain.pem
        
        echo "Self-signed certificates generated successfully"
    else
        echo "SSL certificates found"
    fi
    
    export REDIRECT_TO_HTTPS='return 301 https://$server_name$request_uri;'
    export HTTPS_SERVER_BLOCK="$(envsubst '$DOMAIN_NAME' < /etc/nginx/https-server.conf.template)"
else
    echo "Setting up local environment with HTTP only"
    export REDIRECT_TO_HTTPS='# Local environment - no HTTPS redirect'
    export HTTPS_SERVER_BLOCK='# Local environment - no HTTPS server'
fi

# nginx 설정 파일 생성
envsubst '$ENVIRONMENT $DOMAIN_NAME $REDIRECT_TO_HTTPS $HTTPS_SERVER_BLOCK' < /etc/nginx/nginx.conf.template > /etc/nginx/nginx.conf

echo "Generated nginx.conf:"
echo "===================="
cat /etc/nginx/nginx.conf
echo "===================="

# nginx 시작
nginx -g 'daemon off;'