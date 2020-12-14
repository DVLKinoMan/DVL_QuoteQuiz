using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.WebUI.Extensions;
using DVL_QuoteQuiz.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVL_QuoteQuiz.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IAuthorsRepository _authorsRepo;

        public AuthorsController(ILogger<AuthorsController> logger, IAuthorsRepository authorsRepo)
        {
            _logger = logger;
            _authorsRepo = authorsRepo;
        }

        //todo: only admins have permission
        [HttpGet("List")]
        public async Task<List<QuoteAuthor>> ListAuthorsAsync() =>
            (await _authorsRepo.ListAsync()).ToQuoteAuthors();
    }
}
