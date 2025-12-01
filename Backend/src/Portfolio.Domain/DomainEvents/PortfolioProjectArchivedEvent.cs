namespace Portfolio.Domain.DomainEvents;

public class PortfolioProjectArchivedEvent(Guid projectId, string projectName)
{
    public Guid ProjectId { get; } = projectId;
    public string ProjectName { get; } = projectName;
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}

