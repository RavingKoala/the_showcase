// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;

namespace Web.Areas.Identity.Pages.Account;
public class RegisterModel : PageModel {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;

    public RegisterModel(IHttpClientFactory httpClientFactory, ILogger<RegisterModel> logger, IEmailSender emailSender) {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _emailSender = emailSender;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public class InputModel {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


    public async Task OnGetAsync(string returnUrl = null) {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
        returnUrl ??= Url.Content("~/");
        if (!ModelState.IsValid) 
            return Page();


        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");
        var response = await httpClient.PostAsJsonAsync("Account/register", Input);
        if (response.StatusCode != HttpStatusCode.OK) {
                return LocalRedirect(returnUrl);
        }
        if (response.StatusCode != HttpStatusCode.BadRequest) {
        }

        //foreach (var error in result.Errors) {
        //    ModelState.AddModelError(string.Empty, error.Description);
        //}

        return Page();
    }
}
