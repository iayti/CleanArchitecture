namespace Application.Cities.Queries.GetCities
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Mapster;
    using MediatR;

    using Common.Interfaces;
    using MapsterMapper;

    public class GetAllCitiesQuery : IRequest<GetAllCitiesResponse>
    {

    }

    public class GetCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, GetAllCitiesResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAllCitiesResponse> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            return new GetAllCitiesResponse
            {
                Lists = await _context.Cities
                    .Include(x => x.Districts)
                    .ThenInclude(c => c.Villages)
                    .ProjectToType<CityDto>(_mapper.Config)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
