using System.ComponentModel.DataAnnotations;

namespace Web.Models {
    public class EmailModel {

        [Required]
        [StringLength(40)]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email adres")]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Onderwerp")]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Email bericht")]
        public string Message { get; set; }

    }
}
