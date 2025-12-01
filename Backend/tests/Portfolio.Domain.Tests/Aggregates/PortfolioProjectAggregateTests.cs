using FluentAssertions;
using Portfolio.Domain.Aggregates;
using Portfolio.Domain.Constants;
using Portfolio.Domain.DomainEvents;
using Portfolio.Domain.Entities;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Tests.Aggregates;

public class PortfolioProjectAggregateTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateAggregate()
    {
        Guid projectId = Guid.NewGuid();
        ProjectName name = ProjectName.Create("Test Project");
        string description = "Test Description";
        ProjectType type = ProjectType.Personal;

        PortfolioProjectAggregate aggregate = new(projectId, name, description, type);

        _ = aggregate.Should().NotBeNull();
        _ = aggregate.Project.Should().NotBeNull();
        _ = aggregate.Project.Id.Should().Be(projectId);
        _ = aggregate.Name.Should().Be(name);
        _ = aggregate.DomainEvents.Should().HaveCount(1);
        _ = aggregate.DomainEvents.First().Should().BeOfType<PortfolioProjectCreatedEvent>();
    }

    [Fact]
    public void Constructor_WithRepositoryUrl_ShouldSetRepositoryUrl()
    {
        Guid projectId = Guid.NewGuid();
        ProjectName name = ProjectName.Create("Test Project");
        Url repoUrl = Url.Create("https://github.com/user/repo");

        PortfolioProjectAggregate aggregate = new(
            projectId,
            name,
            "Description",
            ProjectType.Personal,
            repoUrl);

        _ = aggregate.RepositoryUrl.Should().Be(repoUrl);
        _ = aggregate.Project.RepositoryUrl.Should().Be(repoUrl.Value);
    }

    [Fact]
    public void Constructor_WithLiveUrl_ShouldSetLiveUrl()
    {
        Guid projectId = Guid.NewGuid();
        ProjectName name = ProjectName.Create("Test Project");
        Url liveUrl = Url.Create("https://example.com");

        PortfolioProjectAggregate aggregate = new(
            projectId,
            name,
            "Description",
            ProjectType.Personal,
            null,
            liveUrl);

        _ = aggregate.LiveUrl.Should().Be(liveUrl);
        _ = aggregate.Project.LiveUrl.Should().Be(liveUrl.Value);
    }

    [Fact]
    public void Constructor_WithEmptyProjectId_ShouldThrowArgumentException()
    {
        ProjectName name = ProjectName.Create("Test Project");

        Func<PortfolioProjectAggregate> action = () => new PortfolioProjectAggregate(
            Guid.Empty,
            name,
            "Description",
            ProjectType.Personal);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectId}*");
    }

    [Fact]
    public void Constructor_WithNullName_ShouldThrowArgumentNullException()
    {
        Func<PortfolioProjectAggregate> action = () => new PortfolioProjectAggregate(
            Guid.NewGuid(),
            null!,
            "Description",
            ProjectType.Personal);

        _ = action.Should().Throw<ArgumentNullException>()
            .WithParameterName("name");
    }

    [Fact]
    public void Constructor_ShouldRaisePortfolioProjectCreatedEvent()
    {
        Guid projectId = Guid.NewGuid();
        ProjectName name = ProjectName.Create("Test Project");
        Url repoUrl = Url.Create("https://github.com/user/repo");

        PortfolioProjectAggregate aggregate = new(
            projectId,
            name,
            "Description",
            ProjectType.Personal,
            repoUrl);

        PortfolioProjectCreatedEvent? createdEvent = aggregate.DomainEvents.First() as PortfolioProjectCreatedEvent;
        _ = createdEvent.Should().NotBeNull();
        _ = createdEvent!.ProjectId.Should().Be(projectId);
        _ = createdEvent.ProjectName.Should().Be(name.Value);
        _ = createdEvent.RepositoryUrl.Should().Be(repoUrl.Value);
        _ = createdEvent.OccurredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateName_WithValidName_ShouldUpdateName()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();
        ProjectName newName = ProjectName.Create("New Project Name");

        aggregate.UpdateName(newName);

        _ = aggregate.Name.Should().Be(newName);
        _ = aggregate.Project.Name.Should().Be(newName.Value);
    }

    [Fact]
    public void UpdateName_WithNullName_ShouldThrowArgumentNullException()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();

        Action action = () => aggregate.UpdateName(null!);

        _ = action.Should().Throw<ArgumentNullException>()
            .WithParameterName("newName");
    }

    [Fact]
    public void UpdateDescription_ShouldUpdateDescription()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();

        aggregate.UpdateDescription("New Description");

        _ = aggregate.Project.Description.Should().Be("New Description");
    }

    [Fact]
    public void UpdateRepositoryUrl_WithValidUrl_ShouldUpdateRepositoryUrl()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();
        Url newUrl = Url.Create("https://github.com/new/repo");

        aggregate.UpdateRepositoryUrl(newUrl);

        _ = aggregate.RepositoryUrl.Should().Be(newUrl);
        _ = aggregate.Project.RepositoryUrl.Should().Be(newUrl.Value);
    }

    [Fact]
    public void UpdateRepositoryUrl_WithNull_ShouldSetNull()
    {
        Url repoUrl = Url.Create("https://github.com/old/repo");
        PortfolioProjectAggregate aggregate = new(
            Guid.NewGuid(),
            ProjectName.Create("Test Project"),
            "Description",
            ProjectType.Personal,
            repoUrl);

        aggregate.UpdateRepositoryUrl(null);

        _ = aggregate.RepositoryUrl.Should().BeNull();
        _ = aggregate.Project.RepositoryUrl.Should().BeNull();
    }

    [Fact]
    public void UpdateLiveUrl_WithValidUrl_ShouldUpdateLiveUrl()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();
        Url newUrl = Url.Create("https://new-example.com");

        aggregate.UpdateLiveUrl(newUrl);

        _ = aggregate.LiveUrl.Should().Be(newUrl);
        _ = aggregate.Project.LiveUrl.Should().Be(newUrl.Value);
    }

    [Fact]
    public void UpdateLiveUrl_WithNull_ShouldSetNull()
    {
        Url liveUrl = Url.Create("https://old-example.com");
        PortfolioProjectAggregate aggregate = new(
            Guid.NewGuid(),
            ProjectName.Create("Test Project"),
            "Description",
            ProjectType.Personal,
            null,
            liveUrl);

        aggregate.UpdateLiveUrl(null);

        _ = aggregate.LiveUrl.Should().BeNull();
        _ = aggregate.Project.LiveUrl.Should().BeNull();
    }

    [Fact]
    public void MarkAsFeatured_ShouldMarkProjectAsFeaturedAndRaiseEvent()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();
        int initialEventCount = aggregate.DomainEvents.Count;

        aggregate.MarkAsFeatured();

        _ = aggregate.Project.IsFeatured.Should().BeTrue();
        _ = aggregate.DomainEvents.Should().HaveCount(initialEventCount + 1);
        _ = aggregate.DomainEvents.Last().Should().BeOfType<PortfolioProjectFeaturedEvent>();

        PortfolioProjectFeaturedEvent? featuredEvent = aggregate.DomainEvents.Last() as PortfolioProjectFeaturedEvent;
        _ = featuredEvent!.ProjectId.Should().Be(aggregate.Project.Id);
        _ = featuredEvent.ProjectName.Should().Be(aggregate.Name.Value);
    }

    [Fact]
    public void Archive_ShouldArchiveProjectAndRaiseEvent()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();
        int initialEventCount = aggregate.DomainEvents.Count;

        aggregate.Archive();

        _ = aggregate.Project.Status.Should().Be(ProjectStatus.Archived);
        _ = aggregate.DomainEvents.Should().HaveCount(initialEventCount + 1);
        _ = aggregate.DomainEvents.Last().Should().BeOfType<PortfolioProjectArchivedEvent>();

        PortfolioProjectArchivedEvent? archivedEvent = aggregate.DomainEvents.Last() as PortfolioProjectArchivedEvent;
        _ = archivedEvent!.ProjectId.Should().Be(aggregate.Project.Id);
        _ = archivedEvent.ProjectName.Should().Be(aggregate.Name.Value);
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllEvents()
    {
        PortfolioProjectAggregate aggregate = CreateValidAggregate();
        aggregate.MarkAsFeatured();
        aggregate.Archive();

        aggregate.ClearDomainEvents();

        _ = aggregate.DomainEvents.Should().BeEmpty();
    }

    private static PortfolioProjectAggregate CreateValidAggregate()
    {
        return new PortfolioProjectAggregate(
            Guid.NewGuid(),
            ProjectName.Create("Test Project"),
            "Description",
            ProjectType.Personal);
    }
}

