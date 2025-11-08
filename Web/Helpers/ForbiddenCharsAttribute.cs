using System.ComponentModel.DataAnnotations;


namespace Web.Helpers {
    public class ForbidCharsAttribute : ValidationAttribute {
        private readonly string _forbiddenChars;

        public ForbidCharsAttribute(string forbiddenChars) {
            _forbiddenChars = forbiddenChars;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
            if (value == null) {
                return new ValidationResult("No Validation characters specified");
            }
            string? str = value as string;
            if (str != null && str.IndexOfAny(_forbiddenChars.ToCharArray()) >= 0) {
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} contains one of the forbidden characters: {_forbiddenChars}");
            }
            return ValidationResult.Success;
        }
    }

}
