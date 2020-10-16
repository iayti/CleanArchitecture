namespace Application.Cities.Commands.Create
{
    using FluentValidation;

    using Common.Interfaces;

    public class CreateDistrictCommandValidator : AbstractValidator<CreateDistrictCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateDistrictCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
