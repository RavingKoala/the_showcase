using Microsoft.AspNetCore.Mvc;
using Web.Services.Middleware;

namespace Web.Controllers;

public class SSEController : Controller {
    private ServerSentEventsService _sseService;

    public SSEController(ServerSentEventsService sseService) {
        _sseService = sseService;
    }

    [HttpGet]
    [Route("Lobbies/Connect")]
    public IActionResult LobbyConnect() {
        // Logic for Lobby Connect

        return Content("Lobby Connected");
    }

    private void SendEvent(HttpResponse response, string key, string data) {
        SendEvent(response, key, new List<string> { data });
    }
    private void SendEvent(HttpResponse response, string key, IList<string> data) {
        _sseService.SendEventAsync(new ServerSentEvent { Type = key, Data = data });
    }

    [HttpGet("Game/Connect/{id}")]
    public IActionResult GameConnect(int id) {
        throw new NotImplementedException();

        // Logic for Game Connect with provided id
        return Content($"Game Connected with ID: {id}");
    }
}
