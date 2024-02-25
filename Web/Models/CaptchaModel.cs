using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class CaptchaModel {
    [HiddenInput(DisplayValue=false)]
    public int Number1 { get; set; } = 0;

    [HiddenInput(DisplayValue=false)]
    public int Number2 { get; set; } = 0;

    [Required(ErrorMessage = "Please solve the Captcha!")]
    [Display(Name = "Captcha")]
    public int? UserAnswer { get; set; } = null;

    public CaptchaModel(){
        ReGenerateCaptcha();
    }

    public void ReGenerateCaptcha() {
        Number1 = new Random().Next(1, 21);
        Number2 = new Random().Next(3, 21);
    }

    public bool CheckAnswerValid() {
        return Number1 + Number2 == UserAnswer;
    }
}
