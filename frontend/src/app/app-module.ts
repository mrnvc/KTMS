import { LOCALE_ID, NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { LandingPageComponent } from './modules/landing-page/landing-page.component';
import { FooterComponent } from './modules/shared/components/footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { BackButtonComponent } from './modules/shared/components/back-button/back-button.component';
import { TournamentsPageComponent } from './modules/tournaments-page/tournaments-page.component';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatFormField } from '@angular/material/input';
import { MatOption, MatSelect } from '@angular/material/select';
import { MatDialogActions, MatDialogContent } from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { LoginComponent } from './modules/auth/login/login.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { RegisterComponent } from './modules/auth/register/register.component';
import { loadingBarInterceptor } from './core/interceptors/loading-bar-interceptor.service';
import { authInterceptor } from './core/interceptors/auth-interceptor.service';
import { errorLoggingInterceptor } from './core/interceptors/error-logging-interceptor.service';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { materialModules } from './modules/shared/material-modules';
import { SharedModule } from './modules/shared/shared-module';
import { HttpClient } from '@angular/common/http';
import { CustomTranslateLoader } from './core/services/custom-translate-loader';
import { AdminModule } from './modules/admin/admin-module';
import { ContestantsPageComponent } from './modules/contestants-page/contestants-page.component';
import { AddContestantFormComponent } from './modules/contestants-page/add-contestant-form/add-contestant-form.component';
import { EditContestantFormComponent } from './modules/contestants-page/edit-contestant-form/edit-contestant-form.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { PhoneFormatPipe } from './modules/shared/pipes/phone-format.pipe';
import { ClubWithoutCountryPipe } from './modules/shared/pipes/club-without-country-name.pipe';
import { JudgeLicenseFormatPipe } from './modules/shared/pipes/licence-format.pipe';
import { QRCodeComponent } from 'angularx-qrcode';
import { JudgesPageComponent } from './modules/judges-page/judges-page.component';
import { AddJudgeFormComponent } from './modules/judges-page/add-judge-form/add-judge-form.component';
import { EditJudgeFormComponent } from './modules/judges-page/edit-judge-form/edit-judge-form.component';

@NgModule({
  declarations: [
    App,
    LandingPageComponent,
    FooterComponent,
    LoginComponent,
    BackButtonComponent,
    RegisterComponent,
    TournamentsPageComponent,
    ContestantsPageComponent,
    AddContestantFormComponent,
    EditContestantFormComponent,
    PhoneFormatPipe,
    ClubWithoutCountryPipe,
    JudgesPageComponent,
    AddJudgeFormComponent,
    JudgeLicenseFormatPipe,
    EditJudgeFormComponent
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
    MatIconModule,
    MatProgressSpinner,
    MatAutocompleteModule,
    QRCodeComponent,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => new CustomTranslateLoader(http),
        deps: [HttpClient]
      }
    }),
    materialModules,
    SharedModule,
    AdminModule
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
