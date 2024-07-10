﻿using Fitness.Api.Common.Events;
using Fitness.Api.Contracts.RegisterPass.Events;
using Fitness.Api.Passes;
using Fitness.Api.Passes.Data;

namespace Fitness.Api.Contracts.SignContract.Events;
internal sealed class ContractSignedEventHandler(
    PassesPersistence persistence,
    IEventBus eventBus) : IIntegrationEventHandler<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine("Passes module :: Received ContractSignedEvent");
        Console.WriteLine($"Handling {@event.GetType().Name} with ID: {@event.Id}");

        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        await persistence.Passes.AddAsync(pass, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}
