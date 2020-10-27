import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { TopicComponent } from './topic.component';
import { TopicRoutingModule } from './topic-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ControlModule } from 'src/app/helpers/control.module';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TopicRoutingModule,
    ControlModule,
    NgxPaginationModule
  ],
  declarations: [TopicComponent]
})
export class TopicModule { }
