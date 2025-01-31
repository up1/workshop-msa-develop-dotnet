﻿using Fitness.Api.Contracts.Data;

namespace Fitness.Api.Contracts;

internal static class ContractsModule
{
    internal static IServiceCollection AddContracts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        return services;
    }

    internal static IApplicationBuilder UseContracts(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();
        return applicationBuilder;
    }
}
