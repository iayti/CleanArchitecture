namespace Application.Cities.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MapsterMapper;

    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using Domain.Event;
    using Dto;

    public class CreateDistrictCommand : IRequestWrapper<DistrictDto>
    {
        public string Name { get; set; }

        public int CityId { get; set; }
    }

    public class CreateDistrictCommandHandler : IRequestHandlerWrapper<CreateDistrictCommand, DistrictDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateDistrictCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DistrictDto>> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            var entity = new District
            {
                Name = request.Name,
                CityId = request.CityId
            };

            await _context.Districts.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<DistrictDto>(entity));
        }
    }
}
