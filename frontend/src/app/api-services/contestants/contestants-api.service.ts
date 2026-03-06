import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Contestant } from './contestant-api.model';
import { ContestantApiResponse } from './contestant-api-response.model';
import { environment } from '../../../enviroments/enviroment';

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
      // Parse "FirstName LastName, Contestant" format
      const userParts = item.user.replace(', Contestant', '').trim().split(' ');
      const firstName = userParts[0] || '';
      const lastName = userParts.slice(1).join(' ') || '';

      return {
        id: index + 1, // Use index as ID since API doesn't provide one
        firstName,
        lastName,
        belt: item.belt,
        club: item.club,
        category: '' // Default empty category since API doesn't provide one
      };
    });
  }

  getContestant(id: number): Observable<Contestant> {
    return this.http.get<Contestant>(`${this.apiUrl}${this.endpoint}/GetContestantsById/${id}`);
  }

  createContestant(contestant: Partial<Contestant>): Observable<Contestant> {
    return this.http.post<Contestant>(
      `${this.apiUrl}${this.endpoint}/CreateContestants`,
      contestant
    );
  }

  updateContestant(id: number, contestant: Partial<Contestant>): Observable<Contestant> {
    return this.http.put<Contestant>(
      `${this.apiUrl}${this.endpoint}/UpdateContestants?id=${id}`,
      contestant
    );
  }

  deleteContestant(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}${this.endpoint}/DeleteContestants?id=${id}`
    );
  }
}
