using AutoMapper;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Handlers;
using ExchangeRates.Core.Mappings;
using ExchangeRates.Core.Services;
using ExchangeRates.Infra.Repositories;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ExchangeRates.Test.Handlers;

public class AddExchangeRateHandlerTest : TestBase
{
    public AddExchangeRateHandlerTest() : base() { }

    [Fact]
    public async Task AddNewExchangeRateTest_ShouldBeOK()
    {
        //given
        var request = new AddExchangeRateRequest("EUR", "USD", 0.91479000m, 0.91479000m);
        var command = new AddExchangeRateCommand(request);
        var uow = new UnitOfWork(dbContext);
        var messageQueueService = new Mock<IMessageQueueService>();
      
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ExchangeRateProfile());
        }).CreateMapper();

        //when
        var handler = new AddExchangeRateHandler(uow, mapper, messageQueueService.Object, GetLoggerMocker<AddExchangeRateHandler>().Object);
        var result = await handler.Handle(command, default);

        //then

        result.Should().NotBeNull();
        result.CurrencyFrom.Should().BeEquivalentTo(request.CurrencyFrom);
        result.CurrencyTo.Should().BeEquivalentTo(request.CurrencyTo);
        result.AskPrice.Should().Be(request.AskPrice);
        result.BidPrice.Should().Be(request.BidPrice);
    }
}
