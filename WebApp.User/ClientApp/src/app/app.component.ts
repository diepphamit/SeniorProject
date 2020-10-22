import { Component, DoCheck } from '@angular/core';
import { CURRENT_USER } from './constants/db-keys';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements DoCheck {
  title = 'app';
  isUser = false;

  ngDoCheck() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    user != null ? this.isUser = true : this.isUser = false;
  }
}
