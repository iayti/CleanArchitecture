namespace Application.Cities.Queries.GetCities
{
    using System.Collections.Generic;
    using AutoMapper;

    using Common.Mappings;
    using Domain.Entities;

    public class DistrictDTO : IMapFrom<District>
    {
        public DistrictDTO()
        {
            Villages = new List<VillageDTO>();
        }
        public int Id { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string Name { get; set; }

        public IList<VillageDTO> Villages { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<District, DistrictDTO>();
        }
    }
}
