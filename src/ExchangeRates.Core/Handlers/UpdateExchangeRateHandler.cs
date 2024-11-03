using AutoMapper;
using ExchangeRates.Core.Operations;
using ExchangeRates.Core.Repositories;
using MediatR;

namespace ExchangeRates.Core.Handlers;

internal class UpdateExchangeRateHandler(IUnitOfWork uow, IMapper mapper) : IRequestHandler<UpdateExchangeRateResquest, UpdateExchangeRateResponse>
{
    public async Task<UpdateExchangeRateResponse> Handle(UpdateExchangeRateResquest request, CancellationToken cancellationToken)
    {
        var exchangeRate = await uow.ExchangeRepository.GetExternalById(request.Id);

        if (exchangeRate is null)
        {
            throw new Exception("Record not found");
        }

        exchangeRate.CurrencyFrom = request.CurrencyFrom;
        exchangeRate.CurrencyTo = request.CurrencyTo;
        exchangeRate.PriceBid = request.PriceBid; 
        exchangeRate.PriceAsk = request.PriceAsk;
        exchangeRate.UpdatedAt = DateTime.UtcNow;

        await uow.ExchangeRepository.UpdateAsync(exchangeRate);

        await uow.CommitAsync(cancellationToken);

        var response = mapper.Map<UpdateExchangeRateResponse>(exchangeRate);

        return response;
    }
}
