# --- Build stage ---
FROM node:20 AS build
WORKDIR /app

# install deps (cache-friendly)
COPY package*.json ./
RUN npm ci

# copy source and build
COPY . .
# optional: pass API base URL at build time -> docker build --build-arg VITE_API_BASE_URL=http://localhost:5000 .
ARG VITE_API_BASE_URL=http://localhost:5000
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL
RUN npm run build

# --- Serve stage (Nginx) ---
FROM nginx:alpine
# (optional) custom nginx config for SPA routing (uncomment if you add nginx.conf)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80

# healthcheck (optional)
HEALTHCHECK CMD wget -qO- http://localhost/ || exit 1
