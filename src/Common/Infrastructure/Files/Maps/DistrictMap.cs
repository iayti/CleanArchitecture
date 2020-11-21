using Application.Dto;
using CsvHelper.Configuration;
using System.Globalization;

namespace Infrastructure.Files.Maps
{
    public class DistrictMap : ClassMap<DistrictDto>
    {
        public DistrictMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Villages).ConvertUsing(c => "");
        }
    }
}
