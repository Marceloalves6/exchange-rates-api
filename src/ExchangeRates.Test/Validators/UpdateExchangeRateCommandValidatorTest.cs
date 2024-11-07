using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Validators;
using ExchangeRates.Infra.Services;
using FluentAssertions;

namespace ExchangeRates.Test.Validators;

public class UpdateExchangeRateCommandValidatorTest
{
    private readonly CurrencyService currencyService;

    public UpdateExchangeRateCommandValidatorTest()
    {
        currencyService = new CurrencyService();
    }

    [Fact]
    public async Task UpdateExchangeRateCommandValidatorTest_ShouldBeOk()
    {
        //given
        var validator = new UpdateExchangeRateCommandValidator(currencyService);
        var request = new UpdateExchangeRateResquest(Guid.NewGuid(), "EUR", "USD", 100m, 100m);
        var command = new UpdateExchangeRateCommand(request);

        //when
        var result = await validator.ValidateAsync(command);

        //then
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateExchangeRateCommandValidatorTest_ShouldNotBeOk()
    {
        //given
        var validator = new UpdateExchangeRateCommandValidator(currencyService);
        var request = new UpdateExchangeRateResquest(Guid.Empty, "AAA", "ZZZ", 100m, 100m);
        var command = new UpdateExchangeRateCommand(request);
        //when
        var result = await validator.ValidateAsync(command);

        //then
        result.IsValid.Should().BeFalse();
    }
}
