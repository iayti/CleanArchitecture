namespace Application.Cities.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MapsterMapper;

    using Domain.Entities;
    using Common.Interfaces;
    using Common.Models;
    using Dto;

    public class CreateCityCommand : IRequestWrapper<CityDto>
    {
        public string Name { get; set; }
    }

    public class CreateCityCommandHandler : IRequestHandlerWrapper<CreateCityCommand, CityDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CityDto>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = new City
            {
                Name = request.Name
            };

            _context.Cities.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CityDto>(entity));
        }
    }
}
