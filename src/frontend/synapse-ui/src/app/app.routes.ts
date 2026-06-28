import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadComponent: () =>
      import('./features/dashboard/dashboard.component')
        .then(m => m.DashboardComponent)
  },
  {
    path: 'projects',
    loadComponent: () =>
      import('./features/projects/projects.component')
        .then(m => m.ProjectsComponent)
  },
  { path: '**', redirectTo: 'dashboard' }
];