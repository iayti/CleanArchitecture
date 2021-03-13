using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services.Handlers
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpClientHandler> _logger;

        public HttpClientHandler(IHttpClientFactory httpClientFactory, ILogger<HttpClientHandler> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string clientApi, string url, CancellationToken cancellationToken,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null)
            where TResult : class where TRequest : class
        {
            var httpClient = _httpClientFactory.CreateClient(clientApi);

            var requestName = typeof(TRequest).Name;

            try
            {
                _logger.LogInformation("HttpClient Request: {RequestName} {@Request}", requestName, requestEntity);

                var response = method switch
                {
                    MethodType.Get => await httpClient.GetAsync(url, cancellationToken),
                    MethodType.Post => await httpClient.PostAsJsonAsync(url, requestEntity, cancellationToken),
                    _ => null
                };

                if (response != null && response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<TResult>(cancellationToken: cancellationToken);
                    return ServiceResult.Success(data);
                }

                if (response == null)
                    return ServiceResult.Failed<TResult>(ServiceError.ServiceProvider);

                var message = await response.Content.ReadAsStringAsync(cancellationToken);

                var error = new ServiceError(message, (int) response.StatusCode);

                return ServiceResult.Failed<TResult>(error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HttpClient Request: Unhandled Exception for Request {RequestName} {@Request}", requestName, requestEntity);
                return ServiceResult.Failed<TResult>(ServiceError.CustomMessage(ex.ToString()));
            }
        }
    }
}