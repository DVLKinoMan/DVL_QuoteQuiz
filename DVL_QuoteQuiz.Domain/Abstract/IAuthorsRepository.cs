using System.Collections.Generic;
using System.Threading.Tasks;
using DVL_QuoteQuiz.Domain.Models;

namespace DVL_QuoteQuiz.Domain.Abstract
{
    public interface IAuthorsRepository
    {
        Task<Dictionary<int, string>> GetAuthorsWithNames();
        Task<List<Author>> ListAsync();
    }
}
