using System.ComponentModel.DataAnnotations;

namespace The_Showcase.Models {
    public class EmailModel {

        [Required]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email adres")]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Email bericht")]
        public string Message { get; set; }

    }
}
