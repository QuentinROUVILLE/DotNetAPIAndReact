using System.Diagnostics;
using WebAPI.Helpers;

namespace WebAPI.Middlewares;

public class WebSocketMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;
        if (path != null && path.Contains("/api/v1/ws/"))
        {
            var taskId = path.Split("/").Last();

            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await TaskHelpers.HandleWebSocket(webSocket, taskId);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
        else
        {
            await next(context);
        }
    }
}
