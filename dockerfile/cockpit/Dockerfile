FROM node:18.15.0-alpine

WORKDIR /usr/src/app

# set react runtime
RUN yarn global add create-react-app
RUN chown -R node:node /usr/src/app

# set aspnet sdk
#RUN apk --update --no-cache add aspnetcore6-runtime
RUN apk --update --no-cache add dotnet6-sdk

# set timezone
RUN apk --update --no-cache add tzdata && \
    cp /usr/share/zoneinfo/Asia/Tokyo /etc/localtime && \
    apk del tzdata

# set docker
RUN apk --update --no-cache add docker && \
    addgroup node docker && \
    sed -i s/docker:x:101/docker:x:999/ /etc/group

# set backup/restore tools
RUN apk --update --no-cache add mongodb-tools
RUN wget -O /usr/local/bin/mc https://dl.min.io/client/mc/release/linux-amd64/mc
RUN chmod a+x /usr/local/bin/mc
RUN mc alias remove gcs && \
    mc alias remove play && \
    mc alias remove s3 && \
    mc alias remove local && \
    mc alias list

# set startup script
ADD startup.sh /startup.sh
RUN chmod a+x /startup.sh

USER node

CMD ["/startup.sh"]
