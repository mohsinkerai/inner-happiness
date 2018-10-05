#!/bin/bash

echo "Decrypting SSH Keys"
openssl enc -d -aes-256-cbc -in "Inner-Satisfaction.pem.enc" -out "Inner-Satisfaction.pem" -pass pass:$SSH_KEY_PASSWORD

echo "Changing Permissions for PEM Keys"
sudo chmod 400 Inner-Satisfaction.pem

## Server Commands Start
echo "Copying Jar to Server"
scp -i Inner-Satisfaction.pem target/inner-satisfaction-backend-0.0.1-SNAPSHOT.jar ubuntu@34.242.122.236:~/qa-build.jar

echo "SSH to Server"
ssh -i Inner-Satisfaction.pem ubuntu@34.242.122.236 <<EOF

  echo "Killing existing process"
  ps -ef | grep 'java' | grep '8080' | grep -v grep | awk '{print $2}'# | xargs -r kill -9

EOF
## Server Commands End

echo "Removing Keys"
rm Inner-Satisfaction.pem