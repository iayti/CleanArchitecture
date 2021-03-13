using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mapping;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.ExternalServices.OpenWeather.Request;
using CleanArchitecture.Application.ExternalServices.OpenWeather.Response;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Infrastructure.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientHandler _httpClient;

        private string ClientApi { get; } = "open-weather-api";

        public OpenWeatherService(IHttpClientHandler httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<OpenWeatherResponse>> GetCurrentWeatherForecast(OpenWeatherRequest request, CancellationToken cancellationToken)
        {
            return await _httpClient.GenericRequest<OpenWeatherRequest, OpenWeatherResponse>(ClientApi, string.Concat("weather?", StringExtensions
                .ParseObjectToQueryString(request, true)), cancellationToken, MethodType.Get, request);
        }
    }
}