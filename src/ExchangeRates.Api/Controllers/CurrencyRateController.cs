using Microsoft.AspNetCore.Mvc;
using MediatR;
using ExchangeRates.Core.Operations;
using ExchangeRates.Api.Application.Contracts;

namespace ExchangeRates.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CurrencyRateController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : BaseApiController<CurrencyRateController>(mediator, httpContextAccessor)
{
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(ServiceResponse<GetExchangeRateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByCurrency(GetExchangeRateRequest request, CancellationToken cancellationToken)
    {

        var response = await SendAsync(new GetExchangeRateRequest(request.CurrencyFrom, request.CurrencyTo), cancellationToken);

        return Ok(response);
    }


    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(ServiceResponse<AddExchangeRateResquest>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AddExchangeRateResquest resquest, CancellationToken cancellationToken)
    {

        var response = await SendAsync(resquest, cancellationToken);

        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(ServiceResponse<UpdateExchangeRateResquest>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateExchangeRateResquest resquest, CancellationToken cancellationToken)
    {
        var response = await SendAsync(resquest, cancellationToken);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool? hardDelete,  CancellationToken cancellationToken)
    {
        var response = await SendAsync(new DeleteExchangeRateRequest(id, hardDelete.GetValueOrDefault(false)), cancellationToken);

        return Ok(response);
    }
}
