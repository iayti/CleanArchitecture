namespace Infrastructure.Files.Maps
{
    using System.Globalization;
    
    using CsvHelper.Configuration;

    using Application.Dto;

    public class CityMap : ClassMap<CityDto>
    {
        public CityMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.CreateDate).ConvertUsing(c => c.CreateDate != "" ? $"Date : {c.CreateDate}" : "");
        }
    }
}
