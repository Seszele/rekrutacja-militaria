# RabbitMQ Email System

This is a simple email system built using RabbitMQ, .NET 6, and Mailhog for testing. It consists of a .NET Core console application that acts as both the producer and consumer, RabbitMQ as the message broker, and Mailhog as a mock SMTP server for testing email sending.

## Requirements

- Docker
- Docker Compose

## Setup

1. Clone the project and navigate to the project directory.

2. Build the Docker images:

```bash
docker-compose build
```

3. Run the Docker containers:

```bash
docker-compose up
```

## Accessing the RabbitMQ Management Console and Mailhog

Once the Docker containers are running, you can access the RabbitMQ Management Console and Mailhog web interface using the following URLs:

- RabbitMQ Management Console: http://localhost:15672
  - Default username: `guest`
  - Default password: `guest`
- Mailhog: http://localhost:8025

You can view the emails sent by the console application in the Mailhog web interface and monitor the message queue in the RabbitMQ Management Console.
