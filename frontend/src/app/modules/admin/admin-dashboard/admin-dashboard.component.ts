import { Component, inject } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { ThemeService } from '../../../core/services/theme.service';
import { FirebaseMessagingService } from '../../../core/services/firebase-messaging.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: false,
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss',
})
export class AdminDashboardComponent {
  private translate = inject(TranslateService);
  private themeService = inject(ThemeService);
  auth = inject(AuthFacadeService);

  currentLang: string;

  languages = [
    { code: 'bs', name: 'Bosanski', flag: '🇧🇦' },
    { code: 'en', name: 'English', flag: '🇬🇧' }
  ];

  constructor() {
    this.currentLang = this.translate.currentLang || 'bs';
  }

  switchLanguage(langCode: string): void {
    this.currentLang = langCode;
    this.translate.use(langCode);
    localStorage.setItem('language', langCode);
  }

  getCurrentLanguage() {
    return this.languages.find(lang => lang.code === this.currentLang);
  }


  toggleTheme(): void {
    this.themeService.toggleTheme();
  }

  private readonly firebaseMessaging = inject(FirebaseMessagingService);

  async enableNotifications(): Promise<void> {
    const token = await this.firebaseMessaging.requestPermissionAndGetToken();

    if (token) {
      console.log('Copy this token to Swagger:', token);
      alert('FCM token generated. Check console.');
    }
  }
}
