import {inject, Injectable} from '@angular/core';
import {
  LoginCommand,
  LoginCommandDto,
  LogoutCommand,
  RefreshTokenCommand,
  RefreshTokenCommandDto
} from './auth-api.model';
import {Observable, tap} from 'rxjs';
import {environment} from '../../../enviroments/enviroment';
import {HttpClient} from '@angular/common/http';

@Injectable({ providedIn: 'root' })

export class AuthApiService {
  private readonly tokenKey = 'token';
  private readonly guestKey = 'guest_mode';
  private readonly baseUrl = `${environment.apiUrl}/api/Auth`;
  private http = inject(HttpClient);

  enterGuestMode(): void {
    sessionStorage.setItem(this.guestKey, '1');
  }

  exitGuestMode(): void {
    sessionStorage.removeItem(this.guestKey);
  }

  isGuest(): boolean {
    return sessionStorage.getItem(this.guestKey) === '1';
  }

  login(payload: LoginCommand): Observable<LoginCommandDto> {
    return this.http.post<LoginCommandDto>(`${this.baseUrl}/login`, payload);
  }

  logout(payload: LogoutCommand): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/logout`, payload);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey) || sessionStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  /**
   * POST /Auth/refresh
   * Refresh access token using refresh token.
   */
  refresh(payload: RefreshTokenCommand): Observable<RefreshTokenCommandDto> {
    return this.http.post<RefreshTokenCommandDto>(`${this.baseUrl}/refresh`, payload);
  }
}
