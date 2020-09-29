namespace Domain.Event
{
    using Common;
    using Entities;

    public class CityCreatedEvent : DomainEvent
    {
        public CityCreatedEvent(City city)
        {
            City = city;
        }

        public City City { get; }
    }
}
