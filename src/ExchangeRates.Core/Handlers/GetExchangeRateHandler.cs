using AutoMapper;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Repositories;
using ExchangeRates.Core.Services;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ExchangeRates.Core.Handlers;

public class GetExchangeRateHandler(IUnitOfWork uow, IConfiguration configuration, IMapper mapper, IAlphavantageService alphavantageService, IMediator mediator) : IRequestHandler<GetExchangeRateCommand, GetExchangeRateResponse>
{
    public async Task<GetExchangeRateResponse> Handle(GetExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var exchage = await uow.ExchangeRepository.GetAsync(request.GetExchangeRateRequest.CurrencyFrom, request.GetExchangeRateRequest.CurrencyTo);

        if (exchage is null)
        {
            return await GetExchangeRateFromExternalProvider(request.GetExchangeRateRequest.CurrencyFrom, request.GetExchangeRateRequest.CurrencyTo);
        }

        if (exchage is null)
        {
            
        }

        var response = mapper.Map<GetExchangeRateResponse>(exchage);

        return response;
    }

    private async Task<GetExchangeRateResponse> GetExchangeRateFromExternalProvider(string currencyFrom, string currencyTo)
    {
        var function = configuration.GetValue<string>("AlphavantageConfiguration:Function");
        var apiKey = configuration.GetValue<string>("AlphavantageConfiguration:ApiKey");
        var currencyExchangeRate = await alphavantageService.CurrencyExchangeRateAsync(function, currencyFrom, currencyFrom, apiKey);


        var request = new AddExchangeRateRequest(currencyFrom,
                                                 currencyTo,
                                                 currencyExchangeRate.RealtimeCurrencyExchangeRate?.AskPrice ?? 0,
                                                 currencyExchangeRate.RealtimeCurrencyExchangeRate?.AskPrice ?? 0);

         var response = await mediator.Send(new AddExchangeRateCommand(request));

        return mapper.Map<GetExchangeRateResponse>(response);
    }
}
/*
 Your API Key: ed2a70cbe4f595965df8527b
 Example Request: https://v6.exchangerate-api.com/v6/ed2a70cbe4f595965df8527b/latest/USD
*/

/*
 https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=USD&to_currency=EUR&apikey=U23W00043X08VE9U
{
    "Realtime Currency Exchange Rate": {
        "1. From_Currency Code": "USD",
        "2. From_Currency Name": "United States Dollar",
        "3. To_Currency Code": "EUR",
        "4. To_Currency Name": "Euro",
        "5. Exchange Rate": "0.91720000",
        "6. Last Refreshed": "2024-11-05 12:13:01",
        "7. Time Zone": "UTC",
        "8. Bid Price": "0.91719000",
        "9. Ask Price": "0.91723000"
    }
}
 */


