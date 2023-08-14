#!/bin/sh

# Alias set to minio-client. Also check if there is a bucket
minioServer=minio
until (mc admin info $MINIO_ALIAS_NAME)
do
    echo "...wait..."
    sleep 1
    mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
done
until (mc ls ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP})
do
    echo "...wait..."
    sleep 1
    mc mb ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}
done

# Backup start
dbServer=mongo
dbName=growi
tempDir=/tmp/tempDumpDir
backupFile=/tmp/mongodb_$(date +%Y%m%d_%H%M%S).tar.gz
mongodump -h $dbServer -o $tempDir -d $dbName

# Pack/Compress
tar czvf $backupFile $tempDir

# Upload to Minio
mc cp $backupFile ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}

# Delete temporary data
rm -rf $tempDir
rm -rf $backupFile
