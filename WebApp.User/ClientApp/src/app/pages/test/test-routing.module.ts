import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { TestComponent } from './test.component';

export const routes: Routes = [
    {
        path: ':userId', component: TestComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class TestRoutingModule { }
