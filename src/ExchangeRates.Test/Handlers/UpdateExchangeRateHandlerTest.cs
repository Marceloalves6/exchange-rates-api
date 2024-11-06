using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Handlers;
using ExchangeRates.Infra.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExchangeRates.Test.Handlers;

public class UpdateExchangeRateHandlerTest : TestBase
{
    public UpdateExchangeRateHandlerTest() : base() { }

    [Fact]
    public async Task UpdateExchangeRateHandlerTest_ShouldBeOK()
    {

        //given
        var uow = new UnitOfWork(dbContext);
        var exchageRate = await CreateExchangeRate(uow);
        var request = new UpdateExchangeRateResquest(exchageRate.ExternalId, "EUR", "USD", 0.81479000m, 0.81479000m);
        var command = new UpdateExchangeRateCommand(request);
        var logger = new Mock<ILogger<UpdateExchangeRateHandler>>();

        //when
        var handler = new UpdateExchangeRateHandler(uow, mapper, logger.Object);
        var result = await handler.Handle(command, default);

        //then
        result.Should().NotBeNull();
        result.CurrencyFrom.Should().BeEquivalentTo(request.CurrencyFrom);
        result.CurrencyTo.Should().BeEquivalentTo(request.CurrencyTo);
        result.AskPrice.Should().Be(request.AskPrice);
        result.BidPrice.Should().Be(request.BidPrice);
    }

    [Fact]
    public async Task UpdateExchangeRateHandlerTest_ShouldNotBeOK()
    {
        //given
        var uow = new UnitOfWork(dbContext);
        var request = new UpdateExchangeRateResquest(Guid.NewGuid(), "EUR", "USD", 0.81479000m, 0.81479000m);
        var command = new UpdateExchangeRateCommand(request);

        //when
        var handler = new UpdateExchangeRateHandler(uow, mapper, GetLoggerMocker<UpdateExchangeRateHandler>().Object);
        var result = await Record.ExceptionAsync(() => handler.Handle(command, default));

        //then
        result.Should().NotBeNull();
        result.Should().BeOfType<Exception>();
        result.Message.Should().Be("Record not found");
    }
}
