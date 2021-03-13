using System.Collections.Generic;
using CleanArchitecture.Domain.Entities;
using Mapster;

namespace CleanArchitecture.Application.Dto
{
    public class CityDto : IRegister 
    {
        public CityDto()
        {
            Districts = new List<DistrictDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CreateDate { get; set; }

        public bool Active { get; set; }

        public IList<DistrictDto> Districts { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<City, CityDto>()
            .Map(dest => dest.CreateDate,
                src => $"{src.CreateDate.ToShortDateString()}");
        }
    }
}
