using FluentValidation;

namespace CleanArchitecture.Application.Districts.Commands.Create
{
    public class CreateDistrictCommandValidator : AbstractValidator<CreateDistrictCommand>
    {
        public CreateDistrictCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
