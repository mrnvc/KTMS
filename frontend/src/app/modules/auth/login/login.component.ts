import { Component, inject } from '@angular/core';
import {BaseComponent} from '../../../core/components/base-classes/base-component';
import {FormBuilder, Validators} from '@angular/forms';
import {AuthFacadeService} from '../../../core/services/auth/auth-facade.service';
import {ActivatedRoute, Router} from '@angular/router';
import {CurrentUserService} from '../../../core/services/auth/current-user.service';
import {LoginCommand} from '../../../api-services/auth/auth-api.model';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent extends BaseComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthFacadeService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private currentUser = inject(CurrentUserService);
  hidePassword = true;

  form = this.fb.group({
    email: ['admin@ktms.local', [Validators.required, Validators.email]],
    password: ['Admin123!', [Validators.required]],
    rememberMe: [false],
  });

  onSubmit(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    const payload: LoginCommand = {
      email: this.form.value.email ?? '',
      password: this.form.value.password ?? ''
    };

    this.auth.login(payload).subscribe({
      next: () => {
        this.stopLoading();
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
        if (returnUrl) {
          this.router.navigateByUrl(returnUrl);
        } else {
          if (this.auth.isAdmin()) {
            this.router.navigate(['/admin']);
          } else {
            this.router.navigate(['/tournaments']);
          }
        }
      },
      error: (err) => {
        this.stopLoading('Invalid credentials. Please try again.');
        console.error('Login error:', err);
      },
    });
  }
}
