namespace Application.Cities.Commands.Create
{
    using FluentValidation;

    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
