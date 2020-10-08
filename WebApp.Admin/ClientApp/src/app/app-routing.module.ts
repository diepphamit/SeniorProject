import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlankComponent } from './layouts/blank/blank.component';
import { FullComponent } from './layouts/full/full.component';
import { AuthGuard } from './services/auth-guard.service';
// import { AuthGuard } from './services/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '',
    component: BlankComponent,
    children: [
      {
        path: 'login',
        loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule)
      }
    ]
  },
  {
    path: '',
    component: BlankComponent,
    children: [
      {
        path: 'test',
        loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
      }
    ]
  },
  {
    path: '',
    component: FullComponent,
    //canActivateChild: [AuthGuard],
    children: [
      {
        path: 'home',
        loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
      },
      {
        path: 'users',
        loadChildren: () => import('./pages/users-management/users-management.module').then(m => m.UsersManagementModule)
      },
      {
        path: 'topics',
        loadChildren: () => import('./pages/topic/topic.module').then(m => m.TopicModule)
      },
      {
        path: 'pronunciations',
        loadChildren: () => import('./pages/pronunciation/pronunciation.module').then(m => m.PronunciationModule)
      },
      {
        path: 'flashcards',
        loadChildren: () => import('./pages/flashcard/flashcard.module').then(m => m.FlashcardModule)
      },
      {
        path: 'images',
        loadChildren: () => import('./pages/image/image.module').then(m => m.ImageModule)
      },
      // {
      //   path: 'pictures',
      //   loadChildren: () => import('./pages/picture/picture.module').then(m => m.PictureModule)
      // },
      // {
      //   path: 'suppliers',
      //   loadChildren: () => import('./pages/supplier/supplier.module').then(m => m.SupplierModule)
      // },
      // {
      //   path: 'orderdetails',
      //   loadChildren: () => import('./pages/order-detail/order-detail.module').then(m => m.OrderDetailModule)
      // },
      // {
      //   path: 'statistical',
      //   loadChildren: () => import('./pages/statistical/statistical.module').then(m => m.StatisticalModule)
      // },
      // {
      //   path: 'orders',
      //   loadChildren: () => import('./pages/order/order.module').then(m => m.OrderModule)
      // },
      // {
      //   path: 'branches',
      //   loadChildren: () => import('./pages/branch/branch.module').then(m => m.BranchModule)
      // },
      // {
      //   path: 'branchproducts',
      //   loadChildren: () => import('./pages/branch-product/branch-product.module').then(m => m.BranchProductModule)
      // }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
