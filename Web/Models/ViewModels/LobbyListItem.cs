using Microsoft.AspNetCore.Mvc;

namespace Web.Models.ViewModels;

public class LobbyListItem {
    [HiddenInput(DisplayValue = false)]
    public string Id { get; set; }
    public string Name { get; set; }
}
