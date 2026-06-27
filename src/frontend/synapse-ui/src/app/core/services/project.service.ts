import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { CreateProjectRequest, Project } from "../models/project.model";

@Injectable({ providedIn: 'root' })
export class ProjectService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/api/projects`;

    getAll(): Observable<Project[]> {
        return this.http.get<Project[]>(this.apiUrl);
    }

    create(project: CreateProjectRequest): Observable<void> {
        return this.http.post<void>(this.apiUrl, project);
    }
}