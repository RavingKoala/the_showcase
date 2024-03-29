namespace Web.Services.Middleware;

public class ServerSentEvent {
    public string? Id { get; set; }

    public string? Type { get; set; }

    public IList<string> Data { get; set; }
}

internal static class ServerSentEventsExtension {
    internal static async Task WriteSseEventAsync(this HttpResponse response, ServerSentEvent serverSentEvent) {
        if (!string.IsNullOrWhiteSpace(serverSentEvent.Id))
            await response.WriteSseEventFieldAsync("id", serverSentEvent.Id);

        if (!string.IsNullOrWhiteSpace(serverSentEvent.Type))
            await response.WriteSseEventFieldAsync("event", serverSentEvent.Type);

        if (serverSentEvent.Data != null) {
            foreach (string data in serverSentEvent.Data)
                if (!string.IsNullOrWhiteSpace(data))
                    await response.WriteSseEventFieldAsync("data", data);
        }

        await response.WriteSseEventBoundaryAsync();
        await response.Body.FlushAsync();
    }

    private static Task WriteSseEventFieldAsync(this HttpResponse response, string field, string data) {
        return response.WriteAsync($"{field}: {data}\n");
    }

    private static Task WriteSseEventBoundaryAsync(this HttpResponse response) {
        return response.WriteAsync("\n");
    }
}