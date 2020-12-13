using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Concrete
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly QuotesQuizContext _context;

        public QuotesRepository(QuotesQuizContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Quote quote)
        {
            await _context.Quotes.AddAsync(quote);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Quote>> ListAsync(int itemsPerPage = 10, int currentPageNumber = 1,
            bool showDeleted = false) =>
            await _context.Quotes.Where(q => showDeleted || (!q.IsDeleted && !showDeleted))
                .Skip(itemsPerPage * (currentPageNumber - 1))
                .Take(itemsPerPage)
                .ToListAsync();

        public async Task DeleteAsync(int quoteId) => await DeleteAsync(await GetAsync(quoteId));

        public async Task DeleteAsync(Quote quote)
        {
            quote.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Quote quote)
        {
            var q = await GetDetailedAsync(quote.Id);
            q.Text = quote.Text;
            q.QuoteAnswers = quote.QuoteAnswers;
            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<int, int>> GetIdsAndAuthorsAsync(bool includeDeleted = false) =>
            (await (from q in _context.Quotes.Where(q => includeDeleted || !q.IsDeleted)
                join ans in _context.QuoteAnswers on q.Id equals ans.QuoteId
                where ans.IsRightAnswer
                select new {q.Id, ans.AuthorId}).ToListAsync())
            .ToDictionary(q => q.Id, q => q.AuthorId);

        public async Task<Quote> GetAsync(int quoteId) =>
            await _context.Quotes.FirstOrDefaultAsync(q => q.Id == quoteId && q.IsDeleted == false) switch
            {
                { } q => q,
                _ => throw new ArgumentException("Quote was not found with the given Id", nameof(quoteId))
            };

        public async Task<Quote> GetDetailedAsync(int quoteId) =>
            await _context.Quotes
                    .Include("QuoteAnswers")
                    .Include("QuoteAnswers.Author")
                    .FirstOrDefaultAsync(q => q.Id == quoteId && q.IsDeleted == false) switch
                {
                    { } q => q,
                    _ => throw new ArgumentException("Quote was not found with the given Id", nameof(quoteId))
                };

    }
}
