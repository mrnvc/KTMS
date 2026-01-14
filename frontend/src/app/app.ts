import {Component, computed, signal} from '@angular/core';
import {AuthService} from './services/auth.service';
import {NavigationEnd, Router} from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('frontend');

  private currentUrl = signal<string>('/');

  readonly showProfileIcon = computed(() => {
    const url = this.currentUrl();
    const hideOn =
      url === '/' ||
      url.startsWith('/?') ||
      url.startsWith('/login') ||
      url.startsWith('/register');

    return this.auth.isLoggedIn() && !this.auth.isGuest?.() && !hideOn;
  });

  constructor(public auth: AuthService, private router: Router) {
    // set initial
    this.currentUrl.set(this.router.url);

    // update on navigation end (stabilno)
    this.router.events
      .pipe(filter((e): e is NavigationEnd => e instanceof NavigationEnd))
      .subscribe(e => this.currentUrl.set(e.urlAfterRedirects));
  }

  goToProfile(): void {
    this.router.navigate(['/profile']);
  }
}
