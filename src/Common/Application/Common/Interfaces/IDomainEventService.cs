using Domain.Common;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
