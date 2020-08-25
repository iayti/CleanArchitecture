namespace Application.Cities.Commands.Delete
{
    using FluentValidation;

    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.Id).NotNull();
        }
    }
}
