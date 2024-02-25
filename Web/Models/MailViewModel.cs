namespace Web.Models;

public class MailViewModel {
    public EmailModel EmailModel { get; set; } = new EmailModel();
    public CaptchaModel CaptchaModel { get; set; } = new CaptchaModel();
}
