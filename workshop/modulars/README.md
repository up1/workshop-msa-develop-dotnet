# Workshop :: .Net 8 modular
* Project structure
* .NET 8
* Database with PostgreSQL 16
* Using Entity Framework

## Create solution and project
```
$dotnet new globaljson
$dotnet new sln -n fitness
$mkdir src

# API
$dotnet new webapi -o src/Fitness.Api
$dotnet sln add src/Fitness.Api/Fitness.Api.csproj

# Integration test with XUnit
$dotnet new xunit -o src/Fitness.IntegrationTests
$dotnet sln add src/Fitness.IntegrationTests/Fitness.IntegrationTests.csproj
```

## Try to run
```
$dotnet restore
$dotnet test
$dotnet publish
```

## Install packages
```
$dotnet add package Microsoft.EntityFrameworkCore
$dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

## Create [migration with EFCore](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
```
$dotnet add package Microsoft.EntityFrameworkCore.Design
$dotnet ef migrations add InitialCreate -n Passes.Data.Migrations
```

## Run with Docker compose

Buid api with .NET 8
```
$cd src/
$docker compose build api
```

Start database
```
$docker compose up -d postgres
$docker compose ps
NAME       IMAGE         COMMAND                  SERVICE    CREATED          STATUS                    PORTS
postgres   postgres:16   "docker-entrypoint.s…"   postgres   11 seconds ago   Up 11 seconds (healthy)   0.0.0.0:5432->5432/tcp
```

Start API
```
$docker compose up -d api
$docker compose ps
```

List of URLs
* Swagger :: http://localhost:8080/swagger/index.html
* Passes APIs
    * Get all passes :: http://localhost:8080/api/passes


## Delete all
```
$docker compose down

$docker volume prune
$docker system prune
```