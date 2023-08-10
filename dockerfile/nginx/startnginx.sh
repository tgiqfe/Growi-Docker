#!/bin/sh

# 設定ファイルの中の環境変数を解決
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

# 設定ファイルテンプレートを削除
rm -f /etc/nginx/conf.d/growi.template
rm -f /etc/nginx/conf.d/codimd.template
rm -f /etc/nginx/conf.d/minio.template

nginx -g "daemon off;"
