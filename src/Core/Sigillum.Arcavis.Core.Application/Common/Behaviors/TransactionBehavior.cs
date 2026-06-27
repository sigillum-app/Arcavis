using Mediator;
using Microsoft.Extensions.Logging;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence;

namespace Sigillum.Arcavis.Core.Application.Common.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IBaseCommand
{
    #region Dependencies
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(
        IUnitOfWork unitOfWork,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    #endregion

    public async ValueTask<TResponse> Handle(TRequest request, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IManualTransactionRequest)
        {
            return await next(request, cancellationToken);
        }

        var requestName = typeof(TRequest).Name;

        try
        {
            return await _unitOfWork.ExecuteInTransactionAsync(async ct =>
            {
                _logger.LogInformation("--- Begin transaction for {RequestName}", requestName);

                var response = await next(request, cancellationToken);

                _logger.LogInformation("--- Commit transaction for {RequestName}", requestName);

                return response;

            }, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "--- Rollback transaction executed for {RequestName}", requestName);
            throw;
        }

    }
}
