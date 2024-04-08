using System.Collections.Concurrent;

namespace Web.Services.Middleware;

public class ServerSentEventsService {
    private readonly ConcurrentDictionary<Guid, ServerSentEventsClient> _clients = new ConcurrentDictionary<Guid, ServerSentEventsClient>();

    internal Guid AddClient(ServerSentEventsClient client) {
        Guid clientId = Guid.NewGuid();

        _clients.TryAdd(clientId, client);

        return clientId;
    }

    internal void RemoveClient(Guid clientId) {
        _ = _clients.TryRemove(clientId, out _);
    }

    public Task SendEventAsync(ServerSentEvent serverSentEvent) {
        List<Task> clientsTasks = new List<Task>();
        foreach (ServerSentEventsClient client in _clients.Values) {
            clientsTasks.Add(client.SendEventAsync(serverSentEvent));
        }

        return Task.WhenAll(clientsTasks);
    }
}