#!/bin/sh

host="$1"
shift
cmd="$@"

until nc -z -v -w30 $host 5672
do
  echo "Waiting for RabbitMQ ($host) to start..."
  sleep 1
done

echo "RabbitMQ is up - executing command"
exec $cmd
