import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import {Tournament} from './tournament-api.model';
import {environment} from '../../../enviroments/enviroment';

@Injectable({ providedIn: "root" })
export class TournamentsApiService {
  private apiUrl = `${environment.apiUrl}/api`;
  private readonly http = inject(HttpClient);
  private readonly endpoint = "/Tournaments/GetTournaments";

  getTournaments(): Observable<Tournament[]> {
    return this.http.get<Tournament[]>(`${this.apiUrl}${this.endpoint}`);
  }
}
