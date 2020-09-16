namespace Application.Common.Interfaces
{
    using System.Collections.Generic;
    using Dto;

    public interface ICsvFileBuilder
    {
        byte[] BuildCitiesFile(IEnumerable<CityDto> cities);
        byte[] BuildDistrictsFile(IEnumerable<DistrictDto> districts);
    }
}
