import { Component, ViewEncapsulation } from '@angular/core';
import { UserService } from './services/user.service';
import { User } from './models/user';
import { GuardsCheckStart } from '@angular/router';

@Component({
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {

}
