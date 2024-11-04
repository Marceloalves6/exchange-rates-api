using ExchangeRates.Core.Behaviors;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.Core;

public static class Startup
{
    public static void AddCoreDependencies(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddValidatorsFromAssemblyContaining(typeof(Startup), includeInternalTypes : true);
    }
}
