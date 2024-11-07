using AutoMapper;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Repositories;
using ExchangeRates.Core.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRates.Test")]
namespace ExchangeRates.Core.Handlers;

public class GetExchangeRateHandler
(
    IUnitOfWork uow,
    IConfiguration configuration,
    IMapper mapper,
    IAlphavantageService alphavantageService,
    IMediator mediator,
    ILogger<GetExchangeRateHandler> logger
) : IRequestHandler<GetExchangeRateCommand, GetExchangeRateResponse?>
{
    public async Task<GetExchangeRateResponse?> Handle(GetExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var exchage = await uow.ExchangeRepository.GetAsync(request.GetExchangeRateRequest.CurrencyFrom, request.GetExchangeRateRequest.CurrencyTo);

        //Once the exchange rate was not available, it will consult an external API to check for this information  
        if (exchage is null)
        {
            logger.LogWarning($@"It was not possible to find the exchange rate 
                                for {request.GetExchangeRateRequest.CurrencyFrom}/{request.GetExchangeRateRequest.CurrencyTo}.
                                Consulting Alphavantage API to check for the information.");

            return await GetExchangeRateFromExternalProvider(request.GetExchangeRateRequest.CurrencyFrom ?? "", request.GetExchangeRateRequest.CurrencyTo ?? "");
        }

        var response = mapper.Map<GetExchangeRateResponse>(exchage);

        return response;
    }

    private async Task<GetExchangeRateResponse?> GetExchangeRateFromExternalProvider(string currencyFrom, string currencyTo)
    {
        var function = configuration.GetValue<string>("AlphavantageConfiguration:Function");
        var apiKey = configuration.GetValue<string>("AlphavantageConfiguration:ApiKey");

        CurrencyExchangeRateParams paramenters = new()
        {
            Function = function,
            CurrencyFrom = currencyFrom,
            CurrencyTo = currencyTo,
            ApiKey = apiKey,
        };

        var currencyExchangeRate = await alphavantageService.CurrencyExchangeRateAsync(paramenters);

        if (currencyExchangeRate is null || currencyExchangeRate.RealtimeCurrencyExchangeRate is null)
        {
            logger.LogWarning($"It was not possible to find the exchange rate for {currencyFrom}/{currencyTo} in Alphavantage API");

            return default;
        }

        var request = new AddExchangeRateRequest(currencyFrom,
                                                 currencyTo,
                                                 currencyExchangeRate?.RealtimeCurrencyExchangeRate?.AskPrice ?? 0,
                                                 currencyExchangeRate?.RealtimeCurrencyExchangeRate?.AskPrice ?? 0);

         var response = await mediator.Send(new AddExchangeRateCommand(request));

        return mapper.Map<GetExchangeRateResponse>(response);
    }
}
