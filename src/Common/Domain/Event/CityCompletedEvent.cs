namespace Domain.Event
{
    using Common;
    using Entities;

    public class CityCompletedEvent : DomainEvent
    {
        public CityCompletedEvent(City city)
        {
            City = city;
        }

        public City City { get; }
    }
}
