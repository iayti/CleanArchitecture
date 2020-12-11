using System.Threading.Tasks;
using Application.Common.Models;
using Application.ExternalServices.OpenWeather.Request;
using Application.ExternalServices.OpenWeather.Response;
using Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery;

namespace Application.Common.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<ServiceResult<OpenWeatherResponse>> GetCurrentWeatherForecast(OpenWeatherRequest request);
    }
}