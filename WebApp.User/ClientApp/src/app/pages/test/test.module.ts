import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { TestComponent } from './test.component';
import { TestRoutingModule } from './test-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ControlModule } from 'src/app/helpers/control.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { HistoryTestComponent } from './history-test/history-test.component';
import { DetailTestComponent } from './detail-test/detail-test.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TestRoutingModule,
    ControlModule,
    NgxPaginationModule
  ],
  declarations: [TestComponent, HistoryTestComponent, DetailTestComponent]
})
export class TestModule { }
