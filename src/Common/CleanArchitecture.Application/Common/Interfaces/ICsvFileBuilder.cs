using System.Collections.Generic;
using CleanArchitecture.Application.Dto;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildDistrictsFile(IEnumerable<DistrictDto> districts);
    }
}
