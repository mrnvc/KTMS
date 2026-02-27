import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {AuthApiService} from '../../api-services/auth/auth-api.service';

@Component({
  selector: 'app-landing-page',
  standalone: false,
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent {
  constructor(private router: Router, private auth: AuthApiService) { }

  goToLogin() {
    this.router.navigate(['/login']);
  }

  goToRegister(): void {
    this.router.navigate(['/register']);
  }

  goToTournaments(): void {
    this.router.navigate(['/tournaments']);
  }

  watchAsGuest(): void {
    this.auth.enterGuestMode();
    this.router.navigate(['/tournaments']);
  }
}
