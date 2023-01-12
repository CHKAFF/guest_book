#!/bin/bash

if [ -n "$1" ]
then
  echo "New version " $1
else
  echo "Please type version in args. (ex. sh update_front_version.sh 0.0.1)"
  exit 0
fi

echo "Update version"
current_version=$(cat .version)
new_version=$1
echo $new_version > .version
echo $current_version " => " $new_version
aws --endpoint-url=https://storage.yandexcloud.net/ s3 cp .version s3://guest-book
aws --endpoint-url=https://storage.yandexcloud.net/ s3 cp index.html s3://guest-book
