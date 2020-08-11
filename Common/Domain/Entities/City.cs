namespace Domain.Entities
{
    using System.Collections.Generic;

    using Common;

    public class City : BaseEntity
    {
        public City()
        {
            Districts = new List<District>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public IList<District> Districts { get; set; }
    }
}
