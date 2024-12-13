using WebAPI.Middlewares;

namespace WebAPI.Extensions;

public static class WebSocketExtensions
{
    public static void UseWebSocketEndpoint(this IApplicationBuilder app)
    {
        app.UseMiddleware<WebSocketMiddleware>();
    }
}
