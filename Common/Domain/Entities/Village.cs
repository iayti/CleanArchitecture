namespace Domain.Entities
{
    using System.Collections.Generic;
    using Common;

    public class Village : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

    }
}
