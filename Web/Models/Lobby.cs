using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web.Models {

    public class Lobby {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IdentityUser Player1 { get; set; } // host
        public IdentityUser? Player2 { get; set; }
    }

}