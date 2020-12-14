using DVL_QuoteQuiz.Domain.Models;
using DVL_QuoteQuiz.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DVL_QuoteQuiz.WebUI.Extensions
{
    public static class Converters
    {
        #region Quote
        public static Quote ToQuote(this AddEditQuoteRequest request, int? quoteId = null)
        {
            var quote = new Quote
            {
                Text = request.QuoteText,
                QuoteAnswers = request.Answers.ToQuoteAnswers().ToList()
            };
            if (quoteId is { } id)
                quote.Id = id;

            return quote;
        }

        public static IEnumerable<QuoteAnswer> ToQuoteAnswers(this IList<QuoteAnswerViewModel> viewModel) =>
            viewModel.Select(mod => mod.ToQuoteAnswer());

        public static QuoteAnswer ToQuoteAnswer(this QuoteAnswerViewModel viewModel)
        {
            var answer = new QuoteAnswer
            {
                IsRightAnswer = viewModel.IsRightAnswer,
            };

            switch (viewModel.Author)
            {
                case { Id: { } id }:
                    answer.AuthorId = id;
                    break;
                case { FullName: { } name }:
                    answer.Author = new Author() {Name = name};
                    break;
                default:
                    throw new InvalidOperationException("Author id or name should not be null");
            }

            return answer;
        }

        #endregion

        #region Answers
        public static IEnumerable<InGameAnswer> ToInGameAnswers(this IEnumerable<QuoteAnswer> qAnswers) =>
            qAnswers.Select(q => q.ToInGameAnswer());

        public static InGameAnswer ToInGameAnswer(this QuoteAnswer q) => new InGameAnswer
        {
            AuthorId = q.AuthorId,
            AuthorName = q.Author.Name
        };

        public static InGameAnswer ToInGameAnswer(this Author author) => new InGameAnswer
        {
            AuthorId = author.Id,
            AuthorName = author.Name
        };

        public static UserAnsweredQuote ToUserAnsweredQuote(this InGameQuote inGameQuote, int userId,
            bool answeredRight) =>
            new UserAnsweredQuote
            {
                AnsweredAuthorId = inGameQuote.AnsweredAuthorId,
                QuoteId = inGameQuote.Id,
                UserId = userId,
                YesNoQuestion = inGameQuote.AnswersCount == 1,
                AnsweredDateTime = DateTime.Now,
                AnsweredRight = answeredRight
            };
        #endregion

        #region ListQuote

        public static List<ListQuoteResponse> ToListQuoteResponses(this IEnumerable<Quote> quotes) =>
            quotes.Select(q => q.ToListQuoteResponse()).ToList();

        public static ListQuoteResponse ToListQuoteResponse(this Quote quote) =>
            new ListQuoteResponse
            {
                QuoteId = quote.Id,
                QuoteText = quote.Text,
                AuthorName = quote.QuoteAnswers
                    .Where(q => q.IsRightAnswer)
                    .Select(ans => ans.Author.Name)
                    .FirstOrDefault(),
                IsDeleted = quote.IsDeleted
            };

        #endregion

        #region AddEdit
        public static AddEditQuoteRequest ToAddEditRequest(this Quote quote) =>
            new AddEditQuoteRequest
            {
                QuoteText = quote.Text,
                Answers = quote.QuoteAnswers.ToQuoteAnswerViewModels().ToList()
            };

        public static IEnumerable<QuoteAnswerViewModel>
            ToQuoteAnswerViewModels(this IEnumerable<QuoteAnswer> answers) =>
            answers.Select(ans => ans.ToQuoteAnswerViewModel());

        public static QuoteAnswerViewModel ToQuoteAnswerViewModel(this QuoteAnswer answer) =>
            new QuoteAnswerViewModel
            {
                IsRightAnswer = answer.IsRightAnswer,
                Author = answer.Author.ToQuoteAuthor()
            };
#endregion

        #region Users
        public static List<ListUserResponse> ToListUserResponses(this IEnumerable<User> users) =>
            users.Select(u => u.ToListUserResponse()).ToList();

        public static ListUserResponse ToListUserResponse(this User user) =>
            new ListUserResponse
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email,
                AnsweredQuotesCount = user.AnsweredQuotes switch
                {
                    { } count => count.Count,
                    _ => 0
                },
                IsAdmin = user.IsAdmin,
                Gender = user.Gender?.ToString(),
                IsDisabled = user.IsDisabled,
                LastUpdatedDateTime = user.LastUpdatedDateTime
            };
#endregion

        #region Author
        public static Author ToAuthor(this QuoteAuthor author) => author switch
        {
            { Id: { } id } => new Author {Id = id},
            { FullName: { } name } => new Author() {Name = name},
            _ => throw new InvalidOperationException("Author id or name should not be null")
        };

        public static List<QuoteAuthor> ToQuoteAuthors(this IEnumerable<Author> authors) =>
            authors.Select(auth => auth.ToQuoteAuthor()).ToList();

        public static QuoteAuthor ToQuoteAuthor(this Author author) =>
            new QuoteAuthor
            {
                Id = author.Id,
                FullName = author.Name
            };
#endregion
    }
}
