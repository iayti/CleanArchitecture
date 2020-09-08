namespace Application.Cities.Queries.GetCities
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Mapster;
    using MapsterMapper;

    using Common.Interfaces;
    using Common.Models;
    using Dto;

    public class GetAllCitiesQuery : IRequestWrapper<List<CityDto>>
    {

    }

    public class GetCitiesQueryHandler : IRequestHandlerWrapper<GetAllCitiesQuery, List<CityDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            //Will be removed.
            await Task.Delay(5000, cancellationToken);

            List<CityDto> list = await _context.Cities
                .Include(x => x.Districts)
                .ThenInclude(c => c.Villages)
                .ProjectToType<CityDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<CityDto>>(ServiceError.NotFount);

            //TODO: If the Elastic Log see the canceled request remove below code block.
            //try
            //{
            //    await Task.Delay(5000, cancellationToken);

            //    List<CityDto> list = await _context.Cities
            //            .Include(x => x.Districts)
            //            .ThenInclude(c => c.Villages)
            //            .ProjectToType<CityDto>(_mapper.Config)
            //            .ToListAsync(cancellationToken);

            //    return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<CityDto>>(ServiceError.NotFount);
            //}
            //catch (TaskCanceledException)
            //{

            //}

            //return ServiceResult.Failed<List<CityDto>>(ServiceError.Canceled);
        }
    }
}
