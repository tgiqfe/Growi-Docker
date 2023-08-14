# Minio Cmd setup

(sample)

ユーザー名やエイリアス名等は、``.env``ファイルを参照して値を取得する前提。


## エイリアス設定

コマンド
```bash
minioServer=minio
mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
```

エイリアスが無ければ作る場合の書き方  
※普通に``mc admin info ～``で確認した後に終了コードを``$?``で分岐する方法では、うまくいかない。
```bash
minioServer=minio
until (mc admin info $MINIO_ALIAS_NAME)
do
    echo "...wait..."
    sleep 1
    mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
done
```

## バケット作成

コマンド
```bash
mc mb ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}
```

バケットが無ければ作る場合の書き方
```bash
until (mc ls ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP})
do
    echo "...wait..."
    sleep 1
    mc mb ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}
done
```

## ユーザー作成

ユーザーを新規作成
```bash
userName=newUser
userPass=newUserPassword
mc admin user add $MINIO_ALIAS_NAME $userName $userPass
```

有効/無効化
```bash
userName=newUser

# 有効化
mc admin user enable $MINIO_ALIAS_NAME $userName

# 無効化
mc admin user disable $MINIO_ALIAS_NAME $userName
```

ユーザー情報の確認
```bash
# 一覧確認
mc admin user ls $MINIO_ALIAS_NAME

# ユーザー個別確認
userName=newUser
mc admin user info $MINIO_ALIAS_NAME $userName
```

ユーザー削除
```bash
userName=newUser
mc admin user rm $MINIO_ALIAS_NAME $userName
```

Access Key, Secret Keyを作成  
※ポリシー指定は可能だが、Webコンソールで作ったときのように、``Restrict beyond user policy``の設定をいじることは不可 (Go SDK等では可能らしいが、mcコマンドでは不可とのこと)
```bash
userName=newUser

# Access Key, Secret Keyを自動生成
mc admin user svcacct add $MINIO_ALIAS_NAME $userName

# Access Key, Secret Keyを指定
mc admin user svcacct add --accesskey "newAccessKey" --secret-key "newSecretKey" $MINIO_ALIAS_NAME $userName
```
