import { Injectable } from '@angular/core';
import {HttpClient, HttpResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import {RegisterRequest} from '../models/register-request.model';

interface LoginResponse {
  success: boolean;
  token?: string;  // JWT token
  message?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7187/api/Auth';

  constructor(private http: HttpClient) {}

  private guestKey = 'guest_mode';

  enterGuestMode(): void {
    sessionStorage.setItem(this.guestKey, '1');
  }

  exitGuestMode(): void {
    sessionStorage.removeItem(this.guestKey);
  }

  isGuest(): boolean {
    return sessionStorage.getItem(this.guestKey) === '1';
  }

  login(email: string, password: string) {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, {
      user: {
        email: email,
        password: password
      }
    });
  }

  register(payload: RegisterRequest) {
    return this.http.post<void>(`${this.apiUrl}/register`, {
      user: payload
    });
  }

  saveToken(token: string, rememberMe: boolean) {
    this.exitGuestMode();

    if (rememberMe) {
      localStorage.setItem('token', token);
    } else {
      sessionStorage.setItem('token', token);
    }
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem('token');
    sessionStorage.removeItem('token');
    this.exitGuestMode();
  }

  getToken(): string | null {
    return localStorage.getItem('token') || sessionStorage.getItem('token');
  }
}
