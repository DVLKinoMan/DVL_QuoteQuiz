using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Abstract
{
    public interface IAuthorsRepository
    {
        Task<Dictionary<int, string>> GetAuthorsWithNames();
    }
}
