using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common;

public class APIExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<APIExceptionFilter> _logger;

    public APIExceptionFilter(ILogger<APIExceptionFilter> logger)
    {
        _logger = logger;
    }
    public override void OnException(ExceptionContext actionContext)
    {
        var errorMessage = $"{actionContext.Exception.Message}|_|{actionContext.Exception.InnerException?.Message}|_|{actionContext.Exception.InnerException?.InnerException?.Message}";
        _logger.LogError(errorMessage);
        actionContext.Result = new BadRequestObjectResult(errorMessage);
    }
}
