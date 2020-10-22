import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ControlModule } from 'src/app/helpers/control.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        LoginRoutingModule,
        ControlModule
    ],
    declarations: [LoginComponent]
})
export class LoginModule { }
