using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Web.Services;

public class EmailSender : IEmailSender {

    private readonly ILogger _logger;
    private readonly EmailSenderOptions _options;
    private readonly IConfiguration _configuration;

    public EmailSender(ILogger<EmailSender> logger, IConfiguration configuration, IOptions<EmailSenderOptions> optionsAccessor) {
        _logger = logger;
        _options = optionsAccessor.Value;
        _configuration = configuration;
    }


    public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
        await SendEmailAsync(_options.adminEmail, email, subject, htmlMessage);
    }

    public async Task SendEmailToAdminAsync(string fromEmail, string subject, string htmlMessage) {
        await SendEmailAsync(fromEmail, _options.adminEmail, subject, htmlMessage);
    }

    public async Task SendEmailAsync(string fromEmail, string toEmail, string subject, string message) {
        string? smtpServerUsername = _options.SmtpServerUsername;
        string? smtpServerPassword = _options.SmtpServerPassword;

        if (string.IsNullOrEmpty(smtpServerUsername)
            || string.IsNullOrEmpty(smtpServerPassword)) {
            _logger.LogCritical("Mail service not available, unable to get environment variables: SmtpServerPassword or SMTPServer:Password");
            throw new Exception("unable to get environment variables: SMTPServer:Username or SMTPServer:Password");
        }

        await Execute(smtpServerUsername, smtpServerPassword, fromEmail, toEmail, subject, message);
    }

    public async Task Execute(string smtpServerUsername, string smtpServerPassword, string fromEmail, string toEmail, string subject, string message) {
        return;
        IConfigurationSection mailsettings = _configuration.GetSection("MailSettings");

        SmtpClient client = new SmtpClient(mailsettings.GetValue<string>("SmtpServerHost"), mailsettings.GetValue<int>("SmtpServerPort")) {
            Credentials = new NetworkCredential(smtpServerUsername, smtpServerPassword),
            EnableSsl = true
        };

        try {
            await client.SendMailAsync(fromEmail, toEmail, subject, message);
        } catch (Exception e) {
            _logger.LogWarning(e, "Mail failed to send.");
        }
    }
}
