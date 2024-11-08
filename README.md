# Exchange Rates API

## Introduction

This application was developed using .NET 8, and its main objective is to manage exchange rates through a REST API. The API provides four endpoints that allow its clients to perform CRUD (Create, Read, Update, and Delete) operations.

## Solution

The application solution consists of 4 projects that will be detailed below:

### ExchangeRates.Api

This is a Web API project, and its main responsibility is to provide a component that handles receiving and responding to HTTP requests.

### ExchangeRates.Core

This project houses the core business logic. It does not reference any other projects, but it is referenced by others. Contract classes and interfaces for 
repositories and services, which define communication with databases and external services, are also contained within this project. 
It is important to highlight that this project remains completely agnostic to data access and external comunication implementation.

### ExchangeRates.Infra

The core responsibility of this project is to provide communication with databases, message queues, and external services. Unlike `ExchangeRates.Core`, this project includes the necessary implementations.

### ExchangeRates.Test

This project is where unit tests are created.

## Packages

The list below shows some of the main packages used in the implementation:
- AutoMapper
- Entity Framework
- FluentValidation
- FluentAssertion
- MediatR
- Newtonsoft.Json
- Moq
- RabbitMQ
- Refit
- Serilog
- Swagger
## Setup and Run the application

There two ways to get the application running, one that uses database in memory and mock message queue, and the other one where we use docker to run the dependencies (Postgres and RabbiMQ).

### Database in memory and message queue mock.

This is the quickest way to get the application running, just download the source code and open it with `Visual Studio 2022`, then run the project. As mentioned above, this strategy emulates 
the external dependencies to make the process simpler. The database will store data in memory and the message queue service will be mocked. Every time a new message is received by the message
queue, it will store the data in memory and log the message.

**Note**: Ensure that the `"MockExternalDependencies"` property in both `appsettings.Development.json` and `appsettings.json` files is set to `true`.

### Using docker to run the dependencies

There are two ways to use Docker to run the Exchange Rates API:
- Using Docker to run only the dependencies.
- Using Docker to run Exchange Rates API and the dependecies.

#### Using docker to run only the dependecies:
**Steps:**
- Download the source code into a folder of your choice.
- Open the terminal and navigate to the Project folder `/deploy-partial`.
- Now, execute the command `docker-compose up -d`. This will start the dependencies.
- Open the project in Visual Studio 2022.
- In the files `appsettings.Development.json` and `appsettings.json`, make sure the properties `MockExternalDependencies` is set as `false`
- Run the project

#### Using docker to run Exchange Rates API and the dependecies:
**Steps:**
 Download the source code into a folder of your choice.
- Open the terminal and navigate to the  `/src` folder.
- Now, run the command `Docker build . -t exchange-rates-api`, it will createa docker image of our project.
- Go to the `/deploy` folder, and then execute the command `docker-compose up -d`
- Open a browser of your choise and navegate to `http://localhost:5000/swagger/index.html`

#### Information:
Using Docker, you can access dependencies like `PostgreSQL` and `RabbitMQ` to validate the data.

**PgAdmin**: 
- Address: `http://localhost:5050/login?next=/`
- Username: `admin@example.com`
- Password: `admin`

**Postgres**:
- Database: `ExchangeDB`
- Username: `admin`
- Password: `admin`

**RabbitMQ**
- Address: `http://localhost:15672`
- Username: `guest`
- Password: `guest`
- Virtual Host: `/`
- Exhange: `exchange-rate-api`
- Queue: `exchange.rate.q.1`

## Logging

For logging it was used the package `serilog` that allows to register our logs in many different ways, but for this project 
logs will stored in text files, the rolling Interval is configurated to generate a new log file every day.

## Error handling

For error handling it was create a filter (`HttpExceptionHandlerFilter.cs`) in the API project that allows to
handler all exception in only one place.

## Validation

To validate the input data received in our API, we used the `FluentValidation` package combined with `MediatR` behavior (`ValidatorBehavior.cs`). 
This combination allows us to validate every single class that has a validation implemented before the request hits the command handler.   

## Limitations

This project has some limitations that can be addressed with additional work:

- The API currently lacks protection and can be freely consumed. To resolve this, I recommend implementing an authentication/authorization method.
- Logs are stored in text files. To improve this, I suggest using `Elasticsearch` to store logs.
- The `https://www.alphavantage.co` service doesn't provide exchange rates for all types of currency. Adding a second provider could make the application more consistent.
