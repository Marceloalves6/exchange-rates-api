using AutoMapper;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExchangeRates.Core.Handlers;

public class UpdateExchangeRateHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateExchangeRateHandler> logger) : IRequestHandler<UpdateExchangeRateCommand, UpdateExchangeRateResponse>
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

        logger.LogInformation("Exchange rate updated");

        return response;
    }
}
