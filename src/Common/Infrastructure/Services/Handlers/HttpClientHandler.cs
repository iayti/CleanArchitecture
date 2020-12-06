using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;

namespace Infrastructure.Services.Handlers 
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(
            string url,
            CancellationToken cancellationToken,
            TRequest requestEntity = null,
            MethodType method = MethodType.Get)
            where TResult : class where TRequest : class
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = null;
                switch (method)
                {
                    case MethodType.Get:
                        response = await client.GetAsync(url, cancellationToken);
                        break;
                    case MethodType.Post:
                        response = await client.PostAsJsonAsync(url, requestEntity, cancellationToken);
                        break;
                }

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
                return ServiceResult.Failed<TResult>(ServiceError.CustomMessage(ex.ToString()));
            }
        }
    }
}