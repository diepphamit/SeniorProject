import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { PronunciationComponent } from './pronunciation.component';
import { AddPronunciationComponent } from './add-pronunciation/add-pronunciation.component';
import { EditPronunciationComponent } from './edit-pronunciation/edit-pronunciation.component';
export const routes: Routes = [
    {
        path: '', component: PronunciationComponent,
    },
    {
        path: 'add', component: AddPronunciationComponent
    },
    {
        path: 'edit/:id', component: EditPronunciationComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class PronunciationRoutingModule { }
