namespace Application.Common.Behaviours
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Logging;

    using MediatR;

    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, "CleanArchitecture Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

                throw;
            }
        }

    }
}
