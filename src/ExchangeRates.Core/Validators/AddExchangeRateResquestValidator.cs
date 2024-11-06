using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Services;
using FluentValidation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRates.Test")]
namespace ExchangeRates.Core.Validators;

internal class AddExchangeRateResquestValidator : BaseValidator<AddExchangeRateCommand>
{

    public AddExchangeRateResquestValidator(ICurrencyService currencyService) : base(currencyService)
    {
        RuleFor(i => i.AddExchangeRateRequest.CurrencyFrom)
            .NotEmpty();

        RuleFor(i => i.AddExchangeRateRequest.CurrencyFrom)
           .Must((currencyFrom) => ValidadeCurrency(currencyFrom))
           .WithMessage("{PropertyValue} is not a valid value for currencyFrom.");

        RuleFor(i => i.AddExchangeRateRequest.CurrencyTo)
           .NotEmpty();

        RuleFor(i => i.AddExchangeRateRequest.CurrencyTo)
          .Must((currencyTo) => ValidadeCurrency(currencyTo))
          .WithMessage("{PropertyValue} is not a valid value for currencyTo.");

        RuleFor(i => i.AddExchangeRateRequest.AskPrice)
         .GreaterThan(0);

        RuleFor(i => i.AddExchangeRateRequest.BidPrice)
         .GreaterThan(0);
    }
}
