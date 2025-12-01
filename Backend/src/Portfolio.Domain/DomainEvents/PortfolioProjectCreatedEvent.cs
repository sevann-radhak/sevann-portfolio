namespace Portfolio.Domain.DomainEvents;

public class PortfolioProjectCreatedEvent(Guid projectId, string projectName, string? repositoryUrl = null)
{
    public Guid ProjectId { get; } = projectId;
    public string ProjectName { get; } = projectName;
    public string? RepositoryUrl { get; } = repositoryUrl;
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}

