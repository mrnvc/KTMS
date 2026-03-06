import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {TournamentsPageComponent} from '../tournaments-page/tournaments-page.component';
import {ContestantsPageComponent} from '../contestants-page/contestants-page.component';
import {AdminDashboardComponent} from './admin-dashboard/admin-dashboard.component';
// Assuming components exist or will be created
// import { CitiesComponent } from '../cities/cities.component';
// import { GendersComponent } from '../genders/genders.component';
// import { RolesComponent } from '../roles/roles.component';

const routes: Routes = [
  {
    path: '',
    component: AdminDashboardComponent,
    children: [
      // TOURNAMENTS
      {
        path: 'tournaments',
        component: TournamentsPageComponent,
      },
      // CONTESTANTS
      {
        path: 'contestants',
        component: ContestantsPageComponent,
      },
      // CITIES
      {
        path: 'cities',
        component: TournamentsPageComponent, // Placeholder, replace with CitiesComponent
      },
      // GENDERS
      {
        path: 'genders',
        component: TournamentsPageComponent, // Placeholder, replace with GendersComponent
      },
      // ROLES
      {
        path: 'roles',
        component: TournamentsPageComponent, // Placeholder, replace with RolesComponent
      },
      // default admin route → /admin/tournaments
      {
        path: '',
        redirectTo: 'tournaments',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
