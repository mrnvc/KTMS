import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthApiService } from '../../api-services/auth/auth-api.service';

@Component({
  selector: 'app-landing-page',
  standalone: false,
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent {
  currentLang: string = localStorage.getItem('language') || 'bs';

  languages = [
    {
      code: 'bs',
      name: 'Bosanski',
      flagUrl: 'https://flagcdn.com/w40/ba.png'
    },
    {
      code: 'en',
      name: 'English',
      flagUrl: 'https://flagcdn.com/w40/gb.png'
    }
  ];

  constructor(
    private router: Router,
    private auth: AuthApiService,
    private translate: TranslateService
  ) { }

  switchLanguage(lang: string): void {
    this.currentLang = lang;
    localStorage.setItem('language', lang);
    this.translate.use(lang);
  }

  getCurrentLanguage() {
    return this.languages.find(lang => lang.code === this.currentLang);
  }

  goToLogin(): void {
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