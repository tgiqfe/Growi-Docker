# MongoDB backup/restore

## パラメータ

| 設定名 | パラメータ | 値 | 備考 |
| ------ | ---------- | -- | ---- |
| MongoDBサーバ | dbServer | mongo | 同docker-compose内のアクセス |
| Minioサーバ | minioServer | minio | 同docker-compose内のアクセス |
| 対象データベース | dbName | growi | docker-compose.ymlのgrowiコンテナの``mongodb://mongo:27017/growi``で指定している |
| Minioのエイリアス名 | MINIO_ALIAS_NAME | (任意) | .envファイルに記述して、docker-compose.ymlの``environment``パラメータ経由でセット |

## バックアップ

Growi用MongoDBのバックアップ

### 処理フロー

1. minio-clientへエイリアスをセット(未設定の場合のみ)
2. ``mongodump``コマンドでダンプ取得
3. ``tar``コマンドでパック/圧縮
4. ``mc``コマンドでMinioサーバへアップロード
5. 一時データを削除

### スクリプト

```bash
# minio-clientへのエイリアスセット
minioServer=minio
mc admin info $MINIO_ALIAS_NAME
if [ $? -ne 0 ]; then
    mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
fi

# バックアップ開始
dbServer=mongo
dbName=growi
tempDir=/tmp/tempDumpDir
backupFile=mongodb_$(date +%Y%m%d_%H%M%S).tar.gz
mongodump -h $dbServer -o $tempDir -d $dbName

tar czvf /tmp/$backupFile $tempDir
rm -rf $tempDir
```

## リストア

Growi用MongoDBのリストア

### 処理フロー

1. minio-clientへエイリアスをセット(未設定の場合のみ)
2. ``mc``コマンドでMinioサーバからダウンロード
3. ``tar``コマンドで解凍/展開
4. ``mongorestore``でリストア
5. 一時データを削除

### スクリプト

```bash
# minio-clientへのエイリアスセット
minioServer=minio
mc admin info $MINIO_ALIAS_NAME
if [ $? -ne 0 ]; then
    mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
fi

# リストア開始
dbServer=mongo
#dbName=growi
tempDir=/tmp/tempDumpDir
backupFile=mongodb_$(date +%Y%m%d_%H%M%S).tar.gz
tar xzvf $backupFile -C $tempDir --strip-components 2
mongorestore -h $dbServer --drop $tempDir

rm -rf $backupFile
rm -rf $tempDir
```
