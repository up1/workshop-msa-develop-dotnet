using Fitness.Api.Contracts.SignContract;

namespace Fitness.Api.Contracts;

internal static class ContractEndpoints
{
    internal static void MapContracts(this IEndpointRouteBuilder app)
    {
        app.MapSignContract();
    }
}
