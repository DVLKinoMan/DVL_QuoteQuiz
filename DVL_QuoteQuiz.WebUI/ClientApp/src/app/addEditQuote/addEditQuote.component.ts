import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
//import { Quote, QuoteAnswer } from "./quote.model";
import { QuoteService } from "../Services/quote.service";

@Component({
  selector: 'app-addQuote-component',
  templateUrl: './addQuote.component.html'
})

export class AddEditQuoteComponent implements OnInit {
  id: number;
  editMode = false;
  //public quote: Quote = new Quote(null);
  //public newAnswer: QuoteAnswer;
  quoteForm: FormGroup;

  get answersControls() {
    return (this.quoteForm.get('answers') as FormArray).controls;
  }

  constructor(
    private route: ActivatedRoute,
    private quoteService: QuoteService,
    private router: Router) {

  }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
      this.editMode = params['id'] != null;
    });
    this.initForm();
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

  private initForm() {
    let quoteText = '';
    let answers = new FormArray([]);

    //if (this.editMode) {
    //  const recipe = this.recipeService.getRecipe(this.id);
    //  recipeName = recipe.name;
    //  recipeImagePath = recipe.imagePath;
    //  recipeDescription = recipe.description;
    //  if (recipe['ingredients']) {
    //    for (let ingredient of recipe.ingredients) {
    //      recipeIngredients.push(
    //        new FormGroup({
    //          name: new FormControl(ingredient.name, Validators.required),
    //          amount: new FormControl(ingredient.amount, [
    //            Validators.required,
    //            Validators.pattern(/^[1-9]+[0-9]*$/)
    //          ])
    //        })
    //      );
    //    }
    //  }
    //}

    this.quoteForm = new FormGroup({
      quoteText: new FormControl(quoteText, Validators.required),
      answers: answers
    });
  }

  onSubmit() {
    // const newRecipe = new Recipe(
    //   this.recipeForm.value['name'],
    //   this.recipeForm.value['description'],
    //   this.recipeForm.value['imagePath'],
    //   this.recipeForm.value['ingredients']);
    //if (this.editMode) {
    //  this.recipeService.updateRecipe(this.id, this.recipeForm.value);
    //} else {
      this.quoteService.addQuote(this.quoteForm.value);
    //}
    this.onCancel();
  }

  onCancel() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
