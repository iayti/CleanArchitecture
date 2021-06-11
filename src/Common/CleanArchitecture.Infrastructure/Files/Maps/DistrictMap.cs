using System.Globalization;
using CleanArchitecture.Application.Dto;
using CsvHelper.Configuration;

namespace CleanArchitecture.Infrastructure.Files.Maps
{
    public sealed class DistrictMap : ClassMap<DistrictDto>
    {
        public DistrictMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Villages).Convert(_ => "");
        }
    }
}
