// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Web.Areas.Identity.Pages.Account;
[AllowAnonymous]
public class RegisterConfirmationModel : PageModel {
    private readonly IHttpClientFactory _httpClientFactory;

    public RegisterConfirmationModel(IHttpClientFactory httpClientFactory, IEmailSender sender) {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> OnGetAsync(string userId, string code, string changedEmail, string returnUrl = null) {
        if (changedEmail == null) {
            return RedirectToPage("/Index");
        }
        returnUrl = returnUrl ?? Url.Content("~/");
        

        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");
        string endpointParams = $"userId={userId}&code=code&changedEmail=changedEmail";
        var response = await httpClient.GetAsync(("Account/resetPassword"+endpointParams));
        if (response.StatusCode == HttpStatusCode.OK) {
            return RedirectToPage("./ResetPasswordConfirmation");
        }
        if (response.StatusCode == HttpStatusCode.BadRequest) {
            return RedirectToPage("./ResetPasswordConfirmation");
        }

        var content = await response.Content.ReadAsStringAsync();


        return Page();
    }
}
