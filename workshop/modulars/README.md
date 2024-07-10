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
$dotnet format
$dotnet restore
$dotnet test
$dotnet publish
```

## Install packages
```
$dotnet add package Microsoft.EntityFrameworkCore
$dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

## Install Opentelemetry
```
# Automatic tracing, metrics
$dotnet add package OpenTelemetry.Extensions.Hosting

# Telemetry data exporter
$dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol

# Instrumentation packages
$dotnet add package OpenTelemetry.Instrumentation.Http
$dotnet add package OpenTelemetry.Instrumentation.AspNetCore
$dotnet add package Npgsql.OpenTelemetry
$dotnet add package OpenTelemetry.Exporter.Console

$dotnet add package OpenTelemetry.Instrumentation.EntityFrameworkCore --prerelease
$dotnet add package OpenTelemetry.Instrumentation.StackExchangeRedis --prerelease
```

## Create [migration with EFCore](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
```
$dotnet add package Microsoft.EntityFrameworkCore.Design
$dotnet ef migrations add InitialCreate -n Passes.Data.Migrations  --context PassesPersistence -v
dotnet ef migrations add InitialCreate -n Contracts.Data.Migrations --context ContractsPersistence -v
```

## Install [MediatR](https://github.com/jbogard/MediatR)
* In-process messaging with no dependencies.
```
$dotnet add package MediatR
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
* Swagger :: http://localhost:9000/swagger/index.html
* Passes APIs
    * Get all passes :: http://localhost:9000/api/passes


## Start Opentelemetry Collector (LGTM stack)
* [OpenTelemetry wity .NET](https://opentelemetry.io/docs/languages/net/getting-started/)
* Log => Loki
* Grafana
* Tracing => Tempo
* Metric => Prometheus

```
$docker compose up -d otel-collector
$docker compose ps
NAME                   IMAGE                     COMMAND                  SERVICE          CREATED          STATUS                    PORTS
src-otel-collector-1   grafana/otel-lgtm:0.6.0   "/bin/sh -c ./run-al…"   otel-collector   11 seconds ago   Up 11 seconds (healthy)   0.0.0.0:3000->3000/tcp, 0.0.0.0:4317-4318->4317-4318/tcp
```

Access to Grafana dashboard
* http://localhost:3000/


## API gateway with [Kong](https://konghq.com/products/kong-gateway)
* Kong with DB-less mode

```
$docker compose up -d kong
$docker compose ps
NAME      IMAGE               COMMAND                  SERVICE   CREATED          STATUS                    PORTS
kong      kong:3.7.1-ubuntu   "/docker-entrypoint.…"   kong      12 seconds ago   Up 11 seconds (healthy)   0.0.0.0:8000-8002->8000-8002/tcp, 0.0.0.0:8100->8100/tcp, 8443-8444/tcp
```

Kong dashboard
* http://localhost:8002

Access to demo-service from API gateway
* http://localhost:8000/s1/api/passes

Application metric
* http://localhost:8100/metrics

Access to Grafana dashboard again
* http://localhost:3000/

## Keep log of Kong and demo-service
```
$docker compose up -d fluentbit
$docker compose ps
```

Access to demo-service from API gateway
* http://localhost:8000/s1/api/passes

Access to Grafana dashboard again
* http://localhost:3000/

## Delete all
```
$docker compose down

$docker volume prune
$docker system prune
```