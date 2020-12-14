import { Component, Inject } from '@angular/core';
import { User } from "./user.model";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  userList: User[];

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
    this.http.get<User[]>(this.baseUrl + "User/List").subscribe(result => {
      this.userList = result;
    }, error => console.error(error));
  }

  onDelete(index: number) {
    if (confirm("Are you sure to delete " + this.userList[index].userName)) {
      this.http.post(this.baseUrl + "User/Delete/" + this.userList[index].userId, null).subscribe(result => {
          this.fetchData();
        },
        error => console.error(error));
    }
  }

  onDisable(index: number) {
    this.http.post(this.baseUrl + "User/Disable/" + this.userList[index].userId, null).subscribe(result => {
        this.fetchData();
      },
      error => console.error(error));
  }

  onRestore(index: number) {
    this.http.post(this.baseUrl + "User/Restore/" + this.userList[index].userId, null).subscribe(result => {
        this.fetchData();
      },
      error => console.error(error));
  }

  onEdit(index: number) {
    this.router.navigate(['../user/addEdit', { id: this.userList[index].userId }], { relativeTo: this.route });
  }
}
