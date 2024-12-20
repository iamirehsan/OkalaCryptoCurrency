using OkalaCryptoCurrency.Api.Utility;
using OkalaCryptoCurrency.Domain.Base;
using OkalaCryptoCurrency.Domain.Interfaces.Log;
using OkalaCryptoCurrency.Infrastructure.Log;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        BaseResponse baseResponse;

        switch (exception)
        {
            case ManagedException ex:
                baseResponse = new BaseResponse(new Status(ex.ErrorMessage, (int)HttpStatusCode.BadRequest));
                _logger.WriteError("Managed exption.", exception);
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                var message = new string[] { "An unexpected error occurred." };  // Use real exception message
                baseResponse = new BaseResponse(new Status(message, (int)HttpStatusCode.InternalServerError));
                _logger.WriteError("Unhandle exption.", exception);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
        var jsonResponse = JsonSerializer.Serialize(baseResponse);
        await context.Response.WriteAsync(jsonResponse);


    }
}
