using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ExternalServices.OpenWeather.Request;
using Application.ExternalServices.OpenWeather.Response;
using Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery;

namespace Infrastructure.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientHandler<OpenWeatherService> _httpClient;

        public OpenWeatherService(IHttpClientHandler<OpenWeatherService> httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ServiceResult<OpenWeatherResponse>> GetCurrentWeatherForecast(OpenWeatherRequest request)
        {
            throw new NotImplementedException();
        }
    }
}