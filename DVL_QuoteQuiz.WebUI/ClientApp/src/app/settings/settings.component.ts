import { Component, OnInit } from "@angular/core";
import {CookieService} from 'ngx-cookie-service'

@Component({
  selector: 'app-settings-component',
  templateUrl: './settings.component.html'
})

export class SettingsComponent implements OnInit {
  multipleQuoteQuestions: boolean = false;

  constructor(private cookieService: CookieService) {

  }

  ngOnInit(): void {
    var cookie = this.cookieService.get('multipleAnswersQuiz');
    if (cookie)
      this.multipleQuoteQuestions = cookie == "true";
    else 
      this.cookieService.set('multipleAnswersQuiz', this.multipleQuoteQuestions.toString());
    
  }

  onCheckboxChanged() {
    this.cookieService.set('multipleAnswersQuiz', this.multipleQuoteQuestions.toString());
  }
}
