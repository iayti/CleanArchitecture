namespace Application.Cities.Queries.GetCities
{
    using AutoMapper;
    using Common.Mappings;
    using Domain.Entities;

    public class VillageDTO :IMapFrom<Village>
    {
        public int Id { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Village, VillageDTO>();
        }
    }
}
