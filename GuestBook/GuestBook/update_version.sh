#!/bin/bash

if [ -n "$1" ]
then
  echo "New version " $1
else
  echo "Please type version in args. (ex. sh update_version.sh 0.0.1)"
  exit 0
fi

echo "Update version"
current_version=$(cat .version);
new_version=$1
echo $new_version > .version;
echo $current_version " => " $new_version ;

image_name=${REPOSITORY}/api:$new_version;
echo "Image name: " $image_name;

echo "Docker build and push"
docker build -t $image_name . ;
docker push $image_name;

echo "Create Serverless Container"
yc sls container revisions deploy \
    --image "$image_name" \
    --container-id ${CONTAINER_ID} \
    --service-account-id ${SERVICE_ACCOUNT_ID} \
    --execution-timeout 5s \
    --concurrency 4 \
    --memory 512M \
    --cores 1;