#!/bin/bash

echo "Decrypting SSH Keys"
openssl enc -d -aes-256-cbc -in "Inner-Satisfaction.pem.enc" -out "Inner-Satisfaction.pem" -pass pass:$SSH_KEY_PASSWORD

echo "Changing Permissions for PEM Keys"
sudo chmod 400 Inner-Satisfaction.pem.enc

## Server Commands Start
echo "SSH to Server"
ssh -i Inner-Satisfaction.pem ubuntu@34.242.122.236

echo "Exiting Server"
logout
## Server Commands End

echo "Removing Keys"
rm Inner-Satisfaction.pem