namespace Web.Services;

public class EmailSenderOptions {
    public string? SmtpServerUsername { get; set; }
    public string? SmtpServerPassword { get; set; }

    public string adminEmail = "stijn.van.boesschoten@windesheim.nl";

    public EmailSenderOptions() {
        SmtpServerUsername = Environment.GetEnvironmentVariable("SMTPServerUsername");
        SmtpServerPassword = Environment.GetEnvironmentVariable("SMTPServerPassword");
    }
}
