using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IHttpClientHandler
    {
        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(
            string url,
            CancellationToken cancellationToken,
            TRequest requestEntity = null,
            MethodType method = MethodType.Get)
            where TResult : class where TRequest : class;
    }
}