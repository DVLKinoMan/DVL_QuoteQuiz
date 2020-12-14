namespace DVL_QuoteQuiz.WebUI.Models
{
    public class ListQuoteResponse
    {
        public int QuoteId { get; set; }

        public string QuoteText { get; set; } = default!;

        public string AuthorName { get; set; } = default!;

        public bool IsDeleted { get; set; }
    }
}
