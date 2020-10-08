import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FlashcardComponent } from './flashcard.component';
import { AddFlashcardComponent } from './add-flashcard/add-flashcard.component';
import { EditFlashcardComponent } from './edit-flashcard/edit-flashcard.component';

export const routes: Routes = [
    {
        path: '', component: FlashcardComponent,
    },
    {
        path: 'add', component: AddFlashcardComponent
    },
    {
        path: 'edit/:id', component: EditFlashcardComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class FlashcardRoutingModule { }
