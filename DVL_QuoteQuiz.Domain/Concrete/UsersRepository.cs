using System;
using System.Collections.Generic;
using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Concrete
{
    public class UsersRepository : IUsersRepository
    {
        private readonly QuotesQuizContext _context;

        public UsersRepository(QuotesQuizContext context)
        {
            _context = context;
        }

        public async Task<List<User>> ListAsync(int? itemsPerPage, int currentPageNumber = 1, bool showDisabled = false)
        {
            var query = _context.Users.Include("AnsweredQuotes")
                .Where(q => showDisabled || (!q.IsDisabled && !showDisabled));

            if (itemsPerPage is { } itPage)
                return await query.Skip(itPage * (currentPageNumber - 1))
                    .Take(itPage)
                    .ToListAsync();

            return await query.ToListAsync();
        }

        public async Task ToggleDisabledAsync(int userId) => await ToggleDisableAsync(await GetAsync(userId, true));

        public async Task ToggleDisableAsync(User us)
        {
            us.IsDisabled = !us.IsDisabled;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId)
        {
            var us = await GetDetailedAsync(userId, true);
            _context.UserAnsweredQuotes.RemoveRange(us.AnsweredQuotes);
            _context.Users.Remove(us);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(int userId, bool evenIfDisabled) =>
            await _context.Users.FirstOrDefaultAsync(q => q.Id == userId && (evenIfDisabled || q.IsDisabled == false))
                switch
                {
                    { } u => u,
                    _ => throw new ArgumentException("User was not found with the given Id", nameof(userId))
                };

        public async Task<User> GetDetailedAsync(int userId, bool evenIfDisabled) =>
            await _context.Users.Include("AnsweredQuotes").FirstOrDefaultAsync(q => q.Id == userId && (evenIfDisabled || q.IsDisabled == false))
                switch
                {
                    { } u => u,
                    _ => throw new ArgumentException("User was not found with the given Id", nameof(userId))
                };
    }
}
