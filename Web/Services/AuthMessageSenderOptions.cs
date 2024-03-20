namespace Web.Services;

public class AuthMessageSenderOptions {
    public string? SmtpServerUsername { get; set; }
    public string? SmtpServerPassword { get; set; }

    public string adminEmail = "stijn.van.boesschoten@windesheim.nl";

    public AuthMessageSenderOptions() {
        SmtpServerUsername = Environment.GetEnvironmentVariable("SMTPServerUsername");
        SmtpServerPassword = Environment.GetEnvironmentVariable("SMTPServerPassword");
    }
}
