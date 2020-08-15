namespace Application.Cities.Queries.GetCities
{
    using System.Collections.Generic;
    using AutoMapper;

    using Common.Mappings;
    using Domain.Entities;

    public class CityDTO :IMapFrom<City>
    {
        public CityDTO()
        {
            //Districts = new List<DistrictDTO>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        //public IList<DistrictDTO> Districts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityDTO>()
                .ForMember(x=>x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
