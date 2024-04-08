// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Net.Http;

namespace Web.Areas.Identity.Pages.Account;
public class LogoutModel : PageModel {
    IHttpClientFactory _httpClientFactory;
    private readonly ILogger<LogoutModel> _logger;

    public LogoutModel(IHttpClientFactory httpClientFactory, ILogger<LogoutModel> logger) {
         _httpClientFactory = httpClientFactory;
        _logger = logger;
	}

    public async Task<IActionResult> OnPost(string returnUrl = null) {
        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");
        var _ = await httpClient.PostAsJsonAsync("/Account/logout");
        

        _logger.LogInformation("User logged out.");
        if (returnUrl != null) {
            return LocalRedirect(returnUrl);
        } else {
            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return RedirectToPage();
        }
    }
}
