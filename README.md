# Exchange Rates API

## Introduction

This application was developed using dotnet 8 and its main objective is to manage exchange rates through a REST API.
The API provides 4 endpoints that allow its clients to perform CRUD (Create, Read, Update and Delete) operations.

## Solution

The application solution consists of 4 projects that will be detailed below:

### ExchangeRates.Api

This is a Web API project and its main resposability is to prove a compoent that allows receive and response http requests.

### ExchangeRates.Core

This project houses the core business logic. It does not reference any other projects, but it is referenced by others. Contract classes and interfaces for 
repositories and services, which define communication with databases and external services, are also contained within this project. 
It is important to highlight that this project remains completely agnostic to data access and external comunication implementation.

### ExchangeRates.Infra

The core resposability of this project is to provide comunication with with databases, message queues and external services. Unlike the **ExchangeRates.Core**;
this project keeps the implementation necessary.

### ExchangeRates.Test

This project is where the **Unit Tests** are created

## Packages

The list below shows some of the main packges used in the implemantaion:
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
## Setting up and Running the application

There 2 ways to get the application running, one that makes use of in memory database and mock message queue, and the other one where we use docker to run the dependencies (Postgres and RabbiMQ).

### Database in memory and mock message queue

This is the quick way to get the application running: just download the source code and open it in Visual Studio 2022, then run the project.

As mentioned above, it uses strategies to emulate the external dependencies. The database will store data in memory and the message queue service
will be mocked. Every time a new message is received by the message queue, it will store that in memory and the message data will be logged.
