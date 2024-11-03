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

        var response = mapper.Map<GetExchangeRateResponse>(exchage);

        return response;
    }
}
