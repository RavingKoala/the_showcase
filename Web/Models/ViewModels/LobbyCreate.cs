using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class LobbyCreate {
    [Required]
    public string Name { get; set; }
}
