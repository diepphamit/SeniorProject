import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
// import { LoadingBarRouterModule } from 'ngx-loading-bar/router';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { FullComponent } from './layouts/full/full.component';
import { BlankComponent } from './layouts/blank/blank.component';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { ACCESS_TOKEN } from './constants/db-keys';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';
import { TopicService } from './services/topic.service';
import { PronunciationService } from './services/pronunciation.service';
import { FlashcardService } from './services/flashcard.service';
import { AuthGuard } from './services/auth-guard.service';
import { ImageService } from './services/image.service';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN);
}

@NgModule({
  declarations: [
    AppComponent,
    FullComponent,
    BlankComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CoreModule,
    AppRoutingModule,
    // LoadingBarRouterModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
          // whitelistedDomains: [
          //   'localhost:5000',
          //   'localhost:44327',
          //   '52.77.233.77:8081'
          // ],
          // blacklistedRoutes: [
          //   'localhost:5000/api/auth/login',
          //   'localhost:44327/api/auth/login'
          // ]
      }
    }),
    ModalModule.forRoot()
  ],
  providers: [
    AuthGuard,
    AuthService,
    UserService,
    TopicService,
    PronunciationService,
    FlashcardService,
    ImageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
