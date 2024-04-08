// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authorization;
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
[AllowAnonymous]
public class ResendEmailConfirmationModel : PageModel {
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly IEmailSender _emailSender;

    public ResendEmailConfirmationModel(IHttpClientFactory httpClientFactory, IEmailSender emailSender) {
        _httpClientFactory = httpClientFactory;
        _emailSender = emailSender;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public void OnGet() {
    }

    public async Task<IActionResult> OnPostAsync() {
        if (!ModelState.IsValid) {
            return Page();
        }

        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");
        await httpClient.PostAsJsonAsync("Account/resendConfirmationEmail", Input);
        
        ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
        return Page();
    }
}
