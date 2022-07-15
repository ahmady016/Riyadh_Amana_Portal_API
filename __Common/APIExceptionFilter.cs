using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Common;

public class APIExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<APIExceptionFilter> _logger;

    public APIExceptionFilter(ILogger<APIExceptionFilter> logger)
    {
        _logger = logger;
    }
    public override void OnException(ExceptionContext exceptionContext)
    {
        string _errorMessage;
        if (exceptionContext.Exception is HttpRequestException _httpException)
        {
            _errorMessage = _httpException.Message;
            _logger.LogError(_errorMessage);
            exceptionContext.Result = _httpException.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundObjectResult(_errorMessage),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(_errorMessage),
                HttpStatusCode.Conflict => new ConflictObjectResult(_errorMessage),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(_errorMessage),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(_errorMessage),
                _ => new BadRequestObjectResult(_errorMessage),
            };
        }
        else
        {
            _errorMessage = $"{exceptionContext.Exception.Message}|_|{exceptionContext.Exception.InnerException?.Message}|_|{exceptionContext.Exception.InnerException?.InnerException?.Message}";
            _logger.LogError(_errorMessage);
            exceptionContext.Result = new BadRequestObjectResult(_errorMessage);
        }
    }
}
