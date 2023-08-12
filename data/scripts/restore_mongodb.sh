#!/bin/sh

# Alias set to minio-client
minioServer=minio
until (mc admin info $MINIO_ALIAS_NAME)
do
    echo "...waitint..."
    sleep 1
    mc alias set $MINIO_ALIAS_NAME http://${minioServer}:9000 $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
done

# Download backup data
# (Obtain the last file from the results output by the mc ls command)
bkFileName=$(mc --json ls ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP} | jq -r ".key" | tail -n 1)
backupFile=/tmp/$bkFileName
mc cp ${MINIO_ALIAS_NAME}/${MINIO_BUCKET_BACKUP}/${bkFileName} $backupFile

# Unzip/Extract
tempDir=/tmp/tempDumpDir
mkdir -p $tempDir
tar xzvf $backupFile -C $tempDir --strip-components 2

# Start restore
dbServer=mongo
mongorestore -h $dbServer --drop $tempDir

# Delete temporary data
rm -rf $backupFile
rm -rf $tempDir

# Restart Growi container
growiServer=growi-growi
docker container restart growi-growi
