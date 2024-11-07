using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Handlers;
using ExchangeRates.Core.Models;
using ExchangeRates.Core.Services;
using ExchangeRates.Infra.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ExchangeRates.Test.Handlers;

public class GetExchangeRateHandlerTest : TestBase
{

    [Fact]
    public async Task GetExchangeRateHandlerTest_ShouldBeOk()
    {
        var uow = new UnitOfWork(dbContext);
        var exchangeRate = await CreateExchangeRate(uow);
        string currencyFrom = exchangeRate?.CurrencyFrom ?? "EUR";
        string currencyTo = exchangeRate?.CurrencyTo ?? "USD";

        var configuration = new Mock<IConfiguration>();
        var alphavantageService = new Mock<IAlphavantageService>();
        var mediator = new Mock<IMediator>();
        var handler = new GetExchangeRateHandler(uow, configuration.Object, mapper, alphavantageService.Object, mediator.Object, GetLoggerMocker<GetExchangeRateHandler>().Object);
        var request = new GetExchangeRateRequest(currencyFrom, currencyTo);
        var command = new GetExchangeRateCommand(request);

        var result = await handler.Handle(command, default);

        request.CurrencyFrom.Should().Be(currencyFrom);
        request.CurrencyTo.Should().Be(currencyTo);
    }

    [Fact]
    public async Task GetExchangeRateHandlerTest_ShouldNotBeOk()
    {
        var uow = new UnitOfWork(dbContext);
        string currencyFrom = "EUR";
        string currencyTo = "USD";

        var alphavantageService = new Mock<IAlphavantageService>();
        alphavantageService.Setup(i => i.CurrencyExchangeRateAsync(It.IsAny<CurrencyExchangeRateParams>())).ReturnsAsync(new CurrencyExchangeRateResponse());
        var mediator = new Mock<IMediator>();
        var handler = new GetExchangeRateHandler(uow, configuration, mapper, alphavantageService.Object, mediator.Object, GetLoggerMocker<GetExchangeRateHandler>().Object);
        var request = new GetExchangeRateRequest(currencyFrom, currencyTo);
        var command = new GetExchangeRateCommand(request);

        var result = await Record.ExceptionAsync(()=> handler.Handle(command, default));

        result.Should().BeNull();
    }
}
