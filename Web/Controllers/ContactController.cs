using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Web.Models;

namespace Web.Controllers {
    public class ContactController : Controller {
        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(EmailModel model) {
            bool success = false;
            ViewBag.SubmitSuccess = success;
            if (!success)
                return View("Index", model);

            return View("Index");
        }
    }
}
