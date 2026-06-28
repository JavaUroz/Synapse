import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ReviewHistory {
  id: string;
  pr_url: string;
  created_at: string;
  report_preview: string;
}

export interface ReviewRequest {
  pr_url: string;
}

export interface ReviewResponse {
  pr_url: string;
  report: string;
  review_id: string;
}

@Injectable({ providedIn: 'root' })
export class AgentService {
  private readonly http = inject(HttpClient);
  private readonly agentsUrl = `${environment.agentsUrl}/api/agents`;

  getReviewHistory(): Observable<ReviewHistory[]> {
    return this.http.get<ReviewHistory[]>(`${this.agentsUrl}/code-reviewer/history`);
  }

  reviewPR(pr_url: string): Observable<ReviewResponse> {
    return this.http.post<ReviewResponse>(`${this.agentsUrl}/code-reviewer`, { pr_url });
  }
}