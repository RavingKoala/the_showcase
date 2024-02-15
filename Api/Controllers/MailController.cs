using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Web.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Humanizer.Configuration;

namespace Api.Controllers {
	public class MailController : Controller {
		private const string RecieverMail = "stijn.van.boesschoten@windesheim.nl";

		private IConfiguration _configuration;

		public MailController(IConfiguration iConfig) {
		   _configuration = iConfig;
		}

		[HttpPost]
		public IActionResult SendMail([FromBody] EmailModel model) {
			IConfigurationSection mailsettings = _configuration.GetSection("MailSettings");
			//var client = new SmtpClient(mailsettings.GetValue<string>("SmtpServerHost"), mailsettings.GetValue<int>("SmtpServerPort")) {
			//	Credentials = new NetworkCredential("", ""),
			//	EnableSsl = true
			//};
			//if (false)
			//	return BadRequest();
			
			//if (false)
			//	return NotFound();

			//string subject = "The Showcase - " + model.FirstName + " " + model.LastName;
			//client.Send(model.Email, RecieverMail, subject, "testbody");
			
			var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525) {
				Credentials = new NetworkCredential("4b74cdf013b9a0", "d59fbc2af1052c"),
				EnableSsl = true
			};
			client.Send("from@example.com", "to@example.com", "Hello world", "testbody");

			return Ok();
		}
	}
}
