### STAGE 1: Build ###
FROM node:14.16.1 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build --prod
### STAGE 2: Run ###
FROM nginx:1.20.0-alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/webapp /usr/share/nginx/html