namespace Infrastructure.Files.Maps
{
    using System.Globalization;
    
    using CsvHelper.Configuration;

    using Application.Dto;

    public class DistrictMap : ClassMap<DistrictDto>
    {
        public DistrictMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Villages).ConvertUsing(c => "");
        }
    }
}
