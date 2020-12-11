using System.Threading.Tasks;
using Application.Common.Models;
using Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery;

namespace Application.Common.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<ServiceResult<OpenWeatherResponse>> GetCurrenWeatherForecast(OpenWeatherRequest request);
    }
}