import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FlashcardComponent } from './flashcard.component';
import { CreateFlashcardComponent } from './create-flashcard/create-flashcard.component';
import { MyFlashcardComponent } from './my-flashcard/my-flashcard.component';

export const routes: Routes = [
    {
        path: 'topic/:topicId', component: FlashcardComponent
    },
    {
      path: 'add', component: CreateFlashcardComponent
    },
    {
      path: 'myflashcard', component: MyFlashcardComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class FlashcardRoutingModule { }
