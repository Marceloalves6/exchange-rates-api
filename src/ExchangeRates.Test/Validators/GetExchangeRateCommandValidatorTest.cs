using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Validators;
using ExchangeRates.Infra.Services;
using FluentAssertions;

namespace ExchangeRates.Test.Validators;

public class GetExchangeRateCommandValidatorTest
{
    private readonly CurrencyService currencyService;

    public GetExchangeRateCommandValidatorTest()
    {
        currencyService = new CurrencyService();
    }

    [Fact]
    public async Task GetExchangeRateCommandValidatorTest_ShouldBeOk()
    {
        //given
        var validator = new GetExchangeRateCommandValidator(currencyService);
        var request = new GetExchangeRateRequest("EUR", "USD");
        var command = new GetExchangeRateCommand(request);

        //when
        var result = await validator.ValidateAsync(command);

        //then
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task GetExchangeRateCommandValidatorTest_ShouldNotBeOk()
    {
        //given
        var validator = new GetExchangeRateCommandValidator(currencyService);
        var request = new GetExchangeRateRequest("AAA", "BBB");
        var command = new GetExchangeRateCommand(request);

        //when
        var result = await validator.ValidateAsync(command);

        //then
        result.IsValid.Should().BeFalse();
    }
}
