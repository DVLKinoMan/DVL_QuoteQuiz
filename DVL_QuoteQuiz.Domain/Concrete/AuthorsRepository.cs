using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Concrete
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly QuotesQuizContext _context;

        public AuthorsRepository(QuotesQuizContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, string>> GetAuthorsWithNames() =>
            await _context.Authors.ToDictionaryAsync(auth => auth.Id, auth => auth.Name);

        public async Task<List<Author>> ListAsync() =>
            await _context.Authors.ToListAsync();
    }
}
