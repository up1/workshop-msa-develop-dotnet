namespace Fitness.Api.Passes;

using GetAllPasses;

internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {
        app.MapGetAllPasses();
    }
}
