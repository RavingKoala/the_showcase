using System.ComponentModel.DataAnnotations;


namespace Api.Helpers {
    public class ForbiddenCharactersAttribute : ValidationAttribute {
        private readonly string _forbiddenChars;

        public ForbiddenCharactersAttribute(string forbiddenChars) {
            _forbiddenChars = forbiddenChars;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
            if (value == null) {
                return ValidationResult.Success;
            }
            string str = value as string;
            if (str.IndexOfAny(_forbiddenChars.ToCharArray()) >= 0) {
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.MemberName} contains one of the forbidden characters: {_forbiddenChars}");
            }
            return ValidationResult.Success;
        }
    }
}
