import { Component, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-back-button',
  standalone: false,
  templateUrl: './back-button.component.html',
  styleUrl: './back-button.component.scss',
})
export class BackButtonComponent {
  /** Tekst na dugmetu */
  @Input() label: string = 'Back';

  /** Ruta na koju ide ako nema browser history ili ako forceFallback = true */
  @Input() fallbackUrl: string = '/';

  /** Ako želiš uvijek ići na fallbackUrl (npr. uvijek na home) */
  @Input() forceFallback: boolean = false;

  /** Opcionalno: ikona lijevo */
  @Input() showIcon: boolean = true;

  constructor(private location: Location, private router: Router) {}

  async onBack(): Promise<void> {
    if (this.forceFallback) {
      await this.router.navigateByUrl(this.fallbackUrl);
      return;
    }

    // Provjera da li postoji history (approx)
    if (window.history.length > 1) {
      this.location.back();
      return;
    }

    // fallback (npr. home)
    await this.router.navigateByUrl(this.fallbackUrl);
  }
}
