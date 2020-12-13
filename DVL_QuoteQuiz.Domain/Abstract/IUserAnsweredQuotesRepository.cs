using DVL_QuoteQuiz.Domain.Models;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Abstract
{
    public interface IUserAnsweredQuotesRepository
    {
        Task AddAsync(UserAnsweredQuote answeredQuote);
    }
}
