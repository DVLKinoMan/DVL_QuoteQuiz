import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { QuoteListResponse } from "./quote-list.model";

@Component({
  selector: 'app-quoteList-component',
  templateUrl: './quote-list.component.html'
})
export class QuoteListComponent implements OnInit {
  quotesList: QuoteListResponse[];

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  private router: Router) {

  }

  ngOnInit(): void {
    this.fetchData();
  }

  private fetchData() {
    this.http.get<QuoteListResponse[]>(this.baseUrl + "Quotes/List").subscribe(result => {
      this.quotesList = result;
    }, error => console.error(error));
  }

  onDelete(index: number) {
    this.http.post(this.baseUrl + "Quotes/Delete/" + this.quotesList[index].quoteId, null).subscribe(result => {
        this.fetchData();
      },
      error => console.error(error));
  }

  onRestore(index: number) {
    this.http.post(this.baseUrl + "Quotes/Restore/" + this.quotesList[index].quoteId, null).subscribe(result => {
        this.fetchData();
      },
      error => console.error(error));
  }

  onEdit(index: number) {
    this.router.navigate(['../quote/addEdit', { id: this.quotesList[index].quoteId }],{ relativeTo: this.route });
  }

}
