using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Services;
using FluentValidation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRates.Test")]
namespace ExchangeRates.Core.Validators;

internal class UpdateExchangeRateCommandValidator : BaseValidator<UpdateExchangeRateCommand>
{
    public UpdateExchangeRateCommandValidator(ICurrencyService currencyService) : base(currencyService)
    {
        RuleFor(i => i.UpdateExchangeRateResquest.Id)
          .NotEqual(Guid.Empty);

        RuleFor(i => i.UpdateExchangeRateResquest.CurrencyFrom)
           .NotEmpty();

        RuleFor(i => i.UpdateExchangeRateResquest.CurrencyFrom)
           .Must((currencyFrom) => ValidadeCurrency(currencyFrom))
           .WithMessage("{PropertyValue} is not a valid value for currencyFrom.");

        RuleFor(i => i.UpdateExchangeRateResquest.CurrencyTo)
           .NotEmpty();

        RuleFor(i => i.UpdateExchangeRateResquest.CurrencyTo)
          .Must((currencyTo) => ValidadeCurrency(currencyTo))
          .WithMessage("{PropertyValue} is not a valid value for currencyTo.");

        RuleFor(i => i.UpdateExchangeRateResquest.AskPrice)
         .GreaterThan(0);

        RuleFor(i => i.UpdateExchangeRateResquest.BidPrice)
         .GreaterThan(0);
    }
}
