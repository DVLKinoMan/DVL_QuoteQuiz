import { Component, OnInit, Inject } from '@angular/core';
import { FormArray, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { QuoteService } from "../Services/quote.service";
import { HttpClient } from "@angular/common/http";
import { Quote, Author } from "./quote.model";
//import { NgSelectConfig } from '@ng-select/ng-select';

@Component({
  selector: 'app-addQuote-component',
  templateUrl: './quote-add-edit.component.html'
})

export class AddEditQuoteComponent implements OnInit {
  id: number;
  editMode = false;
  quoteForm: FormGroup;
  authors: Author[];

  get answersControls() {
    return (this.quoteForm.get('answers') as FormArray).controls;
  }

  constructor(
    private route: ActivatedRoute,
    private quoteService: QuoteService,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router) {

  }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
      this.editMode = params['id'] != null;
    });
    this.initForm();
    this.fetchAuthors();
  }

  onAddAnswer() {
    (<FormArray>this.quoteForm.get('answers')).push(
      new FormGroup({
        author: new FormGroup({
          id: new FormControl(),
          fullName: new FormControl()
        }),
        isRightAnswer: new FormControl(false)
      })
    );
  }

  onDeleteAnswer(index: number) {
    (<FormArray>this.quoteForm.get('answers')).removeAt(index);
  }

  private fetchAuthors() {
    this.http.get<Author[]>(this.baseUrl + "Authors/List").subscribe(result => {
        this.authors = result;
      },
      error => console.error(error));
  }

  private initForm() {
    let answers = new FormArray([]);

    if (this.editMode) {
      this.http.get<Quote>(this.baseUrl + "Quotes/Get/" + this.id).subscribe(result => {
          for (let answer of result.answers) {
            answers.push(
              new FormGroup({
                author: new FormGroup({
                  id: new FormControl(answer.author.id),
                  fullName: new FormControl(answer.author.fullName)
                }),
                isRightAnswer: new FormControl(answer.isRightAnswer)
              })
            );
          }
          this.quoteForm = new FormGroup({
            quoteText: new FormControl(result.quoteText, Validators.required),
            answers: answers
          });

        },
        error => console.error(error));

    } else {
      this.quoteForm = new FormGroup({
        quoteText: new FormControl("", Validators.required),
        answers: answers
      });
    }
  }

  onSubmit() {
    if (this.editMode) {
      this.quoteService.editQuote(this.quoteForm.value, this.id);
    } else {
      this.quoteService.addQuote(this.quoteForm.value);
    }
    this.onCancel();
  }

  onCancel() {
    this.router.navigate(['/list']);;
  }
}
