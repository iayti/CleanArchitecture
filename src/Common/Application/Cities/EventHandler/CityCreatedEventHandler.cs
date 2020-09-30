namespace Application.Cities.EventHandler
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Event;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CityCreatedEventHandler : INotificationHandler<DomainEventNotification<CityCreatedEvent>>
    {
        private readonly ILogger<CityCompletedEventHandler> _logger;
        private readonly IEmailService _emailService;

        public CityCreatedEventHandler(ILogger<CityCompletedEventHandler> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Handle(DomainEventNotification<CityCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            if (domainEvent.City != null)
            {
                await _emailService.SendAsync(new EmailRequest
                {
                    Subject = domainEvent.City.Name + " is created.",
                    Body = "City created successfully.",
                    FromDisplayName = "Clean Architecture",
                    FromMail = "test@test.com",
                    ToMail = new List<string> { "to@test.com" }
                });
            }
        }
    }
}
