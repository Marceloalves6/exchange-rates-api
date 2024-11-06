using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Validators;
using FluentAssertions;

namespace ExchangeRates.Test.Validators;

public  class AddExchangeRateResquestValidatorTest 
{
    [Fact]
    public async Task AddExchangeRateResquestValidatorTest_ShouldBeOK()
    {
        var validator = new AddExchangeRateResquestValidator();
        var request = new AddExchangeRateRequest("EUR", "USD", 0.91479000m, 0.91479000m);
        var command = new AddExchangeRateCommand(request);
        var result = await validator.ValidateAsync(command);
        
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task AddExchangeRateResquestValidatorTest_ShouldNotBeOK()
    {
        var validator = new AddExchangeRateResquestValidator();
        var request = new AddExchangeRateRequest("", "", 0.91479000m, 0.91479000m);
        var command = new AddExchangeRateCommand(request);
        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
    }
}
