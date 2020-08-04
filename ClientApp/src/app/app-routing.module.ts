import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent} from './components/main-page/main-page.component';
import { DiscussionComponent} from './components/discussion/discussion.component';
import { TopicComponent } from './components/topic/topic.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { SearchComponent } from './components/search/search.component';

const routes: Routes = [
  {
    path: '', component: MainPageComponent,
  },
  {
    path: 'discussions/:discussionId', component: DiscussionComponent,
  },
  {
    path: 'topic/:topicId', component: TopicComponent,
  },
  {
    path: 'login', component: LoginComponent,
  },
  {
    path: 'register', component: RegisterComponent,
  },
  {
    path: 'search/:searchString', component: SearchComponent, pathMatch:'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
