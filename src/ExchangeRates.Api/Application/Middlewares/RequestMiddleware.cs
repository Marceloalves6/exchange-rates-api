
using ExchangeRates.Api.Application.Models;

namespace ExchangeRates.Api.Application.Middlewares;

public class RequestMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Items.Add(HttpContextVariables.RequestId, Guid.NewGuid());
        context.Items.Add(HttpContextVariables.RequestStartedAt, DateTime.Now);

        await next(context);
    }
}
