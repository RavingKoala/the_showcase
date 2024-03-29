using Azure;

namespace Web.Services.Middleware;

public class ServerSentEventsMiddleware {
    private readonly RequestDelegate _next;
    private readonly ServerSentEventsService _serverSentEventsService;

    public ServerSentEventsMiddleware(RequestDelegate next, ServerSentEventsService serverSentEventsService) {
        _next = next;
        _serverSentEventsService = serverSentEventsService;
    }

    public Task InvokeAsync(HttpContext context) {
        if (context.Request.Headers["Accept"] == "text/event-stream") {
            context.Response.ContentType = "text/event-stream";
            context.Response.Body.FlushAsync();

            ServerSentEventsClient client = new ServerSentEventsClient(context.Response);
            Guid clientId = _serverSentEventsService.AddClient(client);

            context.RequestAborted.WaitHandle.WaitOne();

            _serverSentEventsService.RemoveClient(clientId);

            return Task.FromResult(true);
        } else {
            return _next(context);
        }
    }
}

public static class ServerSentEventsMiddlewareExtensions {
    public static IApplicationBuilder UseServerSentEvents(this IApplicationBuilder builder) {
        return builder.UseMiddleware<ServerSentEventsMiddleware>();
    }
}
