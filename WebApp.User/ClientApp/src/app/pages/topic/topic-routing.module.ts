import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { TopicComponent } from './topic.component';

export const routes: Routes = [
    {
        path: '', component: TopicComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class TopicRoutingModule { }
