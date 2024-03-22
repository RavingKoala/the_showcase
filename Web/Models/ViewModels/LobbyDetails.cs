using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class LobbyDetails {
    [HiddenInput(DisplayValue = false)]
    public string Id { get; set; }
    public string Name { get; set; }

    public bool isHost { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string Player1Id { get; set; } // host
    [Display(Name= "Player 1: ")]
    public string Player1Name { get; set; } // host

    [HiddenInput(DisplayValue = false)]
    public string? Player2Id { get; set; }
    [Display(Name= "Player 2: ")]
    public string? Player2Name { get; set; } // host
}
