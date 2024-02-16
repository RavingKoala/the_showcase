using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Models;

namespace Web.Controllers {
    public class ContactController : Controller {
        private ILogger _logger;
        private IHttpClientFactory _httpClientFactory;
        public ContactController(ILogger<ContactController> logger, IHttpClientFactory httpClientFactory) {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMail(EmailModel model) {
            var response = await _httpClientFactory.CreateClient("ApiHttpClient").GetAsync("/Mail");
            bool success = false;
            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                _logger.LogWarning("HTTPStatusCode 503: Mailservice is not working correctly, check api logs!");
            if (response.StatusCode == HttpStatusCode.OK) {
                _logger.LogInformation("Mail succesfully send.");
                success = true;
            }
            ViewBag.SubmitSuccess = success;
            if (!success)
                return View("Index", model);

            return View("Index");
        }
    }
}
