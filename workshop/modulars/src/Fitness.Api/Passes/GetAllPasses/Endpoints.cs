using Fitness.Api.Passes.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Api.Passes.GetAllPasses;

internal static class GetAllPassesEndpoint
{
    internal static void MapGetAllPasses(this IEndpointRouteBuilder app) =>
        app.MapGet("/api/passes", async (ILogger<GetAllPassesResponse> logger, PassesPersistence persistence, CancellationToken cancellationToken) =>
            {
                logger.LogInformation("Getting all passes");
                var passes = await persistence.Passes
                    .AsNoTracking()
                    .Select(passes => PassDto.From(passes))
                    .ToListAsync(cancellationToken);
                var response = GetAllPassesResponse.Create(passes);

                return Results.Ok(response);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Returns all passes that exist in the system",
                Description =
                    "This endpoint is used to retrieve all existing passes.",
            })
            .Produces<GetAllPassesResponse>()
            .Produces(StatusCodes.Status500InternalServerError);
}
