using DVL_QuoteQuiz.WebUI.Models;
using System.ComponentModel.DataAnnotations;

namespace DVL_QuoteQuiz.WebUI.Attributes
{
    public class QuoteAuthorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            value switch
            {
                QuoteAuthor { } val => IsValid(val) ? ValidationResult.Success! : new ValidationResult("Author's FullName or Id must not be null"),
                _ => new ValidationResult("QuoteAuthor must not be null")
            };

        public static bool IsValid(QuoteAuthor author) => author.FullName != null || author.Id.HasValue;
    }
}
