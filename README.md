# ASP.NET - Ports-And-Adapters / Hexagonal Architecture with DDD


## Overview
The project was created purely for practice, helping me become more familiar with C# and ASP.NET after 10 years of Java development. It doesn't have any specific business value and may contain missing elements or mistakes. The domain is simple, supporting only user registration, authentication, and basic user detail management. It showcases an ASP.NET application that implements the port-and-adapter (hexagonal) architecture along with Domain-Driven Design (DDD), focusing on clean architecture principles to facilitate a modular approach, separating concerns for easier maintenance and testing.

|Build Status|License|
|------------|-------|
|[![Build Status](https://img.shields.io/github/actions/workflow/status/hirannor/hexagonal-architecture-asp-net-core/.github/workflows/dotnet.yml)]([https://github.com/hirannor/springboot-hexagonal-ddd/actions/workflows/maven.yml](https://github.com/hirannor/hexagonal-architecture-asp-net-core/blob/main/.github/workflows/dotnet.yml))|[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)|


## Adapter configuration

In the appsettings.json or appsettings.Development.json file, you can configure which adapter will be used by the application on startup.

```JSON
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

Currently available adapters for configuration:
- Authentication:
  - AspNetIdentity
- Persistence:
  - EntityFramework
  - InMemory (not fully implemented; only the base classes have been created for demonstration purposes)
- Notification:
  - Email
  - Mock
- Messaging
  - EventBus
- Web
  - Rest 
