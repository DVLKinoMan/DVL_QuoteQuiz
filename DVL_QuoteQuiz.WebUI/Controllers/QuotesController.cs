using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVL_QuoteQuiz.Domain.Abstract;
using DVL_QuoteQuiz.WebUI.Extensions;
using DVL_QuoteQuiz.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DVL_QuoteQuiz.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : Controller
    {
        private readonly ILogger<QuotesController> _logger;
        private readonly IQuotesRepository _quotesRepo;

        public QuotesController(ILogger<QuotesController> logger, IQuotesRepository quotesRepo)
        {
            _logger = logger;
            _quotesRepo = quotesRepo;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(AddQuoteRequest request)
        {
            await _quotesRepo.AddAsync(request.ToQuote());
            return Ok();
        }

    }
}
