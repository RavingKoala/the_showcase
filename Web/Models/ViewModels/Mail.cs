namespace Web.Models.ViewModels;

public class Mail {
    public EmailMessage EmailModel { get; set; } = new EmailMessage();
    public Captcha CaptchaModel { get; set; } = new Captcha();
}