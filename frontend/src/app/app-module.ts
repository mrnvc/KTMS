import {LOCALE_ID, NgModule, provideBrowserGlobalErrorListeners} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { LandingPageComponent } from './modules/landing-page/landing-page.component';
import { FooterComponent } from './modules/shared/components/footer/footer.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import { BackButtonComponent } from './modules/shared/components/back-button/back-button.component';
import { TournamentsPageComponent } from './modules/tournaments-page/tournaments-page.component';
import {MatIcon} from '@angular/material/icon';
import {MatFormField} from '@angular/material/input';
import {MatOption, MatSelect} from '@angular/material/select';
import {MatDialogActions, MatDialogContent} from '@angular/material/dialog';
import {MatButton} from '@angular/material/button';
import { LoginComponent } from './modules/auth/login/login.component';
import {TranslatePipe} from '@ngx-translate/core';
import { RegisterComponent } from './modules/auth/register/register.component';
import {loadingBarInterceptor} from './core/interceptors/loading-bar-interceptor.service';
import {authInterceptor} from './core/interceptors/auth-interceptor.service';
import {errorLoggingInterceptor} from './core/interceptors/error-logging-interceptor.service';
import {MatProgressSpinner} from '@angular/material/progress-spinner';
import {materialModules} from './modules/shared/material-modules';
import {SharedModule} from './modules/shared/shared-module';

@NgModule({
  declarations: [
    App,
    LandingPageComponent,
    FooterComponent,
    LoginComponent,
    BackButtonComponent,
    RegisterComponent,
    TournamentsPageComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatIcon,
    MatFormField,
    MatSelect,
    MatOption,
    MatDialogContent,
    MatDialogActions,
    MatButton,
    TranslatePipe,
    MatProgressSpinner,
    materialModules,
    SharedModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([
      loadingBarInterceptor,
      authInterceptor,
      errorLoggingInterceptor
    ])),
    { provide: LOCALE_ID, useValue: 'bs-BA' }
  ],
  exports: [
    BackButtonComponent
  ],
  bootstrap: [App]
})
export class AppModule { }
