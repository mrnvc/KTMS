import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LandingPageComponent} from './modules/landing-page/landing-page.component';
import {TournamentsPageComponent} from './modules/tournaments-page/tournaments-page.component';
import {LoginComponent} from './modules/auth/login/login.component';
import {RegisterComponent} from './modules/auth/register/register.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent},
  { path: 'login', component: LoginComponent }, // Login page
  { path: 'register', component: RegisterComponent }, //Register page
  { path: "tournaments", component: TournamentsPageComponent },
  { path: "tournaments/:status", component: TournamentsPageComponent },
  { path: '**', redirectTo: '' } // fallback
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
