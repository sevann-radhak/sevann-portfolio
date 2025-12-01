using FluentAssertions;
using Portfolio.Domain.Constants;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Tests.Entities;

public class PortfolioProjectTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateProject()
    {
        Guid id = Guid.NewGuid();
        string name = "Test Project";
        string description = "Test Description";
        ProjectType type = ProjectType.Personal;

        PortfolioProject project = new(id, name, description, type);

        _ = project.Should().NotBeNull();
        _ = project.Id.Should().Be(id);
        _ = project.Name.Should().Be(name);
        _ = project.Description.Should().Be(description);
        _ = project.Type.Should().Be(type);
        _ = project.Status.Should().Be(ProjectStatus.Active);
        _ = project.IsFeatured.Should().BeFalse();
        _ = project.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        _ = project.UpdatedAt.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithEmptyId_ShouldThrowArgumentException()
    {
        Func<PortfolioProject> action = () => new PortfolioProject(
            Guid.Empty,
            "Test Project",
            "Description",
            ProjectType.Personal);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectId}*");
    }

    [Fact]
    public void Constructor_WithNullName_ShouldThrowArgumentException()
    {
        Func<PortfolioProject> action = () => new PortfolioProject(
            Guid.NewGuid(),
            null!,
            "Description",
            ProjectType.Personal);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        Func<PortfolioProject> action = () => new PortfolioProject(
            Guid.NewGuid(),
            string.Empty,
            "Description",
            ProjectType.Personal);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Constructor_WithWhitespaceName_ShouldThrowArgumentException()
    {
        Func<PortfolioProject> action = () => new PortfolioProject(
            Guid.NewGuid(),
            "   ",
            "Description",
            ProjectType.Personal);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Constructor_WithNullDescription_ShouldSetEmptyString()
    {
        PortfolioProject project = new(
            Guid.NewGuid(),
            "Test Project",
            null!,
            ProjectType.Personal);

        _ = project.Description.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_WithRepositoryUrl_ShouldSetRepositoryUrl()
    {
        string repoUrl = "https://github.com/user/repo";

        PortfolioProject project = new(
            Guid.NewGuid(),
            "Test Project",
            "Description",
            ProjectType.Personal,
            repoUrl);

        _ = project.RepositoryUrl.Should().Be(repoUrl);
    }

    [Fact]
    public void Constructor_WithLiveUrl_ShouldSetLiveUrl()
    {
        string liveUrl = "https://example.com";

        PortfolioProject project = new(
            Guid.NewGuid(),
            "Test Project",
            "Description",
            ProjectType.Personal,
            null,
            liveUrl);

        _ = project.LiveUrl.Should().Be(liveUrl);
    }

    [Fact]
    public void UpdateName_WithValidName_ShouldUpdateName()
    {
        PortfolioProject project = CreateValidProject("Old Name");

        project.UpdateName("New Name");

        _ = project.Name.Should().Be("New Name");
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateName_WithNullName_ShouldThrowArgumentException()
    {
        PortfolioProject project = CreateValidProject();

        Action action = () => project.UpdateName(null!);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void UpdateName_WithEmptyName_ShouldThrowArgumentException()
    {
        PortfolioProject project = CreateValidProject();

        Action action = () => project.UpdateName(string.Empty);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void UpdateDescription_WithValidDescription_ShouldUpdateDescription()
    {
        PortfolioProject project = CreateValidProject(description: "Old Description");

        project.UpdateDescription("New Description");

        _ = project.Description.Should().Be("New Description");
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateDescription_WithNullDescription_ShouldSetEmptyString()
    {
        PortfolioProject project = CreateValidProject();

        project.UpdateDescription(null!);

        _ = project.Description.Should().BeEmpty();
    }

    [Fact]
    public void UpdateRepositoryUrl_WithValidUrl_ShouldUpdateRepositoryUrl()
    {
        PortfolioProject project = CreateValidProject();

        project.UpdateRepositoryUrl("https://github.com/new/repo");

        _ = project.RepositoryUrl.Should().Be("https://github.com/new/repo");
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateRepositoryUrl_WithNull_ShouldSetNull()
    {
        PortfolioProject project = CreateValidProject(repositoryUrl: "https://github.com/old/repo");

        project.UpdateRepositoryUrl(null);

        _ = project.RepositoryUrl.Should().BeNull();
    }

    [Fact]
    public void UpdateLiveUrl_WithValidUrl_ShouldUpdateLiveUrl()
    {
        PortfolioProject project = CreateValidProject();

        project.UpdateLiveUrl("https://new-example.com");

        _ = project.LiveUrl.Should().Be("https://new-example.com");
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void MarkAsFeatured_ShouldSetIsFeaturedToTrue()
    {
        PortfolioProject project = CreateValidProject();

        project.MarkAsFeatured();

        _ = project.IsFeatured.Should().BeTrue();
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UnmarkAsFeatured_ShouldSetIsFeaturedToFalse()
    {
        PortfolioProject project = CreateValidProject();
        project.MarkAsFeatured();

        project.UnmarkAsFeatured();

        _ = project.IsFeatured.Should().BeFalse();
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Archive_ShouldSetStatusToArchived()
    {
        PortfolioProject project = CreateValidProject();

        project.Archive();

        _ = project.Status.Should().Be(ProjectStatus.Archived);
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Activate_ShouldSetStatusToActive()
    {
        PortfolioProject project = CreateValidProject();
        project.Archive();

        project.Activate();

        _ = project.Status.Should().Be(ProjectStatus.Active);
        _ = project.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    private static PortfolioProject CreateValidProject(
        string name = "Test Project",
        string description = "Description",
        ProjectType type = ProjectType.Personal,
        string? repositoryUrl = null,
        string? liveUrl = null)
    {
        return new PortfolioProject(
            Guid.NewGuid(),
            name,
            description,
            type,
            repositoryUrl,
            liveUrl);
    }
}

