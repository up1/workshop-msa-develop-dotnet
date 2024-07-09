


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
$dotnet add package Microsoft.EntityFrameworkCore.SqlServer
$dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```