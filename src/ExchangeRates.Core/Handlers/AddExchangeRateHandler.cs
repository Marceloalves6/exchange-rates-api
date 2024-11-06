using AutoMapper;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Repositories;
using ExchangeRates.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExchangeRates.Core.Handlers;

public class AddExchangeRateHandler
(
    IUnitOfWork uow,
    IMapper mapper,
    IMessageQueueService messageQueueService,
    ILogger<AddExchangeRateHandler> logger
) : IRequestHandler<AddExchangeRateCommand, AddExchangeRateResponse>
{
    public async Task<AddExchangeRateResponse> Handle(AddExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var exchangeRate = mapper.Map<ExchangeRate>(request.AddExchangeRateRequest);
        var result = await uow.ExchangeRepository.AddAsync(exchangeRate);
        await uow.CommitAsync(cancellationToken);
        var response = mapper.Map<AddExchangeRateResponse>(result);

        await messageQueueService.SendAsync(response);

        logger.LogInformation("New exchange rate was added");

        return response;
    }
}
