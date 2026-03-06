import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LandingPageComponent} from './modules/landing-page/landing-page.component';
import {TournamentsPageComponent} from './modules/tournaments-page/tournaments-page.component';
import {LoginComponent} from './modules/auth/login/login.component';
import {RegisterComponent} from './modules/auth/register/register.component';
import {AdminDashboardComponent} from './modules/admin/admin-dashboard/admin-dashboard.component';
import {myAuthGuard, myAuthData} from './core/guards/my-auth-guard';

const routes: Routes = [
  { path: '', component: LandingPageComponent},
  { path: 'login', component: LoginComponent }, // Login page
  { path: 'register', component: RegisterComponent }, //Register page
  { path: "tournaments", component: TournamentsPageComponent },
  { path: "tournaments/:status", component: TournamentsPageComponent },
  { path: 'admin', component: AdminDashboardComponent, canActivate: [myAuthGuard], data: myAuthData({requireAuth: true, requireAdmin: true}), children: [
    { path: 'tournaments', component: TournamentsPageComponent },
    { path: '', redirectTo: 'tournaments', pathMatch: 'full' }
  ] },
  { path: '**', redirectTo: '' } // fallback
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
