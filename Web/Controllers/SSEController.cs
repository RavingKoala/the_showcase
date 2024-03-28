using Microsoft.AspNetCore.Mvc;
using Web.Services.Middleware;

namespace Web.Controllers;
public class SSEController : Controller {

    [HttpGet]
    [Route("Lobby/Connect")]
    public IActionResult LobbyConnect() {


        return Ok();
    }
}
