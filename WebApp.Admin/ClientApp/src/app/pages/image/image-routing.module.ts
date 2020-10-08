import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ImageComponent } from './image.component';
import { AddImageComponent } from './add-image/add-image.component';

export const routes: Routes = [
    {
        path: '', component: ImageComponent,
    },
    {
        path: 'add', component: AddImageComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class ImageRoutingModule { }
