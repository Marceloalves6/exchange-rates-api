using ExchangeRates.Api.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.Api.Controllers;

public class BaseApiController<TDerivedClass>(IMediator mediator) : ControllerBase
{

    public async Task<ServiceResponse<TResult>> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        return await SendMessageAsync<IRequest<TResult>, TResult>(request, cancellationToken);
    }

    private async Task<ServiceResponse<TResult>> SendMessageAsync<IRequest,TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        var startRequest = DateTime.Now; 
        var endrequest = DateTime.Now;
        return new ServiceResponse<TResult>
        {
            Success = true,
            Result = (TResult?)result,
            ProcessTime = new()
            {
                StartRequest = startRequest,
                EndRequest = DateTime.Now,
                ExecutionTimeMilliseconds = (endrequest - startRequest).TotalMilliseconds
            }
        };
    }
}
