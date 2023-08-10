# Postfix

FROM alpine:3.17

ADD main.cf /etc/postfix/main.cf
ADD postfixstart.sh /postfixstart.sh
RUN apk add --no-cache postfix tzdata && \
    cp /usr/share/zoneinfo/Asia/Tokyo /etc/localtime && \
    chown -R postfix:postfix /var/spool/postfix && \
    chmod a+x /postfixstart.sh && \
    mkdir /var/log/postfix

CMD ["/postfixstart.sh"]
