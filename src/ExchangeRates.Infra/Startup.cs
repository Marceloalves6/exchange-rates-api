using ExchangeRates.Core.Repositories;
using ExchangeRates.Core.Services;
using ExchangeRates.Infra.Repositories;
using ExchangeRates.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.Infra;

public static class Startup
{
    public static void AddInfraDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ICurrencyService, CurrencyService>();
        // services.AddScoped<IMessageQueueService, MessageQueueService>();
        services.AddScoped<IMessageQueueService, MockMessageQueueService>();
    }

    public static void ApplyMigrations(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ExchangeRatesDbContext>();
        dbContext.Database.Migrate();
    }
}


