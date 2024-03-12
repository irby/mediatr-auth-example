using Dappa.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dappa.Api.Filters;

public class ExceptionFilter: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {

        if (context.Exception.GetType().IsAssignableTo(typeof(RequestException)))
        {
            var exception = (RequestException) context.Exception;
            context.Result = new ContentResult
            {
                StatusCode = exception.HttpCode,
                Content = exception.Message,
            };
        }
        else
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}

public class BadRequestResult
{
    public string Message { get; set; }
}
