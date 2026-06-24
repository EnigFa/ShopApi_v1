using System.Text;
using System.Text.Json;
using Shop.Domain.Models;

namespace Shop.App;

public class UserCheckMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == "POST" && context.Request.Path == "/api/user/register")
        {
            context.Request.EnableBuffering();

            var reader = new StreamReader(context.Request.Body, Encoding.UTF8, false, 1024, true);
            var body = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;

            var user = JsonSerializer.Deserialize<User>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (user == null || user.Id != 1 || user.Login != "admin")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "No authorization"
                });
                return;
            }
        }

        await next(context);
    }
}