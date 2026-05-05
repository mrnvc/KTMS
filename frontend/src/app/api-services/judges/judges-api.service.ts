import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../enviroments/enviroment';
import { Judge } from './judge-api.model';
import { JudgeDetails } from './judge-details.model';
import { CreateJudgeRequest } from './create-judge-request.model';
import { UpdateJudgeRequest } from './update-judge-request.model';

@Injectable({
  providedIn: 'root'
})
export class JudgesApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/api`;
  private readonly endpoint = '/Judge';

  getJudges(): Observable<Judge[]> {
    return this.http.get<Judge[]>(
      `${this.apiUrl}${this.endpoint}/GetJudges`
    );
  }

  getJudge(id: number): Observable<JudgeDetails> {
    return this.http.get<JudgeDetails>(
      `${this.apiUrl}${this.endpoint}/GetJudgesById/${id}`
    );
  }

  createJudge(judge: CreateJudgeRequest): Observable<number> {
    return this.http.post<number>(
      `${this.apiUrl}${this.endpoint}/CreateJudge`,
      {
        createJudgeDto: judge
      }
    );
  }

  updateJudge(id: number, judge: UpdateJudgeRequest): Observable<number> {
    return this.http.put<number>(
      `${this.apiUrl}${this.endpoint}/UpdateJudge/${id}`,
      {
        updateJudgeDto: judge
      }
    );
  }

  deleteJudge(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}${this.endpoint}/DeleteJudge/${id}`
    );
  }
}