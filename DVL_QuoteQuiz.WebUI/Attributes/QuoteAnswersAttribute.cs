using DVL_QuoteQuiz.WebUI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DVL_QuoteQuiz.WebUI.Attributes
{
    public class QuoteAnswersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            value switch
            {
                IList<QuoteAnswerViewModel> { } val => IsValid(val),
                _ => new ValidationResult("Quotes must have at least one answer")
            };

        public static ValidationResult IsValid(IList<QuoteAnswerViewModel> answers) =>
            answers.Count != 1 && answers.Count != 3 ? new ValidationResult("Quote answers number should be 1 or 3")
            : HasOneRightAnswer(answers) ? ValidationResult.Success!
            : new ValidationResult("Quote answers should contain only one right answer");

        public static bool HasOneRightAnswer(IList<QuoteAnswerViewModel> value) =>
            value.Count(val => val.IsRightAnswer) == 1;

    }
}
