using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Validators;
using ExchangeRates.Infra.Services;
using FluentAssertions;

namespace ExchangeRates.Test.Validators;

public class AddExchangeRateResquestValidatorTest
{
    private readonly CurrencyService currencyService;

    public AddExchangeRateResquestValidatorTest()
    {
        currencyService = new CurrencyService();
    }

    [Fact]
    public async Task AddExchangeRateResquestValidatorTest_ShouldBeOK()
    {
        //given
        var validator = new AddExchangeRateResquestValidator(currencyService);
        var request = new AddExchangeRateRequest("EUR", "USD", 0.91479000m, 0.91479000m);
        var command = new AddExchangeRateCommand(request);

        //when
        var result = await validator.ValidateAsync(command);

        //then
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task AddExchangeRateResquestValidatorTest_ShouldNotBeOK()
    {
        //given
        var validator = new AddExchangeRateResquestValidator(currencyService);
        var request = new AddExchangeRateRequest("", "", 0.91479000m, 0.91479000m);
        var command = new AddExchangeRateCommand(request);

        //when
        var result = await validator.ValidateAsync(command);

        //then
        result.IsValid.Should().BeFalse();
    }
}
