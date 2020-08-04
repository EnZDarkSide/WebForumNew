import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { DiscussionComponent } from './components/discussion/discussion.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { SearchComponent } from './components/search/search.component';
import { TopicComponent } from './components/topic/topic.component';
import { SearchService } from './services/search.service';

import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';

import { JwtInterceptor } from './Interceptors/JWTInterceptor';
import { ErrorInterceptor } from './Interceptors/ErrorInterceptor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { WelcomeComponent } from './components/shared/welcome/welcome.component';

import { QuillModule } from 'ngx-quill';
import Counter from './counter';

@NgModule({
  declarations: [
    MainPageComponent,
    DiscussionComponent,
    LoginComponent,
    RegisterComponent,
    SearchComponent,
    TopicComponent,
    AppComponent,
    WelcomeComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    QuillModule.forRoot({
      customModules: [{
        implementation: Counter,
        path: 'modules/counter'
      }],
      customOptions: [{
        import: 'formats/font',
        whitelist: ['mirza', 'roboto', 'aref', 'serif', 'sansserif', 'monospace']
      }]
    }),
  ],
  providers: [SearchService, HttpClient,
    //Перехват запросов
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
