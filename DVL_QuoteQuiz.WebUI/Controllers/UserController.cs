using System.Collections.Generic;
using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.WebUI.Extensions;
using DVL_QuoteQuiz.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserAnsweredQuotesRepository _answeredQuotesRepo;
        private readonly IUsersRepository _usersRepo;
        private readonly IQuotesRepository _quotesRepo;

        public UserController(ILogger<UserController> logger, IUserAnsweredQuotesRepository answeredQuotesRepo,
            IUsersRepository usersRepo, IQuotesRepository quotesRepo)
        {
            _logger = logger;
            _answeredQuotesRepo = answeredQuotesRepo;
            _usersRepo = usersRepo;
            _quotesRepo = quotesRepo;
        }

        [HttpGet("List")]
        public async Task<List<ListUserResponse>> ListUsersAsync(int? itemsPerPage, int currentPageNumber = 1) =>
            (await _usersRepo.ListAsync(itemsPerPage, currentPageNumber, true)).ToListUserResponses();

        [HttpPost("Post/QuoteAnswer/{userId}")]
        public async Task<InGameAnswer> PostQuoteAnswerAsync(int userId, InGameQuote quote)
        {
            var res = (await _quotesRepo.GetQuoteAuthorAsync(quote.Id)).ToInGameAnswer();
            
            bool isRightAnswer = quote.AnswersCount == 1 //Is YesNo question
                ? (res.AuthorId == quote.Answers[0].AuthorId) == (quote.AnsweredAuthorId != null)
                : res.AuthorId == quote.AnsweredAuthorId;
           
            await _answeredQuotesRepo.AddAsync(quote.ToUserAnsweredQuote(userId, isRightAnswer));

            return res;
        }

        [HttpPost("Delete/{userId}")]
        public async Task DeleteAsync(int userId) => await _usersRepo.DeleteAsync(userId);

        [HttpPost("Disable/{userId}")]
        public async Task DisableAsync(int userId) => await _usersRepo.ToggleDisabledAsync(userId);

        [HttpPost("Restore/{userId}")]
        public async Task RestoreAsync(int userId) => await _usersRepo.ToggleDisabledAsync(userId);

    }
}
