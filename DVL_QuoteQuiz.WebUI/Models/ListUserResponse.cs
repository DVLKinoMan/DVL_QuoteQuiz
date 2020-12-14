using DVL_QuoteQuiz.Domain.Models;
using System;

namespace DVL_QuoteQuiz.WebUI.Models
{
    public class ListUserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Gender { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime LastUpdatedDateTime { get; set; }

        public int AnsweredQuotesCount { get; set; } = 0;

        public bool IsDisabled { get; set; }
    }
}
