namespace Application.Cities.Commands.Update
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using FluentValidation;

    using Common.Interfaces;

    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCityCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified city already exists.")
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(v => v.Id).NotNull();
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            //TODO: Büyük küçük harf ve türkçe karaktere göre kontrol sağla
            bool test = await _context.Cities.AllAsync(x => x.Name != name, cancellationToken);
            return await _context.Cities.AllAsync(x => x.Name != name, cancellationToken);
        }
    }
}
