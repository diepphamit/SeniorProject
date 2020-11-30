import { AfterContentChecked, Component, DoCheck, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { range } from 'rxjs';
import { CURRENT_USER } from 'src/app/constants/db-keys';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit, DoCheck {

  isUser = false;
  toggleLogin = false;
  active = [
    { key: 'home', value: 1},
    { key: 'topic', value: 0},
    { key: 'test', value: 0},
    { key: 'profile', value: 0},
    { key: 'signin', value: 0},
    { key: 'signup', value: 0},
  ];

  constructor(public authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    user != null ? this.isUser = true : this.isUser = false;
  }

  ngDoCheck() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    user != null ? this.isUser = true : this.isUser = false;
  }

  login() {
    this.toggleLogin = !this.toggleLogin;
  }

  get name() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    return user.username || '';
  }

  logout() {
    this.authService.logout();
    this.isUser = false;
    this.router.navigate(['/home']).then(() => {
    });
  }

  get getuserId() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    return user.id;
  }
  get getActive() {
    return 'active';
  }

  changeActive(id: number) {
    for (let i = 0; i < 6; i++) {
      if (id === i) {
        this.active[i]['value'] = 1;
      } else {
        this.active[i]['value'] = 0;
      }
    }
  }

}
