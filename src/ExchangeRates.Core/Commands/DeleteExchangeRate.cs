using MediatR;

namespace ExchangeRates.Core.Commands;

public record DeleteExchangeRateCommand(Guid ExternalId, bool HardDelete = false) : IRequest<Unit>;
