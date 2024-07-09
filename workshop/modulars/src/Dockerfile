FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Fitness.Api/Fitness.Api.csproj Fitness.Api/
RUN dotnet restore Fitness.Api/Fitness.Api.csproj
COPY . .
WORKDIR /src/Fitness.Api
RUN dotnet build Fitness.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Fitness.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fitness.Api.dll"]