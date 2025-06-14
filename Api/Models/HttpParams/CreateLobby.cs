using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Model.HttpParam;
public class CreateLobby {

    [Required]
    public required string UserToken { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [DefaultValue(true)]
    public bool PreferWhite { get; set; }

}
