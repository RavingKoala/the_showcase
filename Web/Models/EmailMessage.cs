using System.ComponentModel.DataAnnotations;

namespace Web.Models {
    public class EmailMessage {

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
        [MaxLength(40, ErrorMessage = ErrorMessages.MaxLength)]
        [Display(Name = "First name", Prompt = "Bob")]
        public string FirstName { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        [Display(Name = "Last name", Prompt = "Ross")]
        public string LastName { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLength)]
        [Display(Name = "Email address", Prompt = "user@example.com")]
        public string Email { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLength)]
        [Display(Name = "Subject", Prompt = "Request for ...")]
        public string Subject { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLength)]
        [Display(Name = "Email Message", Prompt = "Hello Stijn, I'd like to say ...")]
        public string Message { get; set; } = "";
    }
}
