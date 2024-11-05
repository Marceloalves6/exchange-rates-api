using ExchangeRates.Core.Commands;
using FluentValidation;

namespace ExchangeRates.Core.Validators;

internal class AddExchangeRateResquestValidator : AbstractValidator<AddExchangeRateCommand>
{
    public AddExchangeRateResquestValidator()
    {
        RuleFor(i => i.AddExchangeRateRequest.CurrencyFrom)
            .NotEmpty();

        RuleFor(i => i.AddExchangeRateRequest.CurrencyTo)
           .NotEmpty();

        RuleFor(i => i.AddExchangeRateRequest.AskPrice)
         .GreaterThan(0);

        RuleFor(i => i.AddExchangeRateRequest.BidPrice)
         .GreaterThan(0);
    }
}
