using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class LobbyListItem {
    [HiddenInput(DisplayValue = false)]
    public string Id { get; set; }
    public string Name { get; set; }
}
