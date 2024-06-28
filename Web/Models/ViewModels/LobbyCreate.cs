using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Web.Models.ViewModels;

public class LobbyCreate {
    [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
    [MaxLength(40, ErrorMessage = ErrorMessages.MaxLength)]
    [StringValidator(InvalidCharacters = "<>{}")]
    public string Name { get; set; }
}
