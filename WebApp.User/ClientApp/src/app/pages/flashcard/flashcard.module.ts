import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FlashcardComponent } from './flashcard.component';
import { FlashcardRoutingModule } from './flashcard-routing.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ControlModule } from 'src/app/helpers/control.module';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlashcardRoutingModule,
    ControlModule,
    NgxPaginationModule
  ],
  declarations: [FlashcardComponent]
})
export class FlashcardModule { }
