﻿namespace Fitness.Api.Passes.Data;

using Microsoft.EntityFrameworkCore;

internal static class AutomaticMigrationsExtensions
{
    internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PassesPersistence>();
        context.Database.Migrate();

        return applicationBuilder;
    }
}
