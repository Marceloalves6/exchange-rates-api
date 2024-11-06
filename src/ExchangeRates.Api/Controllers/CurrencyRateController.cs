using ExchangeRates.Api.Application.Contracts;
using ExchangeRates.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.Api.Controllers;
/// <summary>
/// Exchange rate API
/// </summary>

[Route("api/v1/[controller]")]
[ApiController]
public class CurrencyRateController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : BaseApiController<CurrencyRateController>(mediator, httpContextAccessor)
{

    /// <summary>
    /// Get the exchange rate of a pair of currencies
    /// </summary>
    /// <param name="request">A pair of currencies, currencyFrom and currency to in ISO format</param>
    /// <returns>An object containing the information about the exechange rate</returns>
    /// <remarks>
        /// All the parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only search by one parameter at a time
        ///  
        /// Sample request:
        ///
        ///     POST /Account
        ///     {
        ///        "userId": null,
        ///        "bankId": null,
        ///        "dateCreated": null
        ///     }
        ///     OR
        ///     
        ///     POST /Account
        ///     {
        ///        "userId": null,
        ///        "bankId": 000,
        ///        "dateCreated": null
        ///     } 
        /// </remarks>
        /// <param name="request"></param>
    
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(ServiceResponse<GetExchangeRateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByExchangeRate(GetExchangeRateRequest request, CancellationToken cancellationToken)
    {

        var response = await SendAsync(new GetExchangeRateCommand(request), cancellationToken);

        return Ok(response);
    }


    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(ServiceResponse<AddExchangeRateCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AddExchangeRateRequest resquest, CancellationToken cancellationToken)
    {
        var response = await SendAsync(new AddExchangeRateCommand(resquest), cancellationToken);

        return Ok(response);
    }

    [HttpPut]
    [Route("")]
    [ProducesResponseType(typeof(ServiceResponse<UpdateExchangeRateResquest>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateExchangeRateResquest resquest, CancellationToken cancellationToken)
    {
        var response = await SendAsync(new UpdateExchangeRateCommand(resquest), cancellationToken);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<NoResult>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool? hardDelete, CancellationToken cancellationToken)
    {
        var response = await SendAsync(new DeleteExchangeRateCommand(id, hardDelete.GetValueOrDefault(false)), cancellationToken);

        return Ok(response);
    }
}