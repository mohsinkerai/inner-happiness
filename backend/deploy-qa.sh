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
sleep 1s
echo "Killing Process of Existing Java QA"
ps -ef | grep 'java' | grep '8080' | grep -v grep | awk '{print \$2}' | xargs -r kill -9
echo "Starting Process of QA"
java -jar qa-build.jar --server.port=8080 --spring.datasource.password=${DB_QA_PW} --spring.datasource.host=${DB_QA_HOST} --spring.datasource.username=${DB_QA_USER} --spring.datasource.db=inner_satisfaction --logging.file=app-qa.log -Xmx700m > startup_log.out 2>&1 &
sleep 10s
EOF

#ssh -i Inner-Satisfaction.pem ubuntu@34.242.122.236 "ps -ef | grep 'java' | grep '8080' | grep -v grep | awk '{print \$2}'"
## Backup Command
##kill $(lsof -i:5000 -t)
## Server Commands End

#ps -ef | grep 'java' | grep '8080' | grep -v grep | awk '{print $2}' | xargs -r kill -9

echo "Removing Keys"
rm -rf Inner-Satisfaction.pem