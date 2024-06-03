using Server.Exceptions;
using System.Net;

namespace Server.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ExceptionMiddleware(RequestDelegate _requestDelegate) => this._requestDelegate = _requestDelegate;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "text/plain";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        switch (ex)
        {
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
            case MotorbikeReservedException or MotorbikeNotReservedException
                 or MotorbikeCannotBeRentException or ThisMotorbikeIsNotRentedException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            case CouldNotCreateInvoiceException:
                statusCode = HttpStatusCode.Conflict;
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(ex.Message);
    }
}
