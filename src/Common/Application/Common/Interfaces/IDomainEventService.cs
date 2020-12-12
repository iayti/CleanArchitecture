using System.Threading.Tasks;
using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
