using System.Collections.Generic;
using DVL_QuoteQuiz.Domain.Models;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.Domain.Abstract
{
    public interface IUsersRepository
    {
        Task<List<User>> ListAsync(int? itemsPerPage, int currentPageNumber = 1,
            bool showDisabled = false);

        Task ToggleDisabledAsync(int userId);

        Task DeleteAsync(int userId);
    }
}
