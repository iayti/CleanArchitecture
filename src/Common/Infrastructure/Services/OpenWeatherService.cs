using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Application.ExternalServices.OpenWeather.Request;
using Application.ExternalServices.OpenWeather.Response;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientHandler _httpClient;

        public OpenWeatherService(IHttpClientHandler httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.ClientApi = "open-weather-api";
        }

        public async Task<ServiceResult<OpenWeatherResponse>> GetCurrentWeatherForecast(OpenWeatherRequest request, CancellationToken cancellationToken)
        {
            string url = string.Concat("weather?", StringExtensions.ParseObjectToQueryString(request));

            var result = await _httpClient.GenericRequest<OpenWeatherRequest, OpenWeatherResponse>(string.Concat("weather?", StringExtensions.ParseObjectToQueryString(request)), cancellationToken, MethodType.Get, request);

            return result;
        }
    }
}