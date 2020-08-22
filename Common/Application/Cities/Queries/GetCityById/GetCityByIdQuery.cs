namespace Application.Cities.Queries.GetCityById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MediatR;
    using Mapster;
    using MapsterMapper;

    using Dto;
    using Common.Interfaces;
    using Common.Models;
    
    public class GetCityByIdQuery : IRequest<ServiceResult<CityDto>>
    {
        public int CityId { get; set; }
    }

    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, ServiceResult<CityDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCityByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CityDto>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            CityDto city = await _context.Cities
                .Where(x => x.Id == request.CityId)
                .Include(d => d.Districts)
                .ThenInclude(v => v.Villages)
                .ProjectToType<CityDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return city != null ? ServiceResult.Success(city) : ServiceResult.Failed<CityDto>(ServiceError.NotFount);
        }
    }
}
