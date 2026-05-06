import { Component, inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { finalize, forkJoin } from 'rxjs';
import { environment } from '../../../../enviroments/enviroment';
import { ToasterService } from '../../../core/services/toaster.service';
import { AuthApiService } from '../../../api-services/auth/auth-api.service';
import { RegisterCommand } from '../../../api-services/auth/auth-api.model';

interface Option {
  id: number;
  name: string;
}

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly authApi = inject(AuthApiService);
  private readonly toaster = inject(ToasterService);

  private readonly apiUrl = `${environment.apiUrl}/api`;

  form!: FormGroup;
  isLoading = false;
  hidePassword = true;
  hideConfirmPassword = true;
  today = new Date().toISOString().split('T')[0];

  cities: Option[] = [];
  genders: Option[] = [];

  // Ako roleId za običnog korisnika/contestanta nije 3, promijeni ovdje
  private readonly defaultRoleId = 3;

  ngOnInit(): void {
    this.initForm();
    this.loadDropdownData();
  }

  private initForm(): void {
    this.form = this.fb.group(
      {
        name: [
          '',
          [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(50),
            Validators.pattern(/^[A-Za-zČĆŽŠĐčćžšđ\s'-]+$/)
          ]
        ],
        surname: [
          '',
          [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(50),
            Validators.pattern(/^[A-Za-zČĆŽŠĐčćžšđ\s'-]+$/)
          ]
        ],
        phoneNumber: [
          '',
          [
            Validators.required,
            Validators.maxLength(30),
            Validators.pattern(/^\+?[0-9\s\-]{7,20}$/)
          ]
        ],
        email: [
          '',
          [
            Validators.required,
            Validators.email,
            Validators.maxLength(100)
          ]
        ],
        dateOfBirth: [
          '',
          [
            Validators.required,
            this.notFutureDateValidator
          ]
        ],
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(30),
            Validators.pattern(/^[a-zA-Z0-9._-]+$/)
          ]
        ],
        cityId: [null, Validators.required],
        genderId: [null, Validators.required],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(100)
          ]
        ],
        confirmPassword: ['', Validators.required]
      },
      {
        validators: this.passwordMatchValidator
      }
    );
  }

  private loadDropdownData(): void {
    forkJoin({
      cities: this.http.get<any[]>(`${this.apiUrl}/City/GetCities`),
      genders: this.http.get<any[]>(`${this.apiUrl}/Gender/GetGenders`)
    }).subscribe({
      next: data => {
        this.cities = data.cities.map(city => ({
          id: city.id,
          name: `${city.cityName}, ${this.translateCountryName(city.country)}`
        }));

        this.genders = data.genders.map(gender => ({
          id: gender.id,
          name: this.translateGenderName(gender.name)
        }));
      },
      error: err => {
        console.error('Error loading register dropdown data:', err);
        this.toaster.error('Failed to load form data.');
      }
    });
  }

  private notFutureDateValidator(control: AbstractControl): ValidationErrors | null {
    if (!control.value) {
      return null;
    }

    const selectedDate = new Date(control.value);
    const today = new Date();

    selectedDate.setHours(0, 0, 0, 0);
    today.setHours(0, 0, 0, 0);

    return selectedDate > today ? { futureDate: true } : null;
  }

  private passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (!password || !confirmPassword) {
      return null;
    }

    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  onSubmit(): void {
    if (this.isLoading) {
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toaster.error('Please check the entered data.');
      return;
    }

    const value = this.form.getRawValue();

    const command: RegisterCommand = {
      user: {
        roleId: this.defaultRoleId,
        cityId: value.cityId,
        genderId: value.genderId,
        name: value.name.trim(),
        surname: value.surname.trim(),
        phoneNumber: value.phoneNumber.trim(),
        dateOfBirth: value.dateOfBirth,
        username: value.username.trim(),
        email: value.email.trim().toLowerCase(),
        password: value.password
      }
    };

    this.isLoading = true;

    this.authApi.register(command)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: () => {
          this.toaster.success('Registration successful. You can now log in.');
          this.router.navigate(['/login']);
        },
        error: err => {
          console.error('Register error:', err);

          const message =
            err?.error?.message ||
            err?.error?.title ||
            err?.error?.detail ||
            err?.error ||
            'Registration failed.';

          this.toaster.error(message);
          this.applyBackendErrorToField(message);
        }
      });
  }

  hasError(controlName: string): boolean {
    const control = this.form?.get(controlName);
    return !!control && control.invalid && control.touched;
  }

  hasPasswordMismatch(): boolean {
    const confirmPassword = this.form?.get('confirmPassword');
    return !!confirmPassword && confirmPassword.touched && !!this.form?.errors?.['passwordMismatch'];
  }

  getErrorMessage(controlName: string): string {
    const control = this.form?.get(controlName);

    if (!control || !control.errors) {
      return '';
    }

    if (control.errors['backend']) {
      return control.errors['backend'];
    }

    if (control.errors['required']) {
      return 'This field is required.';
    }

    if (control.errors['minlength']) {
      return `Minimum ${control.errors['minlength'].requiredLength} characters required.`;
    }

    if (control.errors['maxlength']) {
      return `Maximum ${control.errors['maxlength'].requiredLength} characters allowed.`;
    }

    if (control.errors['email']) {
      return 'Enter a valid email address.';
    }

    if (control.errors['futureDate']) {
      return 'Date of birth cannot be in the future.';
    }

    if (control.errors['pattern']) {
      if (controlName === 'name' || controlName === 'surname') {
        return 'Only letters are allowed.';
      }

      if (controlName === 'phoneNumber') {
        return 'Enter a valid phone number.';
      }

      if (controlName === 'username') {
        return 'Username cannot contain spaces or special characters.';
      }
    }

    return 'Invalid value.';
  }

  private applyBackendErrorToField(message: string): void {
    const lowerMessage = message.toLowerCase();

    if (lowerMessage.includes('email')) {
      this.setBackendError('email', message);
      return;
    }

    if (lowerMessage.includes('username')) {
      this.setBackendError('username', message);
      return;
    }

    if (lowerMessage.includes('phone')) {
      this.setBackendError('phoneNumber', message);
    }
  }

  private setBackendError(controlName: string, message: string): void {
    const control = this.form.get(controlName);

    if (!control) {
      return;
    }

    control.setErrors({
      ...(control.errors || {}),
      backend: message
    });

    control.markAsTouched();
    control.markAsDirty();
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }

  goBackHome(): void {
    this.router.navigate(['/']);
  }

  private translateGenderName(genderName: string): string {
    const normalized = genderName.toLowerCase();

    if (normalized === 'male') {
      return 'Muški';
    }

    if (normalized === 'female') {
      return 'Ženski';
    }

    return genderName;
  }

  private translateCountryName(countryName: string): string {
    if (countryName === 'Bosnia and Herzegovina') {
      return 'Bosna i Hercegovina';
    }

    if (countryName === 'Croatia') {
      return 'Hrvatska';
    }

    if (countryName === 'Serbia') {
      return 'Srbija';
    }

    return countryName;
  }
}