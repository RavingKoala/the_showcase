using Microsoft.AspNetCore.Mvc;

namespace Web.Models.ViewModels;

public class LobbyListItem {
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }
    public string Name { get; set; }
}
