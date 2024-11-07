using ExchangeRates.Api.Application.Contracts;
using ExchangeRates.Api.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.Api.Controllers;

public class BaseApiController<TDerivedClass>(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    public async Task<ServiceResponse<TResult>> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        return await SendMessageAsync<IRequest<TResult>, TResult>(request, cancellationToken);
    }

    private async Task<ServiceResponse<TResult>> SendMessageAsync<IRequest, TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        var startRequest = (DateTime?)httpContextAccessor?.HttpContext?.Items[HttpContextVariables.RequestStartedAt];
        var endRequest = DateTime.Now;
        
        return new ServiceResponse<TResult>
        {
            Id = httpContextAccessor?.HttpContext?.Items[HttpContextVariables.RequestId]?.ToString(),
            Success = true,
            Result = (TResult?)result,
            ProcessTime = new()
            {
                StartRequest = startRequest.GetValueOrDefault(),
                EndRequest = DateTime.Now,
                ExecutionTimeMilliseconds = (endRequest - startRequest.GetValueOrDefault()).TotalMilliseconds
            }
        };
    }
}