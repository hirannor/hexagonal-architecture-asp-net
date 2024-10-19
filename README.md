# ASP.NET - Ports-And-Adapters / Hexagonal Architecture with DDD


## Overview
This project showcases an ASP.NET application that implements the port-and-adapter (hexagonal) architecture along with Domain-Driven Design (DDD). The focus is on clean architecture principles, facilitating a modular approach that separates concerns, making it easier to maintain and test.

|Build Status|License|
|------------|-------|
|[![Build Status](https://img.shields.io/github/actions/workflow/status/hirannor/hexagonal-architecture-asp-net-core/.github/workflows/dotnet.yml)]([https://github.com/hirannor/springboot-hexagonal-ddd/actions/workflows/maven.yml](https://github.com/hirannor/hexagonal-architecture-asp-net-core/blob/main/.github/workflows/dotnet.yml))|[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)|


## Adapter configuration

In the appsettings.json or appsettings.Development.json file, you can configure which adapter will be used by the application on startup.

```JSON
{
  "Adapter": {
    "Persistence": "EntityFramework",
    "Messaging": "EventBus",
    "Notification": "Mock"
  }
}
```

Currently, only the Persistence adapter has two different implementations, which are as follows:
- EntityFramework
- InMemory (not fully implemented; only the base classes have been created for demonstration purposes)
