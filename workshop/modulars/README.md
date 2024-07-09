


## Create solution and project
```
$dotnet new globaljson
$dotnet new sln -n fitness
$mkdir src

# API
$dotnet new webapi -o src/Fitness.Api
$dotnet sln add src/Fitness.Api/Fitness.Api.csproj

# Integration test
$dotnet new webapi -o src/Fitness.Api
$dotnet sln add src/Fitness.Api/Fitness.Api.csproj
```