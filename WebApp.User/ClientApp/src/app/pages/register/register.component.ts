import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserRegister } from 'src/app/models/user/user-register.model';
import { AuthService } from 'src/app/services/auth.service';
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  registerUser: UserRegister;
  isSuccess = true;

  constructor(private fb: FormBuilder,
    private router: Router,
    private authService: AuthService) {
      this.registerForm = this.fb.group({
        username: ['', Validators.required],
        email: ['', [Validators.required, ValidationService.emailValidator]],
        password: ['', [Validators.required, ValidationService.passwordValidator]],
        confirmPassword: ['', [Validators.required, ValidationService.passwordMatch]]
      });
     }

  ngOnInit() {
  }

  register() {
    this.registerUser = Object.assign({}, this.registerForm.value);

    this.authService.register(this.registerUser).subscribe(
      response => {
        // this.loadingBar.stop();
        this.router.navigate(['/home']).then(() => {
          // this.toastr.success('Đăng nhập thành công!');
        });
      },
      error => {
        // this.toastr.error('Tên đăng nhập hoặc mật khẩu không đúng!');
        // this.loadingBar.stop();
        this.isSuccess = false;
      }
    );
  }

  get f() { return this.registerForm.controls; }

}
