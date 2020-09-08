namespace Application.Cities.Commands.Update
{
    using System.Threading;
    using System.Threading.Tasks;

    using MapsterMapper;

    using Common.Interfaces;
    using Common.Models;
    using Dto;
    
    public class UpdateCityCommand :IRequestWrapper<CityDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateCityCommandHandler : IRequestHandlerWrapper<UpdateCityCommand, CityDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CityDto>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cities.FindAsync(request.Id);

            if (entity == null)
            {
                return ServiceResult.Failed<CityDto>(ServiceError.NotFount);
            }

            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CityDto>(entity));
        }
    }
}
