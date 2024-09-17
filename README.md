# Larder

Larder is a free software application for tracking your personal inventories of foods and ingredients; as you cook and eat them, Larder will track some nutritional statistics for you.

## client

React app; run from `client` directory with `npm start`

## WebApi

ASP.NET Core project; run from `WebApi` directory with `dotnet run`

Install the dotnet-ef CLI tool globally to scaffold migrations from model classes:

```
dotnet tool install --global dotnet-ef
```

### Architecture and Design

The frontend is a React app that was created with Create React App. The backend is a monolithic ASP.NET Core project with a controller-service-repository architecture.

#### Authentication

The backend uses cookie-based authentication with ASP.NET Core Identity. Since React is being used without a framework, the client has a React context called `AuthedContext` to track the user authentication state client side. The initial test of whether the client is authenticated is a fetch call to the units index endpoint. Since units are used throughout the app (like for ingredient quantities), there is a React context `UnitsContext`, so this has the dual purpose of checking if there are valid cookies and setting up the client auth context.

#### Controllers, services, repository

Controllers bind incoming HTTP request data into data transfer objects (DTOs), invoke services, and return appropriate responses, with the preferred return type `Task<ActionResult<DataTransferObject>>`. The abstract controller `ApplicationControllerBase` establishes the convention for the base resource URL (`api/[controllername]`), uses `[ApiController]` so all requests that do not successfully bind data into valid DTOs (the parameters of the controller actions) send 400 responses automatically, and `[Authorize]` to require the user be authenticated, sending 401 responses if not.

Services compose the business logic layer. The abstract `ApplicationServiceBase` provides `CurrentUserId()` which accesses the HTTP context to get the user id.

Database access is done through the repositories.

### Style guide

#### Bootstrap

- Prefer `btn-primary` for `<Link>` buttons that change the page and `type="submit"` buttons that redirect
- Prefer `btn-secondary` for `type="button"` buttons that change the state of the current page component