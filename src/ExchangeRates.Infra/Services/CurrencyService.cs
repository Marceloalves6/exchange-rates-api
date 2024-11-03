using ExchangeRates.Core.Services;
using System.Reflection;
using System.Text.Json;

namespace ExchangeRates.Infra.Services;

public class CurrencyService : ICurrencyService
{
    private Dictionary<string, string>? Currencies { get; set; }

    public CurrencyService()
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var file = File.ReadAllText(Path.Join(basePath, "Resources", "currencies.json"));

        if (!string.IsNullOrWhiteSpace(file))
            Currencies = JsonSerializer.Deserialize<Dictionary<string, string>>(file) ?? new();
    }

    public bool IsCurrencyValid(string iso) => Currencies?.ContainsKey(iso) ?? false;
}
