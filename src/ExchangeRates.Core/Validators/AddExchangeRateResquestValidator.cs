using ExchangeRates.Core.Operations;
using FluentValidation;

namespace ExchangeRates.Core.Validators;

internal class AddExchangeRateResquestValidator : AbstractValidator<AddExchangeRateResquest>
{
    public AddExchangeRateResquestValidator()
    {
        RuleFor(i => i.CurrencyFrom)
            .NotEmpty();

        RuleFor(i => i.CurrencyTo)
           .NotEmpty();

        RuleFor(i => i.PriceAsk)
         .GreaterThan(0);

        RuleFor(i => i.PriceBid)
         .GreaterThan(0);
    }
}
