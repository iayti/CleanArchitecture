namespace Application.Cities.Commands.Delete
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Microsoft.EntityFrameworkCore;

    using MapsterMapper;

    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using Dto;
    
    public class DeleteCityCommand : IRequestWrapper<CityDto>
    {
        public int Id { get; set; }
    }

    public class DeleteCityCommandHandler : IRequestHandlerWrapper<DeleteCityCommand, CityDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteCityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CityDto>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cities
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            _context.Cities.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<CityDto>(entity));
        }
    }
}
