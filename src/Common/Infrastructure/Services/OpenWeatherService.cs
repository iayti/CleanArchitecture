using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ExternalServices.OpenWeather.Request;
using Application.ExternalServices.OpenWeather.Response;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientHandler<OpenWeatherService> _httpClient;
        private readonly IConfiguration _configuration;

        public OpenWeatherService(IHttpClientHandler<OpenWeatherService> httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ServiceResult<OpenWeatherResponse>> GetCurrentWeatherForecast(OpenWeatherRequest request,
        CancellationToken cancellationToken)
        {
            //TODO: adding appsettings
            Dictionary<string,string> headers = new Dictionary<string, string>();
            var result = await _httpClient.GenericRequest<OpenWeatherRequest, OpenWeatherResponse>("url", 
            cancellationToken, headers,
                MethodType.Get, request);
            
            throw new NotImplementedException();
        }
    }
}