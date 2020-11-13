import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FlashcardComponent } from './flashcard.component';
import { FlashcardRoutingModule } from './flashcard-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ControlModule } from 'src/app/helpers/control.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { CreateFlashcardComponent } from './create-flashcard/create-flashcard.component';
import { NgxFileDropModule } from 'ngx-file-drop';
import { MyFlashcardComponent } from './my-flashcard/my-flashcard.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlashcardRoutingModule,
    ControlModule,
    NgxFileDropModule,
    NgxPaginationModule
  ],
  declarations: [
    FlashcardComponent,
    CreateFlashcardComponent,
    MyFlashcardComponent
  ]
})
export class FlashcardModule { }
