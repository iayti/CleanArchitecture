using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery;

namespace Infrastructure.Services.WeatherForeCast
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientHandler<OpenWeatherService> _httpClient;

        public OpenWeatherService(IHttpClientHandler<OpenWeatherService> httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ServiceResult<OpenWeatherResponse>> GetCurrenWeatherForecast(OpenWeatherRequest request)
        {
            throw new NotImplementedException();
        }
    }
}