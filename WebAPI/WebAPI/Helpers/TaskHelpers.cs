using System.Collections.Concurrent;
using System.Text;

namespace WebAPI.Helpers;

public static class TaskHelpers
{
    private static readonly ConcurrentDictionary<string, MemoryStream> Tasks = new();
    private static readonly Random Random = new();

    public static string StartTask(string serviceName, string action)
    {
        var taskId = Guid.NewGuid().ToString();
        var memoryStream = new MemoryStream();
        Tasks[taskId] = memoryStream;

        Task.Run(async () =>
        {
            for (var i = 0; i < 180; i++)
            {
                var log = $"[{DateTime.UtcNow}] - {serviceName} execute {action}\n";
                var bytes = Encoding.UTF8.GetBytes(log);
                await memoryStream.WriteAsync(bytes);
                await memoryStream.FlushAsync();
                await Task.Delay(1000);
            }
        });

        return taskId;
    }

    public static async Task HandleWebSocket(System.Net.WebSockets.WebSocket webSocket, string taskId)
    {
        if (!Tasks.TryGetValue(taskId, out var memoryStream))
        {
            await webSocket.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "Task not found", CancellationToken.None);
            return;
        }

        long lastPosition = 0;
        while (webSocket.State == System.Net.WebSockets.WebSocketState.Open)
        {
            memoryStream.Position = lastPosition;
            if (memoryStream.Length > lastPosition)
            {
                var bufferSize = (int)(memoryStream.Length - lastPosition);
                var buffer = new byte[bufferSize];
                await memoryStream.ReadAsync(buffer.AsMemory(0, bufferSize));
                lastPosition = memoryStream.Position;
                
                await webSocket.SendAsync(buffer, System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
            }
            else
            {
                await Task.Delay(500);
            }
        }
    }
}
