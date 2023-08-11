#!/bin/sh

# Resolve environment variables in configuration files.
envsubst \
    '$PATH_SERVERCRT $PATH_SERVERKEY $PATH_CLIENTCRT $URL_GROWI' < \
    /etc/nginx/conf.d/growi.template > \
    /etc/nginx/conf.d/growi.conf
envsubst \
    '$PATH_SERVERCRT $PATH_SERVERKEY $PATH_CLIENTCRT $URL_CODIMD' < \
    /etc/nginx/conf.d/codimd.template > \
    /etc/nginx/conf.d/codimd.conf
envsubst \
    '$PATH_SERVERCRT $PATH_SERVERKEY $PATH_CLIENTCRT $URL_MINIO' < \
    /etc/nginx/conf.d/minio.template > \
    /etc/nginx/conf.d/minio.conf
envsubst \
    '$PATH_SERVERCRT $PATH_SERVERKEY $PATH_CLIENTCRT $URL_COCKPIT' < \
    /etc/nginx/conf.d/cockpit.template > \
    /etc/nginx/conf.d/cockpit.conf

# Delete template config file.
rm -f /etc/nginx/conf.d/growi.template
rm -f /etc/nginx/conf.d/codimd.template
rm -f /etc/nginx/conf.d/minio.template
rm -f /etc/nginx/conf.d/cockpit.template

nginx -g "daemon off;"
