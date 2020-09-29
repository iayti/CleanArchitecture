namespace Application.Cities.EventHandler
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Models;
    using Domain.Event;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CityCreatedEventHandler : INotificationHandler<DomainEventNotification<CityCreatedEvent>>
    {
        private readonly ILogger<CityCompletedEventHandler> _logger;

        public CityCreatedEventHandler(ILogger<CityCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<CityCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
