export interface Project {
    id: string;
    name: string;
    repositoryUrl?: string;
    createdAt: string;
}

export interface CreateProjectRequest {
    name: string;
    repositoryUrl?: string;
}