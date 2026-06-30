using FluentValidation;
using Mediator;
using Microsoft.Extensions.Logging;
using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Application.Common.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IMessage
{
    #region Dependencies
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators, 
        ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }
    #endregion

    public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next(message, cancellationToken);

        var context = new ValidationContext<TRequest>(message);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .Select(f => MapToError(f))
            .DistinctBy(e => e.Key)
            .ToArray();

        if (failures.Length == 0)
            return await next(message, cancellationToken);

        var responseType = typeof(TResponse);

        if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var innerType = responseType.GetGenericArguments()[0];
            var failMethod = typeof(Result<>)
                .MakeGenericType(innerType)
                .GetMethod(nameof(Result<object>.Failure), new[] { typeof(IError[]) });

            return (TResponse)failMethod!.Invoke(null, new object[] { failures })!;
        }

        return (TResponse)(object)Result.Failure(failures);
    }

    private IError MapToError(FluentValidation.Results.ValidationFailure failure)
    {
        if (failure.CustomState is IError stateError)
            return stateError;

        _logger.LogWarning(
            "Validation rule for {Property} on {Request} is missing WithState(IError); falling back to generic error.",
            failure.PropertyName,
            typeof(TRequest).Name);

        return new ValidationError(9000, "VALIDATION_FAILED", failure.PropertyName, failure.ErrorMessage);
    }
}
