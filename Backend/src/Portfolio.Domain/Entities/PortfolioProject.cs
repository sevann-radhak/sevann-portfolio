using Portfolio.Domain.Constants;

namespace Portfolio.Domain.Entities;

public class PortfolioProject
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string? RepositoryUrl { get; private set; }
    public string? LiveUrl { get; private set; }
    public ProjectType Type { get; private set; }
    public ProjectStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsFeatured { get; private set; }

    private PortfolioProject() { }

    public PortfolioProject(Guid id, string name, string description, ProjectType type, string? repositoryUrl = null, string? liveUrl = null)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(string.Format(ErrorMessages.IdCannotBeEmpty, FieldNames.ProjectId), nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.ProjectName), nameof(name));
        }

        Id = id;
        Name = name;
        Description = description ?? string.Empty;
        Type = type;
        RepositoryUrl = repositoryUrl;
        LiveUrl = liveUrl;
        Status = ProjectStatus.Active;
        CreatedAt = DateTime.UtcNow;
        IsFeatured = false;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.ProjectName), nameof(newName));
        }

        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription ?? string.Empty;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRepositoryUrl(string? newUrl)
    {
        RepositoryUrl = newUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLiveUrl(string? newUrl)
    {
        LiveUrl = newUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFeatured()
    {
        IsFeatured = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UnmarkAsFeatured()
    {
        IsFeatured = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = ProjectStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = ProjectStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum ProjectStatus
{
    Active = 0,
    Archived = 1,
    InProgress = 2
}

public enum ProjectType
{
    Professional = 0,
    Personal = 1,
    Demo = 2,
    OpenSource = 3,
    Portfolio = 4
}

