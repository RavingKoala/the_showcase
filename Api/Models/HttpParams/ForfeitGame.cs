using System.ComponentModel.DataAnnotations;

namespace Api.Model.HttpParam;
public class ForfeitGame {
    [Required]
    public bool Confirmed { get; set; }
}
