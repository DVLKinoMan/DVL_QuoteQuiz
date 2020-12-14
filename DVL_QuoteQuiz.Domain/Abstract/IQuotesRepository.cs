using System;
using DVL_QuoteQuiz.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Abstract
{
    public interface IQuotesRepository
    {
        Task AddAsync(Quote quote);

        Task<List<Quote>> ListAsync(int? itemsPerPage, int currentPageNumber = 1,
            bool showDeleted = false);

        Task<Dictionary<int, int>> GetIdsAndAuthorsForUserAsync(int userId, DateTime maxDateTimeAnswered,
            bool includeDeleted = false);

        Task DeleteAsync(int quoteId);

        Task DeleteAsync(Quote quote);

        Task RestoreAsync(int quoteId);

        Task EditAsync(Quote quote);

        Task<Quote> GetAsync(int quoteId, bool evenIfDeleted = false);

        Task<Quote> GetDetailedAsync(int quoteId, bool evenIfDeleted = false);

        Task<bool> ExistsAnswerAsync(int quoteId, int authorId);

        Task<Author> GetQuoteAuthorAsync(int quoteId);
    }
}
