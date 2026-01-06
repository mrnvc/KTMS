import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LandingPageComponent} from './landing-page/landing-page.component';
import {LoginPageComponent} from './login-page/login-page.component';
import {RegisterPageComponent} from './register-page/register-page.component';
import {TournamentsPageComponent} from './tournaments-page/tournaments-page.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent},
  { path: 'login', component: LoginPageComponent }, // Login page
  { path: 'register', component: RegisterPageComponent }, //Register page
  { path: "tournaments", component: TournamentsPageComponent },
  { path: "tournaments/:status", component: TournamentsPageComponent },
  { path: '**', redirectTo: '' } // fallback
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
