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

if [ ! -d $REACT_APP_NAME ]; then
    create-react-app $REACT_APP_NAME --template typescript
fi

cd $REACT_APP_NAME
yarn start


# yarn add @mui/material @emotion/react @emotion/styled