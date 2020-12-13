import { Component, Inject, OnInit } from "@angular/core";
import { QuoteQuizGame, InGameQuote } from "./quoteQuizGame.model";
import { ActivatedRoute, Params } from "@angular/router";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";

@Component({
  selector: 'app-quoteQuiz-component',
  templateUrl: './quoteQuiz.component.html'
})
export class QuoteQuizComponent implements OnInit {
  game: QuoteQuizGame = new QuoteQuizGame();
  multipleAnswersQuiz: boolean;
  currentQuote: InGameQuote;
  currentQuoteNumber: number = 0;

  constructor(
    private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.multipleAnswersQuiz = params['multipleAnswers'] != null;
    });
    this.initForm();
  }

  private initForm() {
    const params = new HttpParams().append('quotesNumber', '10').append('withMultipleChoices', this.multipleAnswersQuiz.toString());

    this.http.get<QuoteQuizGame>(this.baseUrl + 'Quotes/GetQuotesQuiz', { params}).subscribe(result => {
      this.game = result;
      this.game.multipleAnswersQuiz = this.multipleAnswersQuiz;
      this.currentQuote = this.game.quotes[this.currentQuoteNumber];
    }, error => console.error(error));
  }

  onClickYes() {

  }

  onClickNo() {

  }

}
