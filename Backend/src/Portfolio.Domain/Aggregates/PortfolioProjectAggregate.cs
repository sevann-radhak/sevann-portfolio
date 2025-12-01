using Portfolio.Domain.Constants;
using Portfolio.Domain.DomainEvents;
using Portfolio.Domain.Entities;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Aggregates;

public class PortfolioProjectAggregate
{
    private readonly List<object> _domainEvents = [];

    public PortfolioProject Project { get; private set; } = null!;
    public ProjectName Name { get; private set; } = null!;
    public Url? RepositoryUrl { get; private set; }
    public Url? LiveUrl { get; private set; }
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private PortfolioProjectAggregate() { }

    public PortfolioProjectAggregate(
        Guid projectId,
        ProjectName name,
        string description,
        ProjectType type,
        Url? repositoryUrl = null,
        Url? liveUrl = null)
    {
        if (projectId == Guid.Empty)
        {
            throw new ArgumentException(string.Format(ErrorMessages.IdCannotBeEmpty, FieldNames.ProjectId), nameof(projectId));
        }

        ArgumentNullException.ThrowIfNull(name);

        Project = new PortfolioProject(projectId, name.Value, description, type, repositoryUrl?.Value, liveUrl?.Value);
        Name = name;
        RepositoryUrl = repositoryUrl;
        LiveUrl = liveUrl;

        PortfolioProjectCreatedEvent projectCreatedEvent = new(projectId, name.Value, repositoryUrl?.Value);
        _domainEvents.Add(projectCreatedEvent);
    }

    public void UpdateName(ProjectName newName)
    {
        ArgumentNullException.ThrowIfNull(newName);

        Name = newName;
        Project.UpdateName(newName.Value);
    }

    public void UpdateDescription(string newDescription)
    {
        Project.UpdateDescription(newDescription);
    }

    public void UpdateRepositoryUrl(Url? newUrl)
    {
        RepositoryUrl = newUrl;
        Project.UpdateRepositoryUrl(newUrl?.Value);
    }

    public void UpdateLiveUrl(Url? newUrl)
    {
        LiveUrl = newUrl;
        Project.UpdateLiveUrl(newUrl?.Value);
    }

    public void MarkAsFeatured()
    {
        Project.MarkAsFeatured();

        PortfolioProjectFeaturedEvent projectFeaturedEvent = new(Project.Id, Name.Value);
        _domainEvents.Add(projectFeaturedEvent);
    }

    public void Archive()
    {
        Project.Archive();

        PortfolioProjectArchivedEvent projectArchivedEvent = new(Project.Id, Name.Value);
        _domainEvents.Add(projectArchivedEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

