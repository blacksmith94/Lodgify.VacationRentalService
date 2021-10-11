# Lodgify Vacation Rental Service
This repository is a technical test for Lodgify.

Please take a look at the original test README to see the requested functionality: https://github.com/lodgify/public-money-recruitment-be-test

The project is a micro-service RESTful API built in [.NET Core 5.0](https://docs.microsoft.com/es-es/aspnet/core/?view=aspnetcore-5.0) that will let lessors and lessees manage bookings and rentals

Following [Domain Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design) architecture and [SOLID](https://en.wikipedia.org/wiki/SOLID) principles.

Used libraries:
- Entity Framework
- Fluent Validation
- Automapping
- Autofac IoC container.

The API can be served using [Docker](https://docs.docker.com/get-started/overview/) containers.

# Clone, Build and Run 
Clone the project:
* `git clone https://github.com/blacksmith94/public-money-recruitment-be-test.git`

Execute this command from the root of the project:
* `docker-compose build`

The build pipeline is configured in a way that when running the above build command, firstly it will build and run the [Integration](https://en.wikipedia.org/wiki/Integration_testing) and [Unit](https://en.wikipedia.org/wiki/Unit_testing) tests, if they do not pass, the API won't be built and it will show where did the tests fail.

Serve api:
* `docker-compose up api`

Run integration and unit tests:
* `docker-compose up test`

The API will be served at http://localhost:9981/api/v1/

## OpenAPI Specification

The project includes API documentation with [Swagger](https://swagger.io/).

Served at http://localhost:9981/swagger
