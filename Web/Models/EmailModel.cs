using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Web.Models {
    public class EmailModel {

        [Required]
        [StringLength(40)]
        [Display(Name = "First name", Prompt= "Bob")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last name", Prompt = "Ross")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email address", Prompt = "user@example.com")]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Subject", Prompt = "Request for ...")]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Email Message", Prompt = "Hello Stijn, I'd like to say ...")]
        public string Message { get; set; }

    }
}
