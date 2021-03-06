#!/bin/bash

echo "Decrypting SSH Keys"
openssl enc -d -aes-256-cbc -in "Inner-Satisfaction.pem.enc" -out "Inner-Satisfaction.pem" -pass pass:$SSH_KEY_PASSWORD

echo "Changing Permissions for PEM Keys"
sudo chmod 400 Inner-Satisfaction.pem

## Server Commands Start
echo "Copying Jar to Server"
scp -i Inner-Satisfaction.pem target/inner-satisfaction-backend-0.0.1-SNAPSHOT.jar ubuntu@34.242.122.236:~/prod-build.jar

echo "SSH to Server"
ssh -i Inner-Satisfaction.pem ubuntu@34.242.122.236 <<EOF
ps -ef | grep 'java' | grep '8090' | grep -v grep | awk '{print \$2}' | xargs -r kill -9
java -jar prod-build.jar --server.port=8090 --spring.datasource.password=${DB_PROD_PW} --spring.datasource.host=${DB_PROD_HOST} --spring.datasource.username=${DB_PROD_USER} --spring.datasource.db=inner-satisfaction-prod --logging.file=app-prod.log -Xmx800m > startup_prod_log.out 2>&1 &
EOF

mvn clean install -DskipTests=true -PapplicationServer
scp -i Inner-Satisfaction.pem target/inner-satisfaction-backend-0.0.1-SNAPSHOT.war ubuntu@34.242.122.236:~/nginx/prod-build.war

echo "Removing Keys"
rm -rf Inner-Satisfaction.pem