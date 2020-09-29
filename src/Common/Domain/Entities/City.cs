namespace Domain.Entities
{
    using System.Collections.Generic;

    using Common;
    using Event;

    public class City : BaseEntity, IHasDomainEvent
    {
        public City()
        {
            Districts = new List<District>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public IList<District> Districts { get; set; }

        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value == true && _done == false)
                {
                    DomainEvents.Add(new CityCompletedEvent(this));
                }

                _done = value;
            }
        }

        public List<DomainEvent> DomainEvents { get; set; }
    }
}
