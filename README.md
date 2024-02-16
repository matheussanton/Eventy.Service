# Eventy Service

This project is developed using C# and .NET 8, focusing on backend development with a WebAPI architecture. It leverages the EF for efficient data management.
This was built with the purpose to be a simple project that can grow in difficulty as you add more architecture concepts and components

## Technologies Used
- **Language:** C#
- **Framework:** .NET 8
- **Architecture:** DDD
- **ORM:** Entity Framework

## Features
- CRUD Events
- Notify Event Participants by e-mail
- Deny/Accept Invites
- Accept by email link
- Auth w/ JWT

## Getting Started
You can run this project from it's compose, the compose depends on an image for frontend, you can find it here (https://github.com/matheussanton/Eventy.WebApp)

After cloning those two repos, build each one with docker and use the docker-compose.yml here as a reference, feel free to improve or edit it.

``` docker build -t eventy.service . ```

## License
MIT
