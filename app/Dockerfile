# Stage 0, "builder", based on Node.js, to build and compile Angular
FROM node:alpine as builder

WORKDIR /app

#COPY package*.json /app/

COPY ./src/frontend /app/

RUN npm install

RUN npm run build:prod

# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:alpine

RUN rm -rf /usr/share/nginx/html/*

COPY --from=builder /app/dist/trades/. /usr/share/nginx/html
COPY ./src/frontend/nginx-custom.conf /etc/nginx/conf.d/default.conf
