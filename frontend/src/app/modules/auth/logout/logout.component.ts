import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-logout',
  standalone: false,
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.scss'
})
export class LogoutComponent {
  private readonly router = inject(Router);
  private readonly dialogRef = inject(MatDialogRef<LogoutComponent>);

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('user');
    localStorage.removeItem('authToken');

    this.dialogRef.close();
    this.router.navigate(['/']);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}