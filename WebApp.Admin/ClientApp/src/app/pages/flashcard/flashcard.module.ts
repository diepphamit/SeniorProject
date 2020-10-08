import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ControlMessagesComponent } from 'src/app/helper/control-messages.component';
import { AppModule } from 'src/app/app.module';
import { ControlModule } from 'src/app/helper/control.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { FlashcardRoutingModule } from './flashcard-routing.module';
import { FlashcardComponent } from './flashcard.component';
import { AddFlashcardComponent } from './add-flashcard/add-flashcard.component';
import { EditFlashcardComponent } from './edit-flashcard/edit-flashcard.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FlashcardRoutingModule,
        ControlModule,
        NgxPaginationModule,
        BsDatepickerModule.forRoot()
    ],
    declarations: [
        FlashcardComponent,
        AddFlashcardComponent,
        EditFlashcardComponent
    ]
})
export class FlashcardModule { }
