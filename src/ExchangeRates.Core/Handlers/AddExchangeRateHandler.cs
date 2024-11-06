using AutoMapper;
using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Commands;
using ExchangeRates.Core.Repositories;
using MediatR;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRates.Test")]
namespace ExchangeRates.Core.Handlers;

internal class AddExchangeRateHandler(IUnitOfWork uow, IMapper mapper) : IRequestHandler<AddExchangeRateCommand, AddExchangeRateResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<AddExchangeRateResponse> Handle(AddExchangeRateCommand request, CancellationToken cancellationToken)
    {
        var exchangeRate = mapper.Map<ExchangeRate>(request.AddExchangeRateRequest);
        var result = await _uow.ExchangeRepository.AddAsync(exchangeRate);
        await _uow.CommitAsync(cancellationToken);
        var response = mapper.Map<AddExchangeRateResponse>(result);

        return response;
    }
}
