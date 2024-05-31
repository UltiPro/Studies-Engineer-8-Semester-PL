using System.Text;

namespace Middleware;

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

                        // Dodaj użytkownika i hasło do nagłówków odpowiedzi
                        context.Response.OnStarting(() =>
                        {
                            context.Response.Headers.Add("X-Username", username);
                            context.Response.Headers.Add("X-Password", password);
                            return Task.CompletedTask;
                        });

                        // Kontynuuj przetwarzanie żądania
                        await _next(context);
                        return;
                    }
                }
                catch (FormatException)
                {
                    // Obsługa błędów dekodowania Base64
                    context.Response.StatusCode = 400; // Bad Request
                    await context.Response.WriteAsync("Invalid Authorization Header");
                    return;
                }
            }
        }

        // Jeśli autoryzacja nie powiodła się, zwracamy 401 Unauthorized
        context.Response.StatusCode = 401;
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        await context.Response.WriteAsync("Unauthorized");
    }
}
