using ExchangeRates.Core.Repositories;
using ExchangeRates.Core.Services;
using ExchangeRates.Infra.Repositories;
using ExchangeRates.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.Infra;

public static class Startup
{
    public static void AddInfraDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ICurrencyService, CurrencyService>();

        if (configuration.GetValue<bool>("MockExternalDependencies"))
        {
            services.AddScoped<IMessageQueueService, MockMessageQueueService>();
        }
        else
        {
            services.AddScoped<IMessageQueueService, MessageQueueService>();
        }
    }

    public static void ApplyMigrations(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ExchangeRatesDbContext>();
        dbContext.Database.Migrate();
    }
}


