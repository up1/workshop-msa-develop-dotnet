using MediatR;

namespace Fitness.Api.Common.Events
{
    internal interface IIntegrationEvent : INotification
    {
        Guid Id { get; }
        DateTimeOffset OccurredDateTime { get; }
    }
};
