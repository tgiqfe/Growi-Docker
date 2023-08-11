#!/bin/sh

# start ASP.NET server
if [ -d $ASPNET_APP_NAME ]; then
    cd $ASPNET_APP_NAME
    dotnet run &
    cd ..
fi

# start react app
cd $REACT_APP_NAME
if [ ! -d node_modules ]; then
    yarn install
fi
yarn start

# Note:
# =================================
# docker compose build
# docker compose run --rm cockpit sh -c "create-react-app cockpit --template typescript"

# yarn add @mui/material @emotion/react @emotion/styled

