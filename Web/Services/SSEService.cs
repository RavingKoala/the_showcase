using System.Collections.Concurrent;
using Web.Services.Middleware;

namespace Web.Services;

public abstract class SSEService<T> {
    private readonly ConcurrentDictionary<Guid, ServerSentEventsClient> _clients = new ConcurrentDictionary<Guid, ServerSentEventsClient>();

    internal Guid AddClient(ServerSentEventsClient client) {
        Guid clientId = Guid.NewGuid();

        _clients.TryAdd(clientId, client);

        return clientId;
    }

    internal void RemoveClient(Guid clientId) {
        ServerSentEventsClient client;

        _clients.TryRemove(clientId, out client);
    }
}
