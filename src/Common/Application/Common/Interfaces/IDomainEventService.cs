namespace Application.Common.Interfaces
{
    using System.Threading.Tasks;
    using Domain.Common;

    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
