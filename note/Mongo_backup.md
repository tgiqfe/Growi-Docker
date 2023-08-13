# MongoDB backup/restore

## パラメータ

| 設定名 | パラメータ | 値 | 備考 |
| ------ | ---------- | -- | ---- |
| MongoDBサーバ | dbServer | mongo | 同docker-compose内のアクセス |
| Minioサーバ | minioServer | minio | 同上 |
| Growiコンテナ | growiServer | growi-growi | docker-compose.ymlで指定したコンテナ名 |
| 対象データベース | dbName | growi | docker-compose.ymlのgrowiコンテナの``mongodb://mongo:27017/growi``で指定している |
| Minio管理者ユーザー | MINIO_ROOT_USER | (任意) | .envファイルに記述して、docker-compose.ymlの``environment``パラメータ経由でセット |
| Minio管理者パスワード | MINIO_ROOT_PASSWORD | (任意) | 同上 |
| Minioエイリアス名 | MINIO_ALIAS_NAME | (任意) | 同上 |
| Minioバケット名 | MINIO_BUCKET_BACKUP | (任意) | 同上 |

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
# minio-clientへのエイリアスセット。ついでにバケット有無もチェック
minioServer=minio
until (mc admin info $MINIO_ALIAS_NAME)
do
    echo "...waitint..."
    sleep 1
    mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
done
until (mc ls ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP})
do
    echo "...waitint..."
    sleep 1
    mc mb ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}
done

# バックアップ開始
dbServer=mongo
dbName=growi
tempDir=/tmp/tempDumpDir
backupFile=/tmp/mongodb_$(date +%Y%m%d_%H%M%S).tar.gz
mongodump -h $dbServer -o $tempDir -d $dbName

# パック/圧縮
tar czvf $backupFile $tempDir

# Minioへアップロード
mc cp $backupFile ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}

# 一時データを削除
rm -rf $tempDir
rm -rf $backupFile
```

## リストア

Growi用MongoDBのリストア

### 処理フロー

1. minio-clientへエイリアスをセット(未設定の場合のみ)
2. ``mc``コマンドでMinioサーバからダウンロード
3. ``tar``コマンドで解凍/展開
4. ``mongorestore``でリストア
5. 一時データを削除
6. Growiコンテナを再起動

### スクリプト

```bash
# minio-clientへのエイリアスセット
minioServer=minio
until (mc admin info $MINIO_ALIAS_NAME)
do
    echo "...waitint..."
    sleep 1
    mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
done

# バックアップデータをダウンロード
# (mc lsコマンドで出力した結果から、最後の1つのファイルを取得)
bkFileName=$(mc --json ls ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP} | jq -r ".key" | tail -n 1)
backupFile=/tmp/$bkFileName
mc cp ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}/${bkFileName} $backupFile

# 解凍/展開
tempDir=/tmp/tempDumpDir
mkdir -p $tempDir
tar xzvf $backupFile -C $tempDir --strip-components 2

# リストア開始
dbServer=mongo
mongorestore -h $dbServer --drop $tempDir

# 一時データを削除
rm -rf $backupFile
rm -rf $tempDir

# Growiコンテナを再起動
growiServer=growi-growi
docker container restart growi-growi
```
