import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-page',
  standalone: false,
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
})
export class LoginPageComponent {
  submitted = false;
  authError: string | null = null;
  showPassword = false;
  email = '';
  password = '';
  rememberMe = false;

  constructor(private auth: AuthService, private router: Router) {}

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  // ---- VALIDATION HELPERS (ključ za warnings) ----
  get emailValue(): string {
    return (this.email ?? '').trim();
  }

  get passwordValue(): string {
    return (this.password ?? '').trim();
  }

  get emailEmpty(): boolean {
    return this.emailValue.length === 0;
  }

  get passwordEmpty(): boolean {
    return this.passwordValue.length === 0;
  }

  get emailLooksLikeEmail(): boolean {
    return this.emailValue.includes('@');
  }

  get emailInvalid(): boolean {
    if (this.emailEmpty) return false; // empty ima svoju poruku
    if (!this.emailLooksLikeEmail) return this.emailValue.length < 3; // username min 3
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return !emailRegex.test(this.emailValue);
  }

  get canSubmit(): boolean {
    return !this.emailEmpty && !this.passwordEmpty && !this.emailInvalid;
  }
  // -----------------------------------------------

  handleSubmit(): void {
    this.submitted = true;
    this.authError = null;

    // Ako forma nije validna -> ne zovi API
    if (!this.canSubmit) {
      console.log("User did not enter credentials!");
      return;
    }

    this.auth.login(this.emailValue, this.password).subscribe({
      next: (res) => {
        if (res && res.token) {
          this.auth.saveToken(res.token, this.rememberMe);
          this.router.navigate(['/tournaments']);
          console.log("login successfull");
        } else {
          this.authError = 'Invalid email/username or password.';
        }
      },
      error: (err) => {
        if (err?.status === 401 || err?.status === 400) {
          this.authError = 'Invalid email/username or password.';
        } else {
          this.authError = 'Unable to sign in. Please try again later.';
        }
      },
    });
  }
}
