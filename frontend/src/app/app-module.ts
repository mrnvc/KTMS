import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { FooterComponent } from './footer/footer.component';
import { LoginPageComponent } from './login-page/login-page.component';
import {FormsModule} from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';
import { BackButtonComponent } from './shared/back-button/back-button.component';

@NgModule({
  declarations: [
    App,
    LandingPageComponent,
    FooterComponent,
    LoginPageComponent,
    BackButtonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient()
  ],
  bootstrap: [App]
})
export class AppModule { }
