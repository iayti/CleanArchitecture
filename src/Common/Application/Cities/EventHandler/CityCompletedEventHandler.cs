namespace Application.Cities.EventHandler
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Models;
    using Domain.Event;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CityCompletedEventHandler : INotificationHandler<DomainEventNotification<CityCompletedEvent>>
    {
        private readonly ILogger<CityCompletedEventHandler> _logger;

        public CityCompletedEventHandler(ILogger<CityCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<CityCompletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
