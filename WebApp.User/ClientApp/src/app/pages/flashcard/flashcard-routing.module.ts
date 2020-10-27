import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FlashcardComponent } from './flashcard.component';

export const routes: Routes = [
    {
        path: 'topic/:topicId', component: FlashcardComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class FlashcardRoutingModule { }
