import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {CityDto} from '../api-services/cities/city-api.model';
import {GenderDto} from '../api-services/genders/gender-api.model';
import {RoleDto} from '../api-services/roles/role-api.model';

@Injectable({ providedIn: 'root' })
export class LookupService {
  private apiUrl = 'https://localhost:7187/api';

  constructor(private http: HttpClient) {}

  getRoles(): Observable<RoleDto[]> {
    return this.http.get<RoleDto[]>(`${this.apiUrl}/Role/GetRoles`);
  }

  getCities(): Observable<CityDto[]> {
    return this.http.get<CityDto[]>(`${this.apiUrl}/City/GetCities`);
  }

  getGenders(): Observable<GenderDto[]> {
    return this.http.get<GenderDto[]>(`${this.apiUrl}/Gender/GetGenders`);
  }
}
