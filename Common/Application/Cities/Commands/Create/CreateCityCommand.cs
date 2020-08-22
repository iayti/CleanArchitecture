namespace Application.Cities.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;

    using Domain.Entities;
    using Common.Interfaces;

    public class CreateCityCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = new City
            {
                Name = request.Name
            };

            _context.Cities.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
