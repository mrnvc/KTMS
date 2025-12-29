import {Component, ElementRef, ViewChild} from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-page',
  standalone: false,
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
})
export class LoginPageComponent {
  showPassword = false;
  email = '';
  password = '';
  rememberMe = false;

  onBack(): void {
    console.log('Back to home clicked');
  }

  constructor(private auth: AuthService, private router: Router) {}

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  handleSubmit(): void {
    this.auth.login(this.email, this.password).subscribe({
      next: (res) => {
        console.log('LOGIN RESPONSE:', res);

        if (res && res.token) {
          localStorage.setItem('token', res.token);
          console.log('Login successful');
          // this.router.navigate(['/dashboard']);
        } else {
          console.log('Login failed');
        }
      },
      error: (err) => {
        console.error(err);
        alert('Error connecting to server');
      }
    });
  }
}
