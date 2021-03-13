using FluentValidation;

namespace CleanArchitecture.Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery
{
    public class GetCurrentWeatherForecastQueryValidator : AbstractValidator<GetCurrentWeatherForecastQuery>
    {
        public GetCurrentWeatherForecastQueryValidator()
        {
            RuleFor(x=>x.Id)
                .NotNull()
                .NotEmpty().WithMessage("Id is required.");
            
            RuleFor(x=>x.Q)
                .NotNull()
                .NotEmpty().WithMessage("Q is required.");
            
            RuleFor(x=>x.Lat)
                .NotNull()
                .NotEmpty().WithMessage("Lat is required.");
            
            RuleFor(x=>x.Lon)
                .NotNull()
                .NotEmpty().WithMessage("Lon is required.");
        }
    }
}