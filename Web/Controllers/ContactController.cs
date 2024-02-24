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
            var captchaModel = new CaptchaModel {
                Number1 = new Random().Next(1, 21),
                Number2 = new Random().Next(1, 21)
            };
            ViewBag.CaptchaModel = captchaModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMail(EmailModel model, CaptchaModel captchaModel) {
            var newCaptchaModel = new CaptchaModel {
                Number1 = new Random().Next(1, 21),
                Number2 = new Random().Next(1, 21)
            };
            ViewBag.CaptchaModel = newCaptchaModel;

            if ((captchaModel.Number1 + captchaModel.Number2) != captchaModel.UserAnswer) {
                ViewBag.ErrorMessage = "Captcha answer is incorrect.";
                return View("Index", model);
            }

            HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");
            var response = await httpClient.PostAsJsonAsync("Mail", model);

            if (response.StatusCode != HttpStatusCode.OK) {
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable
                    || response.StatusCode == HttpStatusCode.FailedDependency) {
                    _logger.LogWarning("HTTPStatusCode 503: Mailservice is not working correctly, check api logs!");
                    ViewBag.ErrorMessage = "Mail Server is not working correctly. Please try again later!";
                } else { 
                    _logger.LogWarning($"Unhandled ResponseCode {response.StatusCode}: {response.Content} From request: {response.RequestMessage}");
                    ViewBag.ErrorMessage = "Something unexpected happened whilst trying to send your Mail. Please try again immediately, and if it still doesnt work try again later! I will try to solve your problem as soon as possible!";
                }

                return View("Index", model);
            }

            _logger.LogInformation("Mail successfully sent.");
            ViewBag.SubmitSuccess = true;
            
            ModelState.Clear();
            
            return View("Index");
        }
    }
}
