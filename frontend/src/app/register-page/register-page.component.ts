import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse, HttpResponse} from '@angular/common/http';
import {RoleDto} from '../models/role.model';
import {CityDto} from '../models/city.model';
import {GenderDto} from '../models/gender.model';
import {LookupService} from '../services/lookup.service';

@Component({
  selector: 'app-register-page',
  standalone: false,
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss'
})
export class RegisterPageComponent {
  form!: FormGroup;

  roles: RoleDto[] = [];
  cities: CityDto[] = [];
  genders: GenderDto[] = [];

  organizerRoleId: number | null = null;
  athleteRoleId: number | null = null;

  isSubmitting = false;
  errorMsg = '';

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private lookup: LookupService
  ) {
    this.form = this.fb.group({ // <-- this.form
      roleId: [null, Validators.required],
      cityId: [null, Validators.required],
      genderId: [null, Validators.required],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],

      // UI-only (ne šaljemo backendu)
      confirmPassword: ['', Validators.required],
      agreeTerms: [false, Validators.requiredTrue],
    }, { validators: this.passwordsMatchValidator });
  }

  ngOnInit(): void {
    this.lookup.getRoles().subscribe((roles) => {
      this.roles = roles;

      const organizer = roles.find(r => r.title.toLowerCase() === 'organizer');
      const athlete = roles.find(r => r.title.toLowerCase() === 'athlete');

      this.organizerRoleId = organizer?.id ?? null;
      this.athleteRoleId = athlete?.id ?? null;

      const defaultRoleId = this.organizerRoleId ?? this.athleteRoleId ?? null;

      Promise.resolve().then(() => {
        this.form.patchValue({ roleId: defaultRoleId });
      });
    });

    this.lookup.getCities().subscribe((cities) => (this.cities = cities));
    this.lookup.getGenders().subscribe((genders) => (this.genders = genders));
  }

  setRole(roleId: number | null): void {
    if (roleId == null) return;
    this.form.patchValue({ roleId });
  }

  passwordsMatchValidator(group: FormGroup) {
    const p = group.get('password')?.value;
    const c = group.get('confirmPassword')?.value;
    return p && c && p === c ? null : { passwordMismatch: true };
  }

  submit(): void {
    this.errorMsg = '';

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    // mismatch check (jer je group validator)
    if (this.form.errors?.['passwordMismatch']) {
      return;
    }

    this.isSubmitting = true;
    const v = this.form.getRawValue();

    this.auth.register({
      roleId: v.roleId!,
      cityId: v.cityId!,
      genderId: v.genderId!,
      name: v.name!,
      surname: v.surname!,
      phoneNumber: v.phoneNumber!,
      email: v.email!,
      dateOfBirth: v.dateOfBirth!,
      username: v.username!,
      password: v.password!,
    }).subscribe({
      next: () => {
        void this.router.navigate(['/login'], {
          queryParams: { u: v.username, registered: '1' },
        });
      },
      error: (err: HttpErrorResponse) => {
        console.group('Register page error');
        console.error('Status:', err.status);
        console.error('Backend response:', err.error);
        console.groupEnd();

        this.errorMsg = 'Registration Failed.';
        this.isSubmitting = false;
      },
      complete: () => {
        console.log("Registration Complete");
        this.isSubmitting = false;
      },
    });
  }
}
