using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Handlers
{
    public class HttpClientHandler<TService> : IHttpClientHandler<TService> where TService : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TService> _logger;

        public HttpClientHandler(IHttpClientFactory httpClientFactory, ILogger<TService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string url,
            CancellationToken cancellationToken,
            Dictionary<string, string> headers,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null)
            where TResult : class where TRequest : class
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(url);

            if (headers.Count > 0)
            {
                foreach ((string key, string value) in headers)
                {
                    client.DefaultRequestHeaders.Add(key, value);
                }
            }

            var requestName = typeof(TRequest).Name;
            var serviceName = typeof(TService).Name;
            
            try
            {
                _logger.LogInformation("HttpClient Request: {ServiceName} {RequestName} {@Request}", serviceName, requestName, requestEntity);

                var response = method switch
                {
                    MethodType.Get => await client.GetAsync(url, cancellationToken),
                    MethodType.Post => await client.PostAsJsonAsync(url, requestEntity, cancellationToken),
                    _ => null
                };

                if (response != null && response.IsSuccessStatusCode)
                {
                    var jsonResultString = await response.Content.ReadAsStringAsync(cancellationToken);
                    var data = JsonSerializer.Deserialize<TResult>(jsonResultString);
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
                _logger.LogError(ex, "HttpClient Request: Unhandled Exception for Request {ServiceName} {RequestName} {@Request}", serviceName, requestName, requestEntity);
                return ServiceResult.Failed<TResult>(ServiceError.CustomMessage(ex.ToString()));
            }
        }
    }
}