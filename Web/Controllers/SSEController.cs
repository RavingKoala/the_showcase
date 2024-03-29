using Microsoft.AspNetCore.Mvc;
using Web.Services.Middleware;

namespace Web.Controllers;

public class SSEController : Controller {
    private ServerSentEventsService SseService;

    public SSEController(ServerSentEventsService sseService) {
        SseService = sseService;
    }

    public class LobbyController : Controller {
        [HttpGet]
        [Route("Lobby/Connect")]
        //[HttpGet("Lobby/Connect")]
        public IActionResult LobbyConnect() {
            // Logic for Lobby Connect
            throw new NotImplementedException();

            return Content("Lobby Connected");
        }

        [HttpGet("Lobby/Disconnect")]
        public IActionResult LobbyDisconnect() {
            throw new NotImplementedException();

            // Logic for Lobby Disconnect
            return Content("Lobby Disconnected");
        }
    }

    public class GameController : Controller {
        [HttpGet("Game/Connect/{id}")]
        public IActionResult GameConnect(int id) {
            throw new NotImplementedException();

            // Logic for Game Connect with provided id
            return Content($"Game Connected with ID: {id}");
        }

        [HttpGet("Game/Connect/{id}")]
        public IActionResult LobbyDisconnect() {
            throw new NotImplementedException();

            // Logic for Game Disconnect
            return Content($"Game Disconnected");
        }
    }
}
