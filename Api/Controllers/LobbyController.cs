using Api.Models;
using Api.Model.HttpParam;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers;
[Route("[controller]")]
[ApiController]
public class LobbyController : ControllerBase {
    [HttpGet]
    public ActionResult<IEnumerable<Lobby>> GetAll() {
        return Ok("all");
    }

    [HttpGet("{id}")]
    public ActionResult<Lobby> Get(int id) {
        return Ok();
    }

    [HttpPost("{id}/join")]
    public IActionResult Join([FromBody] JoinLobby value) {
        return Ok();
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] CreateLobby value) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors).First().Exception?.Message);
        }
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
