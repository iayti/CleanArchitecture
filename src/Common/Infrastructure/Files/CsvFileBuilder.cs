namespace Infrastructure.Files
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Application.Common.Interfaces;
    using Application.Dto;
    using CsvHelper;
    using Maps;

    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildCitiesFile(IEnumerable<CityDto> cities)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Configuration.RegisterClassMap<CityMap>();
                csvWriter.WriteRecords(cities);
            }

            return memoryStream.ToArray();
        }
    }
}
