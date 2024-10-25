
# ASP.NET - Ports-And-Adapters / Hexagonal Architecture with DDD

## Overview

This project, developed as a practice exercise to gain familiarity with C# and ASP.NET, follows **Hexagonal Architecture** (also known as Ports-and-Adapters Architecture) alongside **Domain-Driven Design** (DDD) principles. 
The project provides a modular, clean, and testable structure, focusing on:
- Customer registration and JWT-based authentication
- Displaying customer's personal details
- Modifying personal details
- Changing password
- Changing email address

Despite its simplicity, this project demonstrates a modern approach to ASP.NET architecture.

| Build Status                                                                                           | License                                                                                           |
| ------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------- |
| [![Build Status](https://img.shields.io/github/actions/workflow/status/hirannor/hexagonal-architecture-asp-net-core/.github/workflows/dotnet.yml)](https://github.com/hirannor/hexagonal-architecture-asp-net-core/actions/workflows/dotnet.yml) | [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) |

---

## Prerequisites for Development

- **.NET 8.0 SDK**: Latest .NET SDK for optimal performance and security
- **Visual Studio or VS Code**: IDEs that support .NET development, such as Visual Studio 2022 or VS Code with C# extensions
- **Docker**: For containerized testing, especially with Testcontainers for integration testing
- **SQL Server**: Required as the primary database for persistence

## Technology Stack

- **.NET 8.0**: The latest .NET platform, offering performance enhancements and new features
- **ASP.NET Core**: Framework for building the REST API and handling HTTP requests
- **Entity Framework Core**: ORM for database interactions with SQL Server
- **MailKit**: SMTP email library for user notifications
- **XUnit & Moq**: Testing frameworks for unit and integration testing
- **Testcontainers**: For Docker-based integration testing with SQL Server
- **Fluent Assertions**: Provides a fluent syntax for assertions in unit tests

---

## Adapter Configuration

Following Hexagonal Architecture principles, this project is highly modular and configurable. You can specify the adapters for various application layers in `appsettings.json` or `appsettings.Development.json`.

### Sample Configuration

```json
{
  "Adapter": {
    "Web": "Rest",
    "Authentication": "AspNetIdentity",
    "Persistence": "EntityFramework",
    "Messaging": "EventBus",
    "Notification": "Email"
  }
}
```

### Available Adapters

- **Authentication**:
  - AspNetIdentity (manages authentication and roles using ASP.NET Identity)
- **Persistence**:
  - EntityFramework (primary storage with SQL Server)
  - InMemory (demonstration adapter, not fully implemented)
- **Notification**:
  - Email (SMTP notifications via MailKit)
  - Mock (for testing)
- **Messaging**:
  - EventBus (event-driven communication)
- **Web**:
  - Rest (enables REST API for user interaction)

#### Important Notes:

- **Persistence Adapter**: Ensure to provide a valid SQL Server connection string for `DefaultConnection`.

  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "<YOUR_CONNECTION_STRING>"
    }
  }
  ```

- **Email Adapter**: Configure SMTP details for sending emails.

  ```json
  {
    "EmailSettings": {
      "SmtpServer": "smtp.yourprovider.com",
      "Port": 587,
      "Username": "your-email@example.com",
      "Password": "your-email-password",
      "FromEmail": "your-email@example.com"
    }
  }
  ```

---

## API Documentation

For detailed API exploration, you can access **Swagger UI** at [http://localhost:5194/swagger/index.html](http://localhost:5194/swagger/index.html).

### Available Endpoints

#### **Registration**

Registers a new user with the system.

```
curl -X 'POST'   'http://localhost:5194/api/register'   -H 'accept: application/json'   -H 'Content-Type: application/json'   -d '{
    "username": "mockuser",
    "emailAddress": "mock.user@local.com",
    "password": "#TestPassword123",
    "firstName": "Mock",
    "lastName": "User",
    "birthOn": "1990-01-01"
  }'
```

#### **Authentication**

Authenticates a user and returns a JWT token.

```
curl -X 'POST'   'http://localhost:5194/api/auth'   -H 'accept: application/json'   -H 'Content-Type: application/json'   -d '{
    "username": "mockuser",
    "password": "#TestPassword123"
  }'
```

#### **Get Personal Details**

Fetches the details of a specific user.

```
curl -X 'GET'   'http://localhost:5194/api/customers/{username}'   -H 'accept: application/json'   -H 'Authorization: Bearer {your_jwt_token}'
```

#### **Change Personal Details**

Updates a user's personal information.

```
curl -X 'PATCH'   'http://localhost:5194/api/customers/{username}'   -H 'accept: application/json'   -H 'Authorization: Bearer {your_jwt_token}'   -H 'Content-Type: application/json'   -d '{
    "address": {
      "street": {
        "streetName": "Main Street",
        "streetNumber": "123"
      },
      "postalCode": "12345",
      "city": "Sample City",
      "country": "Sample Country"
    }
  }'
```

#### **Change Email Address**

Changes the user’s registered email address.

```
curl -X 'PUT'   'http://localhost:5194/api/customers/{username}/email-address'   -H 'accept: application/json'   -H 'Authorization: Bearer {your_jwt_token}'   -H 'Content-Type: application/json'   -d '{
    "oldEmailAddress": "old.user@localhost.com",
    "newEmailAddress": "new.user@localhost.com"
  }'
```

#### **Change Password**

Updates a user's password.

```
curl -X 'PUT'   'http://localhost:5194/api/customers/{username}/password'   -H 'accept: application/json'   -H 'Authorization: Bearer {your_jwt_token}'   -H 'Content-Type: application/json'   -d '{
    "oldPassword": "#TestPassword123",
    "newPassword": "#NewPassword456"
  }'
```

---

## Pre-defined Customer Credentials

| Username        | Password          | Email Address               |
|-----------------|-------------------|-----------------------------|
| janedoe         | #TestPassword123  | jane.doe@localhost.com      |
| michaeljackson  | #TestPassword123  | michael.jackson@localhost.com |
| sarahconnor     | #TestPassword123  | sarah.connor@localhost.com  |
| willsmit        | #TestPassword123  | will.smith@localhost.com    |
| emilyblunt      | #TestPassword123  | emily.blunt@localhost.com   |

---

## Development and Contribution

To run the project locally, ensure SQL Server is configured, Docker is running for Testcontainers, and the required connection strings and email settings are properly set. Contributions are welcome to further improve this example. Please follow the coding standards, and consider submitting issues or feature requests if you notice areas of improvement.
