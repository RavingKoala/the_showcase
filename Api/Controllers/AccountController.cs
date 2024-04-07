using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : Controller {
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(ILogger<AccountController> logger, SignInManager<IdentityUser> signInManager) {
        _logger = logger;
        _signInManager = signInManager;
    }

    [HttpPost("logout")]
	[Authorize]
	public async Task<IActionResult> Logout() {
        await _signInManager.SignOutAsync();

        return Ok();
	}
}
