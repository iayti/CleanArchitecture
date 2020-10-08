 # Clean Architecture Template

This is a solution template for creating a ASP.NET Core Web API following the principles of Clean Architecture. Create a new project based on this template by clicking the above **Use this template** button or by installing and running the associated NuGet package (see Getting Started for full details). 


## Technologies
* .NET Core 3.1.x
* ASP .NET Core 3.1.x
* Entity Framework Core 3.1.x
* MediatR
* Mapster
* FluentValidation
* NUnit, FluentAssertions, Moq & Respawn
* Elasticsearch, Serilog, Kibana

## Getting Started

Install the [NuGet package](https://www.nuget.org/packages/Matech.Clean.Architecture.Template) and run `dotnet new cas`:

1. Install the latest [.NET Core SDK](https://dotnet.microsoft.com/download)
2. Run `dotnet new --install Matech.Clean.Architecture.Template` to install the project template
3. Create a folder for your solution and cd into it (the template will use it as project name)
4. Run `dotnet new cas` to create a new project
5. Navigate to `src/Apps/WebApi` and run `dotnet run` to launch the back end (ASP.NET Core Web API)
6. Open web browser https://localhost:5001/api Swagger UI


### Database Configuration

The template is configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

If you would like to use SQL Server, you will need to update **WebApi/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Common/Infrastructure` (optional if in this folder)
* `--startup-project src/Apps/WebApi`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Common\Infrastructure --startup-project src\Apps\WebApi --output-dir Persistence\Migrations`

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebApi

This layer is a web api application based on ASP.NET Core 3.1.x. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

### Logs

Logging into Elasticsearch using Serilog and viewing logs in Kibana.

#### Prerequisites

* Download and Install [Docker Desktop](https://www.docker.com/products/docker-desktop)

Open CLI in the project folder and run the below comment. 

```powershell
PS CleanArchitecture> docker-compose up
```
`docker-compose.yml` pull and run the ElasticSearch and Kibana images.

If you are running first time Windows 10 [WSL 2 (Windows Subsystem for Linux)](https://docs.microsoft.com/en-us/windows/wsl/install-win10) Linux Container for Docker, You will probably get the following error from the docker.

`Error:` max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144]

`Solution:` Open the Linux WSL 2 terminal `sudo sysctl -w vm.max_map_count=262144` and change the virtual memory for Linux.

## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/iayti/CleanArchitecture/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).

