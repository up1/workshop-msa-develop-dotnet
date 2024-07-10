namespace Fitness.Api.Common.Events;

using MediatR;

internal interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent;
