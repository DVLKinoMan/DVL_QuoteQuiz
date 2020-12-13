using DVL_QuoteQuiz.Domain.Models;
using DVL_QuoteQuiz.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DVL_QuoteQuiz.WebUI.Extensions
{
    public static class Converters
    {
        public static Quote ToQuote(this AddQuoteRequest request) => new Quote
        {
            Text = request.QuoteText,
            QuoteAnswers = request.Answers.ToQuoteAnswers().ToList()
        };

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

        public static Author ToAuthor(this QuoteAuthor author) => author switch
        {
            { Id: { } id } => new Author {Id = id},
            { FullName: { } name } => new Author() {Name = name},
            _ => throw new InvalidOperationException("Author id or name should not be null")
        };

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

    }
}
