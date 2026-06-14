namespace Synapse.Domain.Entities;

 public class Project
 {
     public Guid Id { get; set; }
     public string Name { get; set; } = string.Empty;
     public string? RepositoryUrl { get; set; }
     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 }
