using System.Collections.Generic;

namespace DVL_QuoteQuiz.WebUI.Models
{
    public class QuoteQuizGame
    {
        public int QuotesCount => Quotes.Count;

        public bool MultipleAnswersQuiz { get; set; }

        public IList<InGameQuote> Quotes { get; set; } = new List<InGameQuote>();
    }

    public class InGameQuote
    {
        public int Id { get; set; }

        public string Text { get; set; } = default!;

        public int AnswersCount => Answers.Count;

        public List<InGameAnswer> Answers { get; set; } = new List<InGameAnswer>();

        public int? AnsweredAuthorId { get; set; }
    }

    public class InGameAnswer
    {
        public int AuthorId { get; set; }

        public string AuthorName { get; set; } = default!;
    }
}
