import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user/user.model';
import { ValidationService } from 'src/app/services/validation.service';
import { CURRENT_USER } from 'src/app/constants/db-keys';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html'
})
export class AddUserComponent implements OnInit {

  addUserForm: FormGroup;
  user: any;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService
  ) {
    this.addUserForm = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required, ValidationService.passwordValidator]],
      passwordAgain: ['', [Validators.required, ValidationService.passwordMatch]],
      email: ['', [Validators.required, ValidationService.emailValidator]],
      fullName: [''],
      address: [''],
      phoneNumber: ['', ValidationService.phonenumberValidator],
      gender: [true],
      dateOfBirth: ['']
    });
  }

  ngOnInit() {
    // if (this.getId !== 1) {
    //   this.router.navigate(['/home']);
    // }
  }

  addUser() {
    this.user = Object.assign({}, this.addUserForm.value);
    // console.log(this.user);
    this.userService.createUser(this.user).subscribe(
      () => {
        this.router.navigate(['/users']).then(() => {
          this.toastr.success('Tạo tài khoản thành công');
        });
      },
      (error: HttpErrorResponse) =>
        this.toastr.error('Tạo tài khoản không thành công!')
      );
  }

  get f() { return this.addUserForm.controls; }

  // get getId() {
  //   const user = JSON.parse(localStorage.getItem(CURRENT_USER));
  //   if (user != null) {
  //     return user.id;
  //   }

  //   return 0;
  // }
}