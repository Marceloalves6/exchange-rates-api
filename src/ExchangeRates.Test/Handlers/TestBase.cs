using AutoMapper;
using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Mappings;
using ExchangeRates.Core.Repositories;
using ExchangeRates.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExchangeRates.Test.Handlers;

public class TestBase
{
    protected readonly ExchangeRatesDbContext? dbContext;
    protected readonly ServiceCollection? Services;
    protected readonly IMapper mapper;
    protected readonly IConfiguration configuration;

    public TestBase()
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

        configuration = new ConfigurationBuilder()
       .AddInMemoryCollection(new Dictionary<string, string?>
       {
            {"AlphavantageConfiguration:Function", "CURRENCY_EXCHANGE_RATE"},
            {"AlphavantageConfiguration:ApiKey", "123456789"}
       })
       .Build();
    }
    protected Mock<ILogger<T>> GetLoggerMocker<T>() where T : class => new Mock<ILogger<T>>();

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
