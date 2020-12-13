import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AddEditQuoteComponent } from "./addEditQuote/addEditQuote.component";
import { QuoteQuizComponent } from './quoteQuiz/quoteQuiz.component';
import { CookieService } from 'ngx-cookie-service';
import { SettingsComponent } from "./settings/settings.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddEditQuoteComponent,
    SettingsComponent,
    QuoteQuizComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'addQuote', component: AddEditQuoteComponent },
      { path: 'quoteQuiz', component: QuoteQuizComponent },
      { path: 'settings', component: SettingsComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
