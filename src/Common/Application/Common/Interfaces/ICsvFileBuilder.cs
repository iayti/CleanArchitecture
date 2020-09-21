namespace Application.Common.Interfaces
{
    using System.Collections.Generic;
    using Dto;

    public interface ICsvFileBuilder
    {
        byte[] BuildDistrictsFile(IEnumerable<DistrictDto> districts);
    }
}
