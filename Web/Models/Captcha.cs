using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class Captcha {
    [HiddenInput(DisplayValue = false)]
    public int Number1 { get; set; } = 0;

    [HiddenInput(DisplayValue = true)]
    public int Number2 { get; set; } = 0;

    [Required(ErrorMessage = "Please solve the Captcha!")]
    [Display(Name = "Captcha")]
    public int? UserAnswer { get; set; } = null;

    public void ReGenerateCaptcha() {
        Number1 = new Random().Next(1, 21);
        Number2 = new Random().Next(3, 21);
        UserAnswer = null;
    }

    public bool CheckAnswerValid() {
        return Number1 + Number2 == UserAnswer;
    }
}