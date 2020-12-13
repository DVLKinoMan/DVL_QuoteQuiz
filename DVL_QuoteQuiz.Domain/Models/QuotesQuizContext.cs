using Microsoft.EntityFrameworkCore;

namespace DVL_QuoteQuiz.Domain.Models
{
    public class QuotesQuizContext : DbContext
    {
        public QuotesQuizContext(DbContextOptions<QuotesQuizContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteAnswer> QuoteAnswers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<UserAnsweredQuote> UserAnsweredQuotes { get; set; }
    }
}
