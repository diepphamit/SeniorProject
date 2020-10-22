import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ChatbotService } from './services/chatbot.service';
import { TestComponent } from './test/test.component';
import { TestService } from './services/test.service';
import { CoreModule } from './core/core.module';
import { FullComponent } from './layouts/full/full.component';
import { AppRoutingModule } from './app-routing.module';
import { PopupComponent } from './helpers/popup/popup.component';
import { AuthService } from './services/auth.service';
import { ACCESS_TOKEN } from './constants/db-keys';
import { JwtModule } from '@auth0/angular-jwt';
import { ValidationService } from './services/validation.service';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN);
}

@NgModule({
  declarations: [
    AppComponent,
    FullComponent,
    PopupComponent,
    TestComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CoreModule,
    AppRoutingModule,
    ReactiveFormsModule,
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
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    ChatbotService,
    TestService,
    AuthService,
    ValidationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
