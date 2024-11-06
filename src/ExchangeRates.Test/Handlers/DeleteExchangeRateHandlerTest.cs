
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Handlers;
using ExchangeRates.Infra.Repositories;
using FluentAssertions;
using MediatR;

namespace ExchangeRates.Test.Handlers;

public class DeleteExchangeRateHandlerTest : BaseTest
{

    public DeleteExchangeRateHandlerTest() : base() { }

    [Fact]
    public async Task DeleteExchangeRateTest_ShouldBeOK()
    {
        //given
        var uow = new UnitOfWork(dbContext);
        var exchageRate = await CreateExchangeRate(uow);
        var command = new DeleteExchangeRateCommand(exchageRate.ExternalId, true);

        //when
        var handler = new DeleteExchangeRateHandler(uow);
        var result = await handler.Handle(command, default);

        //then

        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task DeleteExchangeRateTest_ShouldBeNotOK()
    {
        //given
        var uow = new UnitOfWork(dbContext);
        var command = new DeleteExchangeRateCommand(Guid.NewGuid(), true);

        //when
        var handler = new DeleteExchangeRateHandler(uow);
        var result = await Record.ExceptionAsync(() => handler.Handle(command, default));

        //then
        result.Should().BeOfType<Exception>();
        result.Message.Should().Be("Record not found");
    }
}
