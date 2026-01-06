import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Tournament } from "../models/tournament.model";

@Injectable({ providedIn: "root" })
export class TournamentsService {
  private apiUrl = 'https://localhost:7187/api';
  private readonly http = inject(HttpClient);
  private readonly endpoint = "/Tournaments/GetTournaments";

  getTournaments(): Observable<Tournament[]> {
    return this.http.get<Tournament[]>(`${this.apiUrl}${this.endpoint}`);
  }
}
