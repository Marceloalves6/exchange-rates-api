using ExchangeRates.Api.Application.Contracts;
using ExchangeRates.Api.Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExchangeRates.Api.Application.Filters;

public class HttpExceptionHandlerFilter(ILogger<HttpExceptionHandlerFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var requestId = context.HttpContext.Items[HttpContextVariables.RequestId]?.ToString();

        logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                "[RequestId '{RequestId}']: {Message}",
                requestId,
                context.Exception.Message);

        IEnumerable<string> messages;

        bool isBadRequest = false;

        if (context.Exception.InnerException is ValidationException validationException)
        {
            messages = validationException.Errors.Select(i => i.ErrorMessage);
            isBadRequest = true;
        }
        else
        {
            messages = GetExceptionMessage(context.Exception);
        }

        var requestStart = ((DateTime?)context.HttpContext.Items[HttpContextVariables.RequestStartedAt]).GetValueOrDefault();
        var requestEnd = DateTime.Now;

        ServiceResponse<NoResult> result = new()
        {
            Id = requestId,
            Success = false,
            ProcessTime = new()
            {
                StartRequest = requestStart,
                EndRequest = requestEnd,
                ExecutionTimeMilliseconds = (requestEnd - requestStart).TotalMilliseconds,
            },
            Errors = new()
            {
                ["message"] = string.Join(Environment.NewLine, messages),
                ["statusCode"] = isBadRequest ? Convert.ToString(StatusCodes.Status400BadRequest) : Convert.ToString(StatusCodes.Status500InternalServerError)
            }
        };

        context.Result = new ObjectResult(result);
        context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        context.ExceptionHandled = true;
    }

    private static IEnumerable<string> GetExceptionMessage(Exception exception)
    {
        yield return exception.Message;

        if (exception.InnerException != null)
            foreach (var message in GetExceptionMessage(exception.InnerException))
                yield return message;
    }
}