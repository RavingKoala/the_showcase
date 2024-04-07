using Api.Model.HttpParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class GameController : ControllerBase {
    [HttpGet("{lobbyId}/board")]
    public ActionResult<IEnumerable<string>> GetBoard(string lobbyId) {
        return Ok();
    }

    [HttpPost("{lobbyId}/doMove")]
    public IActionResult DoMove(int lobbyId, [FromBody] GameTurn value) {
        return Ok();
    }

    [HttpPost("{lobbyId}/forfeit")]
    public IActionResult Forfeit(int lobbyId, [FromBody] ForfeitGame forfeit) {
        return Ok();
    }

    [HttpPut("{lobbyId}/leave")]
    public IActionResult Leave(int lobbyId) {
        return Ok();
    }
}
