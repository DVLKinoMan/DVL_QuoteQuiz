import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AddEditQuoteComponent } from "./quote-add-edit/quote-add-edit.component";
import { QuoteQuizComponent } from './quote-quiz/quote-quiz.component';
import { CookieService } from 'ngx-cookie-service';
import { SettingsComponent } from "./settings/settings.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddEditQuoteComponent,
    SettingsComponent,
    QuoteQuizComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'quote/add', component: AddEditQuoteComponent },
      { path: 'quote/quiz', component: QuoteQuizComponent },
      { path: 'quote/settings', component: SettingsComponent }
    ])
  ],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
