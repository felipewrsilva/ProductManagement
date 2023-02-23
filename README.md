# Product Management API

This API provides functionalities to manage products and their suppliers. The API is built with .NET 5.0, organized into layers following Domain-Driven Design (DDD) principles, and uses EntityFrameworkCore for data access. AutoMapper is used to map between entity and DTO (Data Transfer Object). The API also includes unit tests using NUnit.Framework and FluentAssertions.

## Functionalities

- Get a product by id
- List products with filtering and pagination
- Insert a new product
- Update a product
- Delete a product by marking it as Inactive

## Product Entity

- Id: unique and sequential identifier
- Description: non-null product description
- Situation: either Active or Inactive
- ManufactureDate: non-null DateTime
- ExpirationDate: non-null DateTime
- Supplier: reference to a supplier entity

## Supplier Entity

- Id: non-null supplier id
- Description: non-null supplier description
- Cnpj: non-null supplier CNPJ

## Components

- FluentValidation to validate the manufacturing and expiration dates when inserting and updating
- Web API built with .NET 5.0
- Code organized into layers and follow Domain-Driven Design (DDD) principles
- EntityFrameworkCore
- AutoMapper to map between entity and DTO (Data Transfer Object)
- Unit tests for the API using NUnit.Framework and FluentAssertions

## Structure

- Solution: ProductManagement
- Projects: Api, Application, Domain, Infrastructure, UnitTests

### Api
The Api project contains the presentation layer of the application. It includes controllers, models, and mapping profiles.

### Application
The Application project contains the business logic of the application. It includes services, commands, and queries.

### Domain
The Domain project contains the domain model of the application. It includes entities, value objects, and domain events.

### Infrastructure
The Infrastructure project contains the implementation details of the application. It includes data access, logging, and configuration.

### UnitTests
The UnitTests project contains the unit tests for the application. It includes tests for controllers, services, and repositories.

## Installation

To run the application, follow these steps:

1. Clone the repository
2. Build the solution
3. Update the connection string in the appsettings.json file in the Api project
4. Run the application

## Contributors

- Felipe Silva (https://github.com/felipewrsilva) - Maintainer

## License

This project is licensed under the MIT License.
