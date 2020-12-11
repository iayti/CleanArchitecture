using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IHttpClientHandler<TService> where  TService : class 
    {
        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string url,
            CancellationToken cancellationToken,
            Dictionary<string, string> headers,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null)
            where TResult : class where TRequest : class;
    }
}