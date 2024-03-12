using Dappa.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dappa.Api.Filters;

public class ExceptionFilter: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception as RequestException;
        if (exception?.Type == RequestException.ClassName)
        {
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
