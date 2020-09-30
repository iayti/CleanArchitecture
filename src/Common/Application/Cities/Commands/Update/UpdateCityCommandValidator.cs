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
                .MustAsync(BeUniqueName).WithMessage("The specified city already exists. If you just want to activate the city leave the name field blank!");

            RuleFor(v => v.Id).NotNull();
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            //TODO: Control by uppercase and CultureInfo
            return await _context.Cities.AllAsync(x => x.Name != name, cancellationToken);
        }
    }
}
