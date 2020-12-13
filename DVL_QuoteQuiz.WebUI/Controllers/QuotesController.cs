using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.WebUI.Extensions;
using DVL_QuoteQuiz.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DVL_QuoteQuiz.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : Controller
    {
        private readonly ILogger<QuotesController> _logger;
        private readonly IQuotesRepository _quotesRepo;
        private readonly IAuthorsRepository _authorsRepo;
        private readonly long _maxedAllowedIntervalForQuote = TimeSpan.TicksPerDay;

        public QuotesController(ILogger<QuotesController> logger, IQuotesRepository quotesRepo, IAuthorsRepository authorsRepo, IConfiguration config)
        {
            _logger = logger;
            _quotesRepo = quotesRepo;
            _authorsRepo = authorsRepo;
            if (long.TryParse(config["maxedAllowedIntervalForQuote"], out var val))
                _maxedAllowedIntervalForQuote = val;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(AddQuoteRequest request)
        {
            await _quotesRepo.AddAsync(request.ToQuote());
            return Ok();
        }

        [HttpGet("Get/NextQuote/{userId}")]
        public async Task<InGameQuote?> GetQuoteForUserAsync(int userId, bool withMultipleChoices = true)
        {
            InGameQuote? gameQuote = null;

            var quotesWithAuthorIds = await _quotesRepo.GetIdsAndAuthorsForUserAsync(userId, new DateTime(DateTime.Now.Ticks - _maxedAllowedIntervalForQuote));

            if (quotesWithAuthorIds.Count == 0)
                return gameQuote;

            var authorNames = await _authorsRepo.GetAuthorsWithNames();

            var random = new Random();

            gameQuote = new InGameQuote
                {Id = quotesWithAuthorIds.ElementAt(random.Next(0, quotesWithAuthorIds.Count)).Key};
            await AddRandomAnswers(gameQuote);
            quotesWithAuthorIds.Remove(gameQuote.Id);

            return gameQuote;

            async Task AddRandomAnswers(InGameQuote quote)
            {
                var dbQuote = withMultipleChoices
                    ? await _quotesRepo.GetDetailedAsync(quote.Id)
                    : await _quotesRepo.GetAsync(quote.Id);
                quote.Text = dbQuote.Text;

                if (!withMultipleChoices)
                {
                    var ans = new InGameAnswer();
                    (ans.AuthorId, ans.AuthorName) = (random.Next(0, 2) == 1
                        ? (quotesWithAuthorIds[quote.Id], authorNames[quotesWithAuthorIds[quote.Id]])
                        : PickAnyAuthorExcept(quotesWithAuthorIds[quote.Id]));
                    quote.Answers.Add(ans);
                }
                else
                {
                    quote.Answers.AddRange(dbQuote.QuoteAnswers.ToInGameAnswers());
                    var authorIds = quote.Answers.Select(ans => ans.AuthorId).ToList();
                    //todo it may loop forever
                    //if this question has not multiple answers
                    while (quote.AnswersCount != 3)
                    {
                        var ans = new InGameAnswer();
                        (ans.AuthorId, ans.AuthorName) = PickAnyAuthorExcept(authorIds.ToArray());
                        authorIds.Add(ans.AuthorId);
                        quote.Answers.Add(ans);
                    }
                }
            }

            (int, string) PickAnyAuthorExcept(params int[] authorIds)
            {
                int authId;
                do
                {
                    authId = authorNames.ElementAt(random.Next(0, authorNames.Count)).Key;
                } while (authorIds.Contains(authId));

                return (authId, authorNames[authId]);
            }
        }

        //[HttpGet("{quoteId}/Belongs/{authorId}")]
        //public async Task<bool> IfAnsweredRight(int quoteId, int authorId, bool mustBeTrue = true) =>
        //    (await _quotesRepo.ExistsAnswerAsync(quoteId, authorId)) == mustBeTrue;

    }
}
