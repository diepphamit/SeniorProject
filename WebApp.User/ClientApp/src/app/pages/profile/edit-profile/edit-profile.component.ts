import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CURRENT_USER } from 'src/app/constants/db-keys';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  user: any;
  id: any;
  constructor(private userService: UserService,
    private router: Router) { }

  ngOnInit() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));
    if (user !== null) {
      this.id = user.id;

      this.getUser(this.id);
    }
  }

  getUser(id: number) {
    this.user = this.userService.getUserById(id).subscribe(data => this.user = data);
  }

  UpdateUser() {
    this.userService.editUser(this.id, this.user).subscribe(data => {
      this.router.navigate(['/profile/' + this.id]);
    });
  }

}
