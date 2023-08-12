#!/bin/sh

# start ASP.NET server
if [ -d $ASPNET_APP_NAME ]; then
    cd $ASPNET_APP_NAME
    dotnet build --configuration Release
    dotnet run --configuration Release -- -a * -p 4000 -s "/usr/src/app/scripts" &
    cd ..
fi

# start react app
cd $REACT_APP_NAME
yarn install
yarn start

# Note:
# =================================
# docker compose build
# docker compose run --rm cockpit sh -c "create-react-app cockpit --template typescript"

# yarn add @mui/material @emotion/react @emotion/styled

