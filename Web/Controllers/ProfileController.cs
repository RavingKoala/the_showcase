using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models.ViewModels;

namespace Web.Controllers {
    public class ProfileController : Controller {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}