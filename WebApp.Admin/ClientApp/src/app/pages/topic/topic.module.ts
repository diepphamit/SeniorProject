import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ControlMessagesComponent } from 'src/app/helper/control-messages.component';
import { AppModule } from 'src/app/app.module';
import { ControlModule } from 'src/app/helper/control.module';
import { NgxPaginationModule } from 'ngx-pagination';

import { TopicRoutingModule } from './topic-routing.module';
import { TopicComponent } from './topic.component';
import { AddTopicComponent } from './add-topic/add-topic.component';
import { EditTopicComponent } from './edit-topic/edit-topic.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TopicRoutingModule,
        ControlModule,
        NgxPaginationModule,
        BsDatepickerModule.forRoot()
    ],
    declarations: [
        TopicComponent,
        AddTopicComponent,
        EditTopicComponent
    ]
})
export class TopicModule { }
