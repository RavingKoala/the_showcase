using Api.Model;
using Api.Model.RequestParam;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace showcase_api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
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

        [HttpDelete("{id}")]
        public IActionResult Leave(int id) {
            return Ok();
        }
    }
}
