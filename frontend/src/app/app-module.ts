import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { FooterComponent } from './footer/footer.component';
import { LoginPageComponent } from './login-page/login-page.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';
import { BackButtonComponent } from './shared/back-button/back-button.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { TournamentsPageComponent } from './tournaments-page/tournaments-page.component';

@NgModule({
  declarations: [
    App,
    LandingPageComponent,
    FooterComponent,
    LoginPageComponent,
    BackButtonComponent,
    RegisterPageComponent,
    TournamentsPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient()
  ],
  exports: [
    BackButtonComponent
  ],
  bootstrap: [App]
})
export class AppModule { }
