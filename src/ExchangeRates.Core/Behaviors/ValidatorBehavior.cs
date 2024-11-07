using ExchangeRates.Core.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExchangeRates.Core.Behaviors;

public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidatorBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var typeName = request.GetGenericTypeName();

        logger.LogDebug("====> Validating message {MessageType}", typeName);

        var failures = validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .ToList();

        if (failures.Any())
        {
            logger.LogWarning(
                "Validation errors - {MessageType} - Message: {@Message} - Errors: {@ValidationErrors}",
                typeName,
                request,
                failures
            );

            throw new Exception(
                $"Command Validation Errors for type {typeof(TRequest).Name}",
                new FluentValidation.ValidationException("Validation exception", failures)
            );
        }

        return await next();
    }
}
