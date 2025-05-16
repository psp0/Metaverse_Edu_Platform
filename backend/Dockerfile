FROM gradle:jdk11 AS builder
WORKDIR /app
COPY build.gradle settings.gradle ./
COPY src ./src
RUN gradle bootJar --no-daemon

FROM openjdk:11-jre-slim
WORKDIR /app
RUN apt-get update && apt-get install -y netcat-openbsd
COPY --from=builder /app/build/libs/*.jar app.jar
EXPOSE 8080
COPY wait-for-it.sh .
RUN chmod +x ./wait-for-it.sh
ENTRYPOINT ["/app/wait-for-it.sh", "db", "3306", "java", "-jar", "/app/app.jar"]