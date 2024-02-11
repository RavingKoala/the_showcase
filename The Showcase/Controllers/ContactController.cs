﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using The_Showcase.Models;

namespace The_Showcase.Controllers {
    public class ContactController : Controller {
        [HttpGet]
        public IActionResult Index() {
            TempData.TryGetValue("SubmitSuccess", out object? submitSuccess);
            ViewBag.SubmitSuccess = submitSuccess as bool? ?? false;
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(EmailModel model) {
            TempData["SubmitSuccess"] = true;
            return RedirectToAction("Index");
        }
    }
}
