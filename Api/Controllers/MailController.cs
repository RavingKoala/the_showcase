using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Web.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Humanizer.Configuration;

namespace Api.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class MailController : ControllerBase {
        private const string RecieverMail = "stijn.van.boesschoten@windesheim.nl";

        private IConfiguration _configuration;
        private ILogger _logger;

        public MailController(IConfiguration iConfig, ILogger<MailController> logger) {
           _configuration = iConfig;
           _logger = logger;
        }

        [HttpPost]
        public IActionResult SendMail([FromBody] EmailModel model) {
            _logger.LogInformation("Attempting to send mail.");

            IConfigurationSection mailsettings = _configuration.GetSection("MailSettings");
            // usersecrets, to set: see README in solution
            string? SmtpServerUsername = Environment.GetEnvironmentVariable("SMTPServer:Username");
            string? SmtpServerPassword = Environment.GetEnvironmentVariable("SMTPServer:Password");

            if (SmtpServerUsername == null || SmtpServerPassword == null) {
                _logger.LogCritical("Mail service not available, unable to get environment variables: SMTPServer:Username or SMTPServer:Password");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Unable to send any mail at this time, please try again later"); ;
            }

            SmtpClient client = new SmtpClient(mailsettings.GetValue<string>("SmtpServerHost"), mailsettings.GetValue<int>("SmtpServerPort")) {
                Credentials = new NetworkCredential(SmtpServerUsername, SmtpServerPassword),
                EnableSsl = true
            };

            string body = $"by {model.FirstName} {model.LastName}, \n\r{model.Message}";
            try {
                client.Send(model.Email, RecieverMail, model.Subject, body);
            } catch (Exception e) {
                _logger.LogWarning(e, "Mail failed to send.");
                return StatusCode(StatusCodes.Status424FailedDependency, "Failed to send mail, please try it again.");
            }

            _logger.LogInformation("Mail succesfully send.");
            return Ok("Mail succesfully send.");
        }
    }
}
