using AutoMapper;
using ExchangeRates.Core.Operations;
using ExchangeRates.Core.Repositories;
using MediatR;

namespace ExchangeRates.Core.Handlers;

public class GetExchangeRateHandler(IUnitOfWork uow, IMapper mapper) : IRequestHandler<GetExchangeRateRequest, GetExchangeRateResponse>
{
    public async Task<GetExchangeRateResponse> Handle(GetExchangeRateRequest request, CancellationToken cancellationToken)
    {
        var exchage = await uow.ExchangeRepository.GetAsync(request.CurrencyFrom, request.CurrencyTo);

        if (exchage is null)
        {
            
        }

        var response = mapper.Map<GetExchangeRateResponse>(exchage);

        return response;
    }
}
/*
 Your API Key: ed2a70cbe4f595965df8527b
 Example Request: https://v6.exchangerate-api.com/v6/ed2a70cbe4f595965df8527b/latest/USD
*/

/*
 https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=USD&to_currency=EUR&apikey=U23W00043X08VE9U
 */