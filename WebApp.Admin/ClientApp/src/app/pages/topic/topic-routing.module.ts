import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { TopicComponent } from './topic.component';
import { AddTopicComponent } from './add-topic/add-topic.component';
import { EditTopicComponent } from './edit-topic/edit-topic.component';
export const routes: Routes = [
    {
        path: '', component: TopicComponent,
    },
    {
        path: 'add', component: AddTopicComponent
    },
    {
        path: 'edit/:id', component: EditTopicComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class TopicRoutingModule { }
