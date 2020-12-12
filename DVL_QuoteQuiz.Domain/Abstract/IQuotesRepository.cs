using DVL_QuoteQuiz.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Abstract
{
    public interface IQuotesRepository
    {
        Task AddAsync(Quote quote);

        Task<List<Quote>> ListAsync(int itemsPerPage = 10, int currentPageNumber = 1, bool showDeleted = false);

        Task DeleteAsync(int quoteId);

        Task DeleteAsync(Quote quote);

        Task EditAsync(Quote quote);
    }
}
