import { Component, OnInit } from '@angular/core';
import { User } from '../../../models/user';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {

  user: User = new User();
  searchInput: string;

  constructor(private authenticationService: AuthenticationService, private router: Router) {
  }

  ngOnInit(): void {
    let userTmp = JSON.parse(localStorage.getItem("currentUser"));
    if(userTmp)
    {
      this.user = userTmp;
      console.log(this.user);
    }
  }

  submitSearch(){
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
        return false;
    }
    this.router.onSameUrlNavigation = 'reload';
    if(!this.isEmptyOrSpaces(this.searchInput))
      this.router.navigate(['/search', this.searchInput]);
  }

  logout() {
    this.authenticationService.logout();
    location.reload();
  }

  isEmptyOrSpaces(str:string = ""){
    return str === null || str?.match(/^ *$/) !== null;
  }

}
