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

# set backup/restore tools
RUN apk --update --no-cache add docker && \
    addgroup node docker && \
    sed -i s/docker:x:101/docker:x:999/ /etc/group
RUN apk --update --no-cache add mongodb-tools jq curl
RUN wget -O /usr/local/bin/mc https://dl.min.io/client/mc/release/linux-amd64/mc
RUN chmod a+x /usr/local/bin/mc

# set startup script
ADD startup.sh /startup.sh
RUN chmod a+x /startup.sh

USER node

CMD ["/startup.sh"]
