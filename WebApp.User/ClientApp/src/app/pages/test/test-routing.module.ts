import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { TestComponent } from './test.component';
import { HistoryTestComponent } from './history-test/history-test.component';
import { DetailTestComponent } from './detail-test/detail-test.component';

export const routes: Routes = [
    {
        path: ':userId', component: TestComponent
    },
    {
      path: 'history/view', component: HistoryTestComponent
    },
    {
      path: 'history/view/:id', component: DetailTestComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class TestRoutingModule { }
