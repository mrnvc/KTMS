import { Injectable } from '@angular/core';

export type ThemeMode = 'light' | 'dark';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private readonly storageKey = 'ktms-theme';

  initTheme(): void {
    const savedTheme = localStorage.getItem(this.storageKey) as ThemeMode | null;

    if (savedTheme) {
      this.setTheme(savedTheme);
      return;
    }

    this.setTheme('light');
  }

  setTheme(theme: ThemeMode): void {
    document.body.classList.remove('light-theme', 'dark-theme');
    document.body.classList.add(`${theme}-theme`);

    localStorage.setItem(this.storageKey, theme);
  }

  toggleTheme(): void {
    const isDark = document.body.classList.contains('dark-theme');
    this.setTheme(isDark ? 'light' : 'dark');
  }

  getCurrentTheme(): ThemeMode {
    return document.body.classList.contains('dark-theme') ? 'dark' : 'light';
  }
}