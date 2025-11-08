using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Api.Helpers;
using Api.Models;

namespace Api.Model.HttpParam;
public class CreateLobby {

    [Required]
    public required string UserToken { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
    [MaxLength(40, ErrorMessage = ErrorMessages.MaxLength)]
    [ForbiddenCharacters("<>{}[]()", ErrorMessage = "Invalid characters: ")]
    public required string Name { get; set; }

    [Required]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLength)]
    public required string Description { get; set; }

    [DefaultValue(true)]
    public bool PreferWhite { get; set; }

}
