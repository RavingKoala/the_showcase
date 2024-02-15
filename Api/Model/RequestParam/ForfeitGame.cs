using System.ComponentModel.DataAnnotations;

namespace Api.Model.RequestParam {
    public class ForfeitGame {
        [Required]
        public bool Confirmed { get; set; }
    }
}
