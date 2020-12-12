using DVL_QuoteQuiz.WebUI.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DVL_QuoteQuiz.WebUI.Models
{
    public class AddQuoteRequest
    {
        [Required]
        public string QuoteText { get; set; } = default!;

        [Required] [QuoteAnswers] public List<QuoteAnswerViewModel> Answers { get; set; } = default!;
    }

    public class QuoteAnswerViewModel
    {
        [Required]
	    [QuoteAuthor]
        public QuoteAuthor Author { get; set; }

        public bool IsRightAnswer { get; set; } = false;
    }

    public class QuoteAuthor
    {
        public int? Id { get; set; }

        public string? FullName { get; set; }
    }
}
