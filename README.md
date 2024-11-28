# Larder

Larder is a free software application (that is a work in progress) for tracking your personal inventories of foods and ingredients; as you cook and eat them, Larder will track some nutritional statistics for you. 

## larder-client

Typescript React vite app; run from `larder-client` directory with `npm run dev`

## Larder

ASP.NET Core project; run from `Larder` directory with `dotnet run`

Install the dotnet-ef CLI tool globally to scaffold migrations from model classes:

```
dotnet tool install --global dotnet-ef
```

This project has a controller-service-repository architecture; all database access is done through the repositories, the services handle most of the domain logic, and the controllers parse query parameters, bind incoming data into the Data Transfer Object (DTO) types, and handle exceptions with appropriate status codes.

## Larder.Tests

XUnit test project for unit tests of `Larder` where the repository interface is mocked

## Larder.UITests

NUnit Selenium WebDriver test project for end-to-end browser testing of `larder-client` and `Larder`; both must be running for this to test against