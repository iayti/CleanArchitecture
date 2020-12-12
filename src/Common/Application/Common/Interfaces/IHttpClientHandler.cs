using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IHttpClientHandler//<TService> where  TService : class 
    {
        //string ClientApi { get; set; }

        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string url,
            CancellationToken cancellationToken,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null)
            where TResult : class where TRequest : class;
    }
}