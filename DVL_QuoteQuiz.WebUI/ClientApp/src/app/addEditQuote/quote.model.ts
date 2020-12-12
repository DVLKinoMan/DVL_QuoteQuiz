export class Quote {
  public quoteText: string;
  public answers: QuoteAnswer[];

  constructor(quoteText: string) {
    this.quoteText = quoteText;
    this.answers = [];
  }
}

export class QuoteAnswer {
  public author: Author;
  public isRightAnswer: boolean;

  constructor(author: Author, isRightAnswer: boolean) {
    this.author = author;
    this.isRightAnswer = isRightAnswer;
  }
}

export class Author {
  public id: number;
  public fullName: string;

  constructor(id: number, fullName: string) {
    this.id = id;
    this.fullName = fullName;
  }
}
