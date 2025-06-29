﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Models.ViewModels;

namespace Web.Controllers;
public class ContactController : Controller {
    private ILogger _logger;
    private IHttpClientFactory _httpClientFactory;
    public ContactController(ILogger<ContactController> logger, IHttpClientFactory httpClientFactory) {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Index() {
        Mail mymodel = new Mail();

        mymodel.CaptchaModel.ReGenerateCaptcha();

        return View(mymodel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendMail(Mail model) {
        if (!ModelState.IsValid) {
            return View("Index", model);
        }

        if (!model.CaptchaModel.CheckAnswerValid()) {
            model.CaptchaModel.ReGenerateCaptcha();
            ViewBag.ErrorMessage = "Captcha answer is incorrect.";
            return View("Index", model);
        }

        model.CaptchaModel.ReGenerateCaptcha();

        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");

        try {
            var response = await httpClient.PostAsJsonAsync("Mail", model.EmailModel);

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
        } catch (Exception e) {
            _logger.LogError("PostAsJsonAsync returned error: " + e);
            ViewBag.ErrorMessage = "Something unexpected happened whilst trying to send your Mail. Please try again immediately, and if it still doesnt work try again later! I will try to solve your problem as soon as possible!";
            return View("Index", model);
        }


        _logger.LogInformation("Mail successfully sent.");
        ViewBag.SubmitSuccess = true;

        ModelState.Clear();

        Mail mailViewModel = new Mail();

        mailViewModel.CaptchaModel.ReGenerateCaptcha();

        return View("Index", mailViewModel);
    }
}
