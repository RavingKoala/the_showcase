using Api.Model.RequestParam;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace showcase_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase {
        [HttpGet("{lobbyId}")]
        public ActionResult<IEnumerable<string>> GetBoard(string lobbyId) {
            return Ok();
        }

        [HttpPost("{lobbyId}")]
        public IActionResult DoMove(int lobbyId, [FromBody] GameTurn value) {
            return Ok();
        }

        [HttpPost("{lobbyId}")]
        public IActionResult Forfeit(int lobbyId, [FromBody] ForfeitGame forfeit) {
            return Ok();
        }

        [HttpPut("{lobbyId}")]
        public IActionResult Leave(int lobbyId) {
            return Ok();
        }
    }
}
