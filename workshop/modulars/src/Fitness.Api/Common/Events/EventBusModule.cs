namespace Fitness.Api.Common.Events;
using System.Reflection;
using Fitness.Api.Common.Events.InMemory;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
}
