namespace Application.Cities.Commands.Update
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
