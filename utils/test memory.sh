#! /bin/bash

while :
do


    echo "$(ps -p 99221 v | grep "WorldGenerator" | awk '{ print $8 }')" >> test.txt
    sleep 1s
done
