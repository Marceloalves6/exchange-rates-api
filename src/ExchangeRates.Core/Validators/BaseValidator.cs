using ExchangeRates.Core.Services;
using FluentValidation;

namespace ExchangeRates.Core.Validators
{
    internal class BaseValidator<T>(ICurrencyService currencyService) : AbstractValidator<T>
    {
        protected bool ValidadeCurrency(string? iso) => currencyService.IsCurrencyValid(iso);
    }
}
