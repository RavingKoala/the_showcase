namespace Web.Services.Middleware;

public class ServerSentEventsClient {
    private readonly HttpResponse _response;

    internal ServerSentEventsClient(HttpResponse response) {
        _response = response;
    }

    public Task SendEventAsync(ServerSentEvent serverSentEvent) {
        return _response.WriteSseEventAsync(serverSentEvent);
    }
}