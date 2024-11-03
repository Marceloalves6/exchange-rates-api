using AutoMapper;
using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Operations;
using ExchangeRates.Core.Repositories;
using MediatR;

namespace ExchangeRates.Core.Handlers;

internal class AddExchangeRateHandler(IUnitOfWork uow, IMapper mapper) : IRequestHandler<AddExchangeRateResquest, AddExchangeRateResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<AddExchangeRateResponse> Handle(AddExchangeRateResquest request, CancellationToken cancellationToken)
    {
        var exchangeRate = mapper.Map<ExchangeRate>(request);
        var result = await _uow.ExchangeRepository.AddAsync(exchangeRate);
        await _uow.CommitAsync(cancellationToken);
        var response = mapper.Map<AddExchangeRateResponse>(result);

        return response;
    }
}
