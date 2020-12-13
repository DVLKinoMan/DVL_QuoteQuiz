import { Component, Inject, OnInit } from "@angular/core";
import { InGameQuote, InGameAnswer } from "./quote-quiz-game.model";
import { ActivatedRoute, Params } from "@angular/router";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { CookieService } from 'ngx-cookie-service'

@Component({
  selector: 'app-quoteQuiz-component',
  templateUrl: './quote-quiz.component.html'
})
export class QuoteQuizComponent implements OnInit {
  multipleAnswersQuiz: boolean;

  currentQuote: InGameQuote;

  message: string;
  actualAuthorName: string;
  showAnswer: boolean = false;
  userId: number = 1;//todo
  gameHasEnded: boolean = false;

  constructor(
    private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private cookieService: CookieService) {
    
  }

  ngOnInit() {
    var cookie = this.cookieService.get('multipleAnswersQuiz');
    if (cookie)
      this.multipleAnswersQuiz = cookie == "true";
    this.getQuote();
  }

  private getQuote() {
    const params = new HttpParams().append('withMultipleChoices', this.multipleAnswersQuiz.toString());

    this.http.get<InGameQuote>(this.baseUrl + "Quotes/Get/NextQuote/" + this.userId, { params }).subscribe(result => {
      if (result == null)
        this.gameHasEnded = true;
      else this.currentQuote = result;
    }, error => console.error(error));
  }

  yesOrNoClick(yesClick: boolean) {
    if (yesClick)
      this.currentQuote.answeredAuthorId = this.currentQuote.answers[0].authorId;

    this.http.post<InGameAnswer>(this.baseUrl +"User/Post/QuoteAnswer/"+this.userId, this.currentQuote)
      .subscribe(result => {
          this.checkAnswer(result, this.currentQuote.answers[0].authorId, yesClick);
        },
        error => console.error(error));

  }

  onClickAuthor(authorIndex: number) {
    this.currentQuote.answeredAuthorId = this.currentQuote.answers[authorIndex].authorId;

    this.http.post<InGameAnswer>(this.baseUrl + "User/Post/QuoteAnswer/" + this.userId, this.currentQuote)
      .subscribe(result => {
        this.checkAnswer(result, this.currentQuote.answeredAuthorId);
        },
        error => console.error(error));

  }

  private checkAnswer(actualAuthor: InGameAnswer, expectedAuthorId: number, mustBeTrue: boolean = true) {
    this.showAnswer = true;
    this.actualAuthorName = actualAuthor.authorName;
    if ((actualAuthor.authorId === expectedAuthorId) === mustBeTrue) 
      this.message = "Correct! The right answer is: " + this.actualAuthorName;
    else 
      this.message = "Sorry, you are wrong! The right answer is: " + this.actualAuthorName;
  }

  onClickNext() {
    this.showAnswer = false;
    this.actualAuthorName = null;
    this.message = null;
    this.currentQuote = null;

    this.getQuote();
  }

}
