using System.Globalization;
using Application.Dto;
using CsvHelper.Configuration;

namespace Infrastructure.Files.Maps
{
    public sealed class DistrictMap : ClassMap<DistrictDto>
    {
        public DistrictMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Villages).ConvertUsing(_ => "");
        }
    }
}
