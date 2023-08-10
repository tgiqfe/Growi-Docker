# Nginx

FROM nginx:1.22.1-alpine

ADD startnginx.sh /startnginx.sh
RUN apk add --no-cache tzdata && \
    cp /usr/share/zoneinfo/Asia/Tokyo /etc/localtime && \
    rm /etc/nginx/conf.d/default.conf && \
    chmod a+x /startnginx.sh

ADD conf.d /etc/nginx/conf.d

WORKDIR /etc/nginx

CMD ["/startnginx.sh"]
