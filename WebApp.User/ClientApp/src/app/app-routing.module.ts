import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FullComponent } from './layouts/full/full.component';

// import { AuthGuard } from './services/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    component: FullComponent,
    children: [
      {
        path: 'home',
        loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
      },
      {
        path: '',
        loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
      },
      {
        path: 'login',
        loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule)
      },
      {
        path: 'register',
        loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterModule)
      },
      {
        path: 'topic',
        loadChildren: () => import('./pages/topic/topic.module').then(m => m.TopicModule)
      },
      {
        path: 'flashcard',
        loadChildren: () => import('./pages/flashcard/flashcard.module').then(m => m.FlashcardModule)
      },
      {
        path: 'test',
        loadChildren: () => import('./pages/test/test.module').then(m => m.TestModule)
      },
      {
        path: 'profile',
        loadChildren: () => import('./pages/profile/profile.module').then(m => m.ProfileModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
