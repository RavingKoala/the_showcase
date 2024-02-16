using Api.Model;
using Api.Model.HttpParam;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase {
        [HttpGet]
        public ActionResult<IEnumerable<Lobby>> GetAll() {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Lobby> Get(int id) {
            return Ok();
        }

        [HttpPost("{id}/join")]
        public IActionResult Join([FromBody] JoinLobby value) {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateLobby value) {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult ChangeDetails(int id, [FromBody] CreateLobby value) {
            return Ok();
        }

        [HttpPut("{id}/leave")]
        public IActionResult Leave(int id) {
            return Ok();
        }
    }
}
