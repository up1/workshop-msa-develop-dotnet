﻿using Fitness.Api.Passes.Data;

namespace Fitness.Api.Passes;

internal static class PassesModule
{
    internal static IServiceCollection AddPasses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        return services;
    }

    internal static IApplicationBuilder UsePasses(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();
        return applicationBuilder;
    }
}
