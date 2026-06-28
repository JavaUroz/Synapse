import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ProjectService } from '../../core/services/project.service'
import { Project } from '../../core/models/project.model';
import { AgentService, ReviewHistory } from '../../core/services/agent.service';


@Component({
    selector: 'app-dashboard',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
    private readonly projectService = inject(ProjectService);
    private readonly agentService = inject(AgentService);

    projects = signal<Project[]>([]);
    reviews = signal<ReviewHistory[]>([]);
    loading = signal(true);
    error = signal<string | null>(null);

    totalProjects = computed(() => this.projects().length);
    totalReviews = computed(() => this.reviews().length);

    ngOnInit(): void {
        this.projectService.getAll().subscribe({
            next: (data) => {
                this.projects.set(data);
                this.loading.set(false);
            },
            error: (err) => {
                this.error.set('Failed to load projects...');
                this.loading.set(false);
            }
        });

        this.agentService.getReviewHistory().subscribe({
            next: (data) => this.reviews.set(data),
            error: (err) => console.error('Failed to load reviews', err)
        });
    }
}