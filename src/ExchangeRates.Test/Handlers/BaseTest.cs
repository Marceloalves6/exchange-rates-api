using AutoMapper;
using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Mappings;
using ExchangeRates.Core.Repositories;
using ExchangeRates.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ExchangeRates.Test.Handlers;

public class BaseTest
{
    protected readonly ExchangeRatesDbContext? dbContext;
    protected readonly ServiceCollection? Services;
    protected readonly IMapper mapper;

    public BaseTest()
    {
        var services = new ServiceCollection();

        services.AddDbContext<ExchangeRatesDbContext>(options =>
        {
            options.UseInMemoryDatabase("ExchangeDB");
        });

        dbContext = services.BuildServiceProvider().GetRequiredService<ExchangeRatesDbContext>();

        mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ExchangeRateProfile());
        }).CreateMapper();
    }

    protected async Task<ExchangeRate> CreateExchangeRate(IUnitOfWork uow)
    {
        var exchangeRate = await uow.ExchangeRepository.AddAsync(new ExchangeRate
        {
            CurrencyFrom = "EUR",
            CurrencyTo = "USD",
            AskPrice = 0.91479000m,
            BidPrice = 0.91479000m,
            CreatedAt = DateTime.UtcNow,
            Deleted = false,
            ExternalId = Guid.NewGuid(),
            UpdatedAt = DateTime.MinValue
        });

        await uow.CommitAsync(default);

        return exchangeRate;
    }
}
