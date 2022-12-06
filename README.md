![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

# Introduction

OatShop is an online store that sells oat products.

# Components

The solution is comprised of 4 client components:
1. OatShop API is a REST API that allows for client applications to interact with the OatShop system.  Provides data access and back-end processing.
2. OatShop Web is a client-side SPA website to interface with the system.
3. OatShop WebAdmin is a server-side SPA website to provide administration functionality for the system.
4. OatShop Tests is a set of unit tests and integration tests to automate testing of the system components.

# Tech Stack

1. .NET 6.0 (for Web API, Blazor WebAssembly, Blazor Server and libraries)
2.  Entity Framework Core 6.0 (for ORM)
3. MySQL 8.0 (for database)
4. Swagger (API documentation)
5. Serilog (logging)
6. Azure (for cloud hosting)
7. MSTest2 (for unit & integration testing)

_Note: no .NET Standard 2.1 or .NET Standard 2.0._

# Problem Statement

We need to practice the use of Dependency Injection pattern (DI) and Inversion of Control Container (IoC) frameworks.

This project demostrates the .NET Core built-in IoC Container to manage the complete object creation and its lifetime and dependency injection (automatically injects dependencies to the class).

This project also demostrates the use of clean architecture layered software design patterns.

A solution called OatShop will be developed to perform the required operations.

# Code

```csharp
src\

    LiteBulb.OatShop.Web (Blazor WASM SPA)
    LiteBulb.OatShop.WebAdmin (Blazor Server SPA)
    LiteBulb.OatShop.Api (ASP.NET WebAPI REST)
	LiteBulb.OatShop.Domain (DTOs, enumerations, DTO extension methods)
    LiteBulb.OatShop.Application (Business logic)
    LiteBulb.OatShop.Infrastructure (Entities, database and external services)
    LiteBulb.OatShop.Infrastructure.Migrations (Strictly for EF Core migrations)
	LiteBulb.OatShop.Shared (Project to share common code between repos - could become NuGet package)
	LiteBulb.OatShop.Infrastructure.Shared (Project to share common code between repos - could become NuGet package)

test\

    LiteBulb.OatShop.Domain.Tests (Unit tests)
	LiteBulb.OatShop.Application.Tests (Unit & Integration tests)
    LiteBulb.OatShop.Infrastructure.Tests (Unit & Integration tests)
	LiteBulb.OatShop.Shared.Tests (Unit tests)
```

_Note: See below for links to code repositories._

# GitHub

- [OatShop Project](https://github.com/users/MrJohnB/projects/2)
- [OatShop Repository](https://github.com/MrJohnB/OatShop)

# Build and Test

- Build the solution in Visual Studio 2022 and run.
- TODO: Describe and show how to build your code and run the tests.

# MSDN Documentation

[Dependency injection in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
[DbContext Lifetime, Configuration, and Initialization](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration)

# Contribute

- [OatShop Repository](https://github.com/MrJohnB/OatShop)

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)