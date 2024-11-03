using MediatR;

namespace ExchangeRates.Core.Operations;

public record DeleteExchangeRateRequest(Guid ExternalId, bool HardDelete = false) : IRequest<Unit>;
