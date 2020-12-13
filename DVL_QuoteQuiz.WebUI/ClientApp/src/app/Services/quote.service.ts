import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Quote } from "../quote-add-edit/quote.model";
import { Inject, Injectable } from "@angular/core";
import { throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class QuoteService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  addQuote(quote: Quote) {
    this.http
      .post<Quote>(
        this.baseUrl + 'Quotes/Add',
        quote
      )
      .subscribe(
        responseData => {
          console.log(responseData);
        },
        error => {
          let errorMessage;
          if (error.error instanceof ErrorEvent) 
            errorMessage = `Error Message: ${error.error.message}`;
          else 
            errorMessage = `Error Status: ${error.status}\nMessage: ${error.message}`;

          window.alert(errorMessage);

          return throwError(errorMessage);
        }
      );
  }

}
