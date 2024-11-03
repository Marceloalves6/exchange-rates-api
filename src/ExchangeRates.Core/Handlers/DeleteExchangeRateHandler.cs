using ExchangeRates.Core.Operations;
using ExchangeRates.Core.Repositories;
using MediatR;

namespace ExchangeRates.Core.Handlers;

public class DeleteExchangeRateHandler(IUnitOfWork uow) : IRequestHandler<DeleteExchangeRateRequest, Unit>
{
    public async Task<Unit> Handle(DeleteExchangeRateRequest request, CancellationToken cancellationToken)
    {
        var exchageRate = await uow.ExchangeRepository.GetExternalById(request.ExternalId);

        if (exchageRate is null)
        {
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

        return Unit.Value;
    }
}
