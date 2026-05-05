import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Contestant } from './contestant-api.model';
import { ContestantApiResponse } from './contestant-api-response.model';
import { environment } from '../../../enviroments/enviroment';
import { CreateContestantRequest } from './create-contestant-request.model';
import { ContestantDetails } from './contestant-details.model';
import { UpdateContestantRequest } from './update-contestant-request.model';

@Injectable({ providedIn: "root" })
export class ContestantsApiService {
  private apiUrl = `${environment.apiUrl}/api`;
  private readonly http = inject(HttpClient);
  private readonly endpoint = "/Contestants";

  getContestants(): Observable<Contestant[]> {
    return this.http.get<ContestantApiResponse[]>(`${this.apiUrl}${this.endpoint}/GetContestants`)
      .pipe(
        map(response => this.mapApiResponseToContestants(response))
      );
  }

  private mapApiResponseToContestants(apiResponse: ContestantApiResponse[]): Contestant[] {
    return apiResponse.map((item, index) => {
      // API returns user like: "Leki Kokic, Contestant"
      // This removes everything after comma.
      const cleanUser = item.user?.split(',')[0].trim() ?? '';

      const userParts = cleanUser.split(' ');
      const firstName = userParts[0] || '';
      const lastName = userParts.slice(1).join(' ') || '';

      return {
        id: item.id ?? index + 1,
        user: cleanUser,
        firstName,
        lastName,
        belt: item.belt,
        club: item.club,
        category: ''
      };
    });
  }

  getContestant(id: number): Observable<ContestantDetails> {
    return this.http.get<ContestantDetails>(
      `${this.apiUrl}${this.endpoint}/GetContestantsById/${id}`
    );
  }

  createContestant(contestant: CreateContestantRequest): Observable<number> {
    return this.http.post<number>(
      `${this.apiUrl}${this.endpoint}/CreateContestants`,
      {
        createContestantsDto: contestant
      }
    );
  }

  updateContestant(id: number, contestant: UpdateContestantRequest): Observable<number> {
    return this.http.put<number>(
      `${this.apiUrl}${this.endpoint}/UpdateContestants/${id}`,
      {
        updateContestantsDto: contestant
      }
    );
  }

  deleteContestant(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}${this.endpoint}/DeleteContestants/${id}`
    );
  }
}
