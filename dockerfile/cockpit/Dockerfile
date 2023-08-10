FROM node:18.15.0-alpine

WORKDIR /usr/src/app

# set react runtime
RUN yarn global add create-react-app
RUN chown -R node:node /usr/src/app

# set aspnet runtime
RUN apk --update --no-cache add aspnetcore6-runtime

# set timezone
RUN apk --update --no-cache add tzdata && \
    cp /usr/share/zoneinfo/Asia/Tokyo /etc/localtime && \
    apk del tzdata

# set startup script
ADD startup.sh /startup.sh
RUN chmod a+x /startup.sh

# set docker
RUN apk --update --no-cache add docker && \
    addgroup node docker && \
    sed -i s/docker:x:101/docker:x:999/ /etc/group

USER node

CMD ["/startup.sh"]
