# Use Unity WebGL build image
FROM unityci/editor:ubuntu-2022.3.10f1-webgl-1 AS builder

# Set working directory
WORKDIR /app

# Copy entire Unity project
COPY . .

# Build WebGL version
RUN unity-editor -quit -batchmode -projectPath . -executeMethod BuildScript.BuildWebGL

# Use nginx to serve WebGL build
FROM nginx:alpine

# Copy WebGL build
COPY --from=builder /app/Build /usr/share/nginx/html

# Expose port
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
