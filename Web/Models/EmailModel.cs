using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Web.Models {
    public class EmailModel {

        [Required]
        [StringLength(40)]
        [Display(Name = "Voornaam", Prompt= "Jan")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Achternaam", Prompt = "Schaap")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email adres", Prompt = "user@example.com")]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Onderwerp", Prompt = "contactverzoek")]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Email bericht", Prompt = "Hallo mijn naam is...")]
        public string Message { get; set; }

    }
}
