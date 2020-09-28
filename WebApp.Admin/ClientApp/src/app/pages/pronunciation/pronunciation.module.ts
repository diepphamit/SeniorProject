import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ControlMessagesComponent } from 'src/app/helper/control-messages.component';
import { AppModule } from 'src/app/app.module';
import { ControlModule } from 'src/app/helper/control.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { PronunciationRoutingModule } from './pronunciation-routing.module';
import { PronunciationComponent } from './pronunciation.component';
import { AddPronunciationComponent } from './add-pronunciation/add-pronunciation.component';
import { EditPronunciationComponent } from './edit-pronunciation/edit-pronunciation.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        PronunciationRoutingModule,
        ControlModule,
        NgxPaginationModule,
        BsDatepickerModule.forRoot()
    ],
    declarations: [
        PronunciationComponent,
        AddPronunciationComponent,
        EditPronunciationComponent
    ]
})
export class PronunciationModule { }
