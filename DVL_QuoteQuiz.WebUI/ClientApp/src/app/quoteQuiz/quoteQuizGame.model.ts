export class QuoteQuizGame {
  public multipleAnswersQuiz: boolean;

  public quotes: InGameQuote[];
}

export class InGameQuote {
  public id: number;

  public text: string;

  public answers: InGameAnswer[];

  public answeredAuthorId: number;
}

export class InGameAnswer {
  public authorId: number;

  public authorName: string;
}
