using AutoMapper;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Repositories;
using MediatR;

namespace ExchangeRates.Core.Handlers;

internal class UpdateExchangeRateHandler(IUnitOfWork uow, IMapper mapper) : IRequestHandler<UpdateExchangeRateCommand, UpdateExchangeRateResponse>
{
    public async Task<UpdateExchangeRateResponse> Handle(UpdateExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var exchangeRate = await uow.ExchangeRepository.GetExternalById(request.UpdateExchangeRateResquest.Id);

        if (exchangeRate is null)
        {
            throw new Exception("Record not found");
        }

        exchangeRate.CurrencyFrom = request.UpdateExchangeRateResquest.CurrencyFrom;
        exchangeRate.CurrencyTo = request.UpdateExchangeRateResquest.CurrencyTo;
        exchangeRate.BidPrice = request.UpdateExchangeRateResquest.BidPrice; 
        exchangeRate.AskPrice = request.UpdateExchangeRateResquest.AskPrice;
        exchangeRate.UpdatedAt = DateTime.UtcNow;

        await uow.ExchangeRepository.UpdateAsync(exchangeRate);

        await uow.CommitAsync(cancellationToken);

        var response = mapper.Map<UpdateExchangeRateResponse>(exchangeRate);

        return response;
    }
}
