import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';

import { CreateContestantRequest } from '../../../api-services/contestants/create-contestant-request.model';
import { ContestantsApiService } from '../../../api-services/contestants/contestants-api.service';
import { ToasterService } from '../../../core/services/toaster.service';

interface Option {
  id: number;
  name: string;
}

interface AddContestantDialogData {
  genders: Option[];
  cities: Option[];
  belts: Option[];
  clubs: Option[];
}

@Component({
  selector: 'app-add-contestant-form',
  standalone: false,
  templateUrl: './add-contestant-form.component.html',
  styleUrl: './add-contestant-form.component.scss'
})
export class AddContestantFormComponent implements OnInit {
  private readonly dialogRef = inject(MatDialogRef<AddContestantFormComponent>);
  private readonly fb = inject(FormBuilder);
  private readonly contestantsApi = inject(ContestantsApiService);
  private readonly toaster = inject(ToasterService);
  private readonly dialogData = inject<AddContestantDialogData>(MAT_DIALOG_DATA);

  form!: FormGroup;
  isLoading = false;
  today = new Date().toISOString().split('T')[0];

  genders: Option[] = this.dialogData.genders;
  cities: Option[] = this.dialogData.cities;
  belts: Option[] = this.dialogData.belts;
  clubs: Option[] = this.dialogData.clubs;

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    this.form = this.fb.group({
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
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(100)
        ]
      ],
      genderId: [null, Validators.required],
      cityId: [null, Validators.required],
      beltId: [null, Validators.required],
      clubId: [null, Validators.required]
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

  save(): void {
    if (this.isLoading) {
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();

      const firstInvalidControl = this.getFirstInvalidControlName();

      if (firstInvalidControl) {
        this.toaster.error(this.getFrontendValidationMessage(firstInvalidControl));
      } else {
        this.toaster.error('Please check the entered data.');
      }

      return;
    }

    const value = this.form.getRawValue();

    const command: CreateContestantRequest = {
      name: value.name.trim(),
      surname: value.surname.trim(),
      phoneNumber: value.phoneNumber.trim(),
      email: value.email.trim(),
      dateOfBirth: value.dateOfBirth,
      username: value.username.trim(),
      password: value.password,
      cityId: value.cityId,
      genderId: value.genderId,
      beltId: value.beltId,
      clubId: value.clubId
    };

    this.isLoading = true;

    this.contestantsApi.createContestant(command)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: () => {
          this.toaster.success('Contestant added successfully.');
          this.dialogRef.close(true);
        },
        error: err => {
          console.error('Create contestant error:', err);

          const message =
            err?.error?.message ||
            err?.error?.title ||
            err?.error?.detail ||
            err?.error ||
            'Error while adding contestant.';

          this.toaster.error(message);
          this.applyBackendErrorToField(message);
        }
      });
  }

  onCancel(): void {
    if (this.isLoading) {
      return;
    }

    this.dialogRef.close();
  }

  hasError(controlName: string): boolean {
    const control = this.form?.get(controlName);
    return !!control && control.invalid && control.touched;
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

    if (control.errors['futureDate']) {
      return 'Date of birth cannot be in the future.';
    }

    return 'Invalid value.';
  }

  private getFirstInvalidControlName(): string | null {
    for (const controlName of Object.keys(this.form.controls)) {
      const control = this.form.get(controlName);

      if (control?.invalid) {
        return controlName;
      }
    }

    return null;
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
    return;
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

  private getFrontendValidationMessage(controlName: string): string {
    const control = this.form.get(controlName);

    if (!control || !control.errors) {
      return 'Please check the entered data.';
    }

    const fieldNames: Record<string, string> = {
      name: 'First name',
      surname: 'Last name',
      phoneNumber: 'Phone number',
      email: 'Email',
      dateOfBirth: 'Date of birth',
      username: 'Username',
      password: 'Password',
      genderId: 'Gender',
      cityId: 'City',
      beltId: 'Belt',
      clubId: 'Club'
    };

    const field = fieldNames[controlName] ?? 'Field';

    if (control.errors['required']) {
      return `${field} is required.`;
    }

    if (control.errors['email']) {
      return 'Email is invalid.';
    }

    if (control.errors['minlength']) {
      return `${field} is too short.`;
    }

    if (control.errors['maxlength']) {
      return `${field} is too long.`;
    }

    if (control.errors['futureDate']) {
      return 'Date of birth cannot be in the future.';
    }

    if (control.errors['pattern']) {
      if (controlName === 'phoneNumber') {
        return 'Phone number is invalid.';
      }

      if (controlName === 'username') {
        return 'Username is invalid.';
      }

      if (controlName === 'name') {
        return 'First name is invalid.';
      }

      if (controlName === 'surname') {
        return 'Last name is invalid.';
      }
    }

    return `${field} is invalid.`;
  }
}