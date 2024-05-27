namespace Middleware;

public class CustomMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public CustomMiddleware(RequestDelegate _requestDelegate)
    {
        this._requestDelegate = _requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Add("X-Custom-Answer", "Tak");
        await _requestDelegate(context);
    }
}