using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExchangeRates.Core.Handlers;

public class DeleteExchangeRateHandler
(
    IUnitOfWork uow,
    ILogger<DeleteExchangeRateHandler> logger
) : IRequestHandler<DeleteExchangeRateCommand, Unit>
{
    public async Task<Unit> Handle(DeleteExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var exchageRate = await uow.ExchangeRepository.GetExternalById(request.ExternalId);

        if (exchageRate is null)
        {
            logger.LogError($"It was not possible to find a exchange rate with Id : {request.ExternalId}");

            throw new Exception("Record not found");
        }

        if (request.HardDelete)
        {
            await uow.ExchangeRepository.DeleteAsync(exchageRate);
        }
        else
        {
            exchageRate.Deleted = true;
        }

        await uow.CommitAsync(cancellationToken);

        logger.LogInformation($"The exchange rate with id: {request.ExternalId} was deleted");

        return Unit.Value;
    }
}
