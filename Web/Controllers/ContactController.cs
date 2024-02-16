using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
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
            HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.PostAsJsonAsync("Mail", model);

            if (response.StatusCode != HttpStatusCode.OK) {
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    _logger.LogWarning("HTTPStatusCode 503: Mailservice is not working correctly, check api logs!");
                else
                    _logger.LogWarning($"Unhandled ResponseCode {response.StatusCode}: {response.Content} From request: {response.RequestMessage}");

                return View("Index", model);
            }

            _logger.LogInformation("Mail successfully sent.");
            ViewBag.SubmitSuccess = true;
            
            ModelState.Clear();
            
            return View("Index");
        }
    }
}
