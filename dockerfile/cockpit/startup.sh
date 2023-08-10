#!/bin/sh

# start ASP.NET server
dllPath=/usr/src/app/aspnet/CockpitApp.dll
aspnetAddress=*
aspnetPort=5000
if [ -f $dllPath ]; then
    if [ -n $ASPNET_ADDRESS ]; then
        aspnetAddress=$ASPNET_ADDRESS
    fi
    if [ -n $ASPNET_PORT ]; then
        aspnetPort=$ASPNET_PORT
    fi
    dotnet $dllPath --Address $ASPNET_ADDRESS --port $ASPNET_PORT &
fi

cd $REACT_APP_NAME
yarn start

# Note:
# =================================
# docker compose build
# docker compose run --rm cockpit sh -c "create-react-app cockpit --template typescript"

# yarn add @mui/material @emotion/react @emotion/styled

