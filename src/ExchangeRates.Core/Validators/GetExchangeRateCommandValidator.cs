using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Services;
using FluentValidation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRates.Test")]
namespace ExchangeRates.Core.Validators;

internal class GetExchangeRateCommandValidator : BaseValidator<GetExchangeRateCommand>
{
    public GetExchangeRateCommandValidator(ICurrencyService currencyService) : base(currencyService)
    {
        RuleFor(i => i.GetExchangeRateRequest.CurrencyFrom)
            .NotEmpty()
            .WithMessage("currencyFrom is required");

        RuleFor(i => i.GetExchangeRateRequest.CurrencyFrom)
           .Must((currencyFrom) => ValidadeCurrency(currencyFrom))
           .WithMessage("'{PropertyValue}' is not a valid value for currencyFrom.");

        RuleFor(i => i.GetExchangeRateRequest.CurrencyTo)
            .NotEmpty()
             .WithMessage("currencyTo is required");

        RuleFor(i => i.GetExchangeRateRequest.CurrencyTo)
            .Must((currencyTo) => ValidadeCurrency(currencyTo))
            .WithMessage("'{PropertyValue}' is not a valid value for currencyTo.");

        RuleFor(i => i.GetExchangeRateRequest.CurrencyTo)
          .NotEqual(i => i.GetExchangeRateRequest.CurrencyFrom)
          .WithMessage("CurrencyFrom and CurrencyTo can not have the same value");
    }
}
