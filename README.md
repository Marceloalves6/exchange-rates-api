# Exchange Rates API

## Introduction

This application was developed using dotnet 8 and its main objective is to manage exchange rates through a REST API.
The API provides 4 endpoints that allow its clients to perform CRUD (Create, Read, Update and Delete) operations.

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

## Solution

The application solution consists of 4 projects that will be detailed below::

### ExchangeRates.Api

This is a Web API project and its main resposability is to prove a compoent that allows receive and response http requests.

### ExchangeRates.Core

This project houses the core business logic. It does not reference any other projects, but it is referenced by others. Contract classes and interfaces for 
repositories and services, which define communication with databases and external services, are also contained within this project. 
It is important to highlight that this project remains completely agnostic to data access and external comunication implementation.

### ExchangeRates.Infra

The core resposability of this project is to provide comunication with with databases, message queues and external services. Unlike the **ExchangeRates.Core**,
this project keeps the implementation necessary  
