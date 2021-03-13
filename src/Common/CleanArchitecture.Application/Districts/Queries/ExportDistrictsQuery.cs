using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.Dto;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Districts.Queries
{
    [Authorize(Roles = "Administrator")]
    public class ExportDistrictsQuery : IRequest<ExportDto>
    {
        public int CityId { get; set; }
    }

    public class ExportDistrictsQueryHandler : IRequestHandler<ExportDistrictsQuery, ExportDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportDistrictsQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportDto> Handle(ExportDistrictsQuery request, CancellationToken cancellationToken)
        {
            var result = new ExportDto();

            var records = await _context.Districts
                .Where(t => t.CityId == request.CityId)
                .ProjectToType<DistrictDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            result.Content = _fileBuilder.BuildDistrictsFile(records);
            result.ContentType = "text/csv";
            result.FileName = "Districts.csv";

            return await Task.FromResult(result);
        }
    }
}
