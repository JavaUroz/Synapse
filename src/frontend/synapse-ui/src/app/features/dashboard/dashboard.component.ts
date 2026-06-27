import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ProjectService } from '../../core/services/project.service'
import { Project } from '../../core/models/project.model';

@Component({
    selector: 'app-dashboard',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
    private readonly projectService = inject(ProjectService);

    projects = signal<Project[]>([]);
    loading = signal(true);
    error = signal<string | null>(null);

    totalProjects = computed(() => this.projects().length)

    ngOnInit(): void {
        this.projectService.getAll().subscribe({
            next: (data) => {
                this.projects.set(data);
                this.loading.set(true);
            },
            error: (err) => {
                this.error.set('Failed to load projects...');
                this.loading.set(false);
            }
        });
    }
}