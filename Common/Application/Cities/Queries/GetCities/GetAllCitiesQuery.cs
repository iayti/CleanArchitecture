namespace Application.Cities.Queries.GetCities
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;

    using Common.Interfaces;


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
            var list = _context.Cities;
                //.Include(x => x.Districts)
                //.ThenInclude(c => c.Villages);
                //.ToListAsync(cancellationToken: cancellationToken);
            GetAllCitiesResponse result = new GetAllCitiesResponse
            {
                Lists = await list
                    .ProjectTo<CityDTO>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };

            return new GetAllCitiesResponse
            {
                Lists = await _context.Cities
                    .Include(x => x.Districts)
                    .ThenInclude(c => c.Villages)
                    .ProjectTo<CityDTO>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
