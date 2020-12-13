using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.Domain.Models;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Concrete
{
    public class UserAnsweredQuotesRepository : IUserAnsweredQuotesRepository
    {
        private readonly QuotesQuizContext _context;

        public UserAnsweredQuotesRepository(QuotesQuizContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserAnsweredQuote answeredQuote)
        {
            _context.UserAnsweredQuotes.Add(answeredQuote);
            await _context.SaveChangesAsync();
        }
    }
}
