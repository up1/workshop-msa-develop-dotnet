using Fitness.Api.Common.Events;
using Fitness.Api.Contracts.Data;
using Fitness.Api.Contracts.SignContract.Events;

namespace Fitness.Api.Contracts.SignContract;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app) => app.MapPost("/api/contracts/{id}",
            async (Guid id, SignContractRequest request,
                ContractsPersistence persistence,
                IEventBus bus,
                TimeProvider timeProvider,
                CancellationToken cancellationToken) =>
            {

                Console.WriteLine("Contracts Module :: SignContractEndpoint.MapSignContract");
                Console.WriteLine("id: " + id);

                // var contract =
                //     await persistence.Contracts.FindAsync([id], cancellationToken: cancellationToken);

                // if (contract is null)
                // {
                //     return Results.NotFound();
                // }

                var dateNow = timeProvider.GetUtcNow();
                var contract =Contract.Prepare(id, 0, 0, dateNow);
                contract.Sign(request.SignedAt, dateNow);
                await persistence.SaveChangesAsync(cancellationToken);

                var @event = ContractSignedEvent.Create(
                    contract.Id,
                    contract.CustomerId,
                    contract.SignedAt!.Value,
                    contract.ExpiringAt!.Value,
                    timeProvider.GetUtcNow());
                await bus.PublishAsync(@event, cancellationToken);

                return Results.NoContent();
            })
        // .ValidateRequest<SignContractRequest>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Signs prepared contract",
            Description = "This endpoint is used to sign prepared contract by customer."
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
