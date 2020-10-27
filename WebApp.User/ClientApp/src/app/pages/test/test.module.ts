import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { TestComponent } from './test.component';
import { TestRoutingModule } from './test-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ControlModule } from 'src/app/helpers/control.module';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TestRoutingModule,
    ControlModule,
    NgxPaginationModule
  ],
  declarations: [TestComponent]
})
export class TestModule { }
