using Server.Exceptions;
using System.Text;

namespace Server.Middleware;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();
                try
                {
                    var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var credentials = credentialString.Split(':');

                    if (credentials.Length == 2)
                    {
                        var username = credentials[0];
                        var password = credentials[1];

                        if(username != "admin" && password != "admin")
                        {
                            throw new NotAuthorizedException();
                        }

                        await _next(context);
                        return;
                    }
                }
                catch (FormatException)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid Authorization Header");
                    return;
                }
                catch (NotAuthorizedException e) 
                {
                    context.Response.StatusCode = 401;
                    context.Response.Headers["WWW-Authenticate"] = "Basic";
                    await context.Response.WriteAsync(e.Message);
                    return;
                }
            }
        }

        context.Response.StatusCode = 401;
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        await context.Response.WriteAsync("Unauthorized");
    }
}
