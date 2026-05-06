import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
  FormControl
} from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { JudgesApiService } from '../../../api-services/judges/judges-api.service';
import { JudgeDetails } from '../../../api-services/judges/judge-details.model';
import { UpdateJudgeRequest } from '../../../api-services/judges/update-judge-request.model';
import { ToasterService } from '../../../core/services/toaster.service';

interface Option {
  id: number;
  name: string;
}

interface EditJudgeDialogData {
  judge: JudgeDetails;
  genders: Option[];
  cities: Option[];
  firstNames: string[];
  lastNames: string[];
  ranks: string[];
}

@Component({
  selector: 'app-edit-judge-form',
  standalone: false,
  templateUrl: './edit-judge-form.component.html',
  styleUrl: './edit-judge-form.component.scss'
})
export class EditJudgeFormComponent implements OnInit {
  private readonly dialogRef = inject(MatDialogRef<EditJudgeFormComponent>);
  private readonly fb = inject(FormBuilder);
  private readonly judgesApi = inject(JudgesApiService);
  private readonly toaster = inject(ToasterService);
  private readonly dialogData = inject<EditJudgeDialogData>(MAT_DIALOG_DATA);

  form!: FormGroup;
  isLoading = false;
  today = new Date().toISOString().split('T')[0];

  judge = this.dialogData.judge;

  genders: Option[] = this.dialogData.genders;
  cities: Option[] = this.dialogData.cities;

  firstNames: string[] = this.dialogData.firstNames ?? [];
  firstNameSearchControl = new FormControl<string>('');
  filteredFirstNames: string[] = [];

  lastNames: string[] = this.dialogData.lastNames ?? [];
  lastNameSearchControl = new FormControl<string>('');
  filteredLastNames: string[] = [];

  ranks: string[] = this.dialogData.ranks ?? [];
  rankSearchControl = new FormControl<string>('');
  filteredRanks: string[] = [];

  ngOnInit(): void {
    this.initForm();
    this.patchForm();
    this.initFirstNameAutocomplete();
    this.initLastNameAutocomplete();
    this.initRankAutocomplete();
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
      genderId: [null, Validators.required],
      cityId: [null, Validators.required],
      license: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(50)
        ]
      ],
      rank: [
        '',
        [
          Validators.maxLength(50)
        ]
      ]
    });
  }

  private patchForm(): void {
    this.form.patchValue({
      name: this.judge.name,
      surname: this.judge.surname,
      phoneNumber: this.judge.phoneNumber,
      email: this.judge.email,
      dateOfBirth: this.judge.dateOfBirth?.split('T')[0],
      username: this.judge.username,
      cityId: this.judge.cityId,
      genderId: this.judge.genderId,
      license: this.judge.license?.toUpperCase(),
      rank: this.judge.rank ?? ''
    });

    this.firstNameSearchControl.setValue(this.judge.name, { emitEvent: false });
    this.lastNameSearchControl.setValue(this.judge.surname, { emitEvent: false });
    this.rankSearchControl.setValue(this.judge.rank ?? '', { emitEvent: false });
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

    const command: UpdateJudgeRequest = {
      name: value.name.trim(),
      surname: value.surname.trim(),
      phoneNumber: value.phoneNumber.trim(),
      email: value.email.trim(),
      dateOfBirth: value.dateOfBirth,
      username: value.username.trim(),
      cityId: value.cityId,
      genderId: value.genderId,
      license: value.license.trim()?.toUpperCase(),
      rank: value.rank?.trim() || null
    };

    this.isLoading = true;

    this.judgesApi.updateJudge(this.judge.id, command)
      .pipe(
        finalize(() => {
          setTimeout(() => {
            this.isLoading = false;
          });
        })
      )
      .subscribe({
        next: () => {
          this.toaster.success('Judge updated successfully.');
          this.dialogRef.close(true);
        },
        error: err => {
          console.error('Update judge error:', err);

          const message =
            err?.error?.message ||
            err?.error?.title ||
            err?.error?.detail ||
            err?.error ||
            'Error while updating judge.';

          this.toaster.error(message);
          this.applyBackendErrorToField(message);
        }
      });
  }

  private initFirstNameAutocomplete(): void {
    this.filteredFirstNames = this.firstNames;

    this.firstNameSearchControl.valueChanges.subscribe(value => {
      const searchValue = (value ?? '').toLowerCase();

      this.filteredFirstNames = this.firstNames.filter(firstName =>
        firstName.toLowerCase().includes(searchValue)
      );

      this.form.patchValue({
        name: value ?? ''
      });
    });
  }

  private initLastNameAutocomplete(): void {
    this.filteredLastNames = this.lastNames;

    this.lastNameSearchControl.valueChanges.subscribe(value => {
      const searchValue = (value ?? '').toLowerCase();

      this.filteredLastNames = this.lastNames.filter(lastName =>
        lastName.toLowerCase().includes(searchValue)
      );

      this.form.patchValue({
        surname: value ?? ''
      });
    });
  }

  private initRankAutocomplete(): void {
    this.filteredRanks = this.ranks;

    this.rankSearchControl.valueChanges.subscribe(value => {
      const searchValue = (value ?? '').toLowerCase();

      this.filteredRanks = this.ranks.filter(rank =>
        rank.toLowerCase().includes(searchValue)
      );

      this.form.patchValue({
        rank: value ?? ''
      });
    });
  }

  onFirstNameSelected(firstName: string): void {
    this.form.patchValue({
      name: firstName
    });

    this.firstNameSearchControl.setValue(firstName, { emitEvent: false });
  }

  onLastNameSelected(lastName: string): void {
    this.form.patchValue({
      surname: lastName
    });

    this.lastNameSearchControl.setValue(lastName, { emitEvent: false });
  }

  onRankSelected(rank: string): void {
    this.form.patchValue({
      rank: rank
    });

    this.rankSearchControl.setValue(rank, { emitEvent: false });
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

    if (lowerMessage.includes('license')) {
      this.setBackendError('license', message);
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
      genderId: 'Gender',
      cityId: 'City',
      license: 'License',
      rank: 'Rank'
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