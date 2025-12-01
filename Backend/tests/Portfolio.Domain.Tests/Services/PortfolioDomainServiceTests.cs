using FluentAssertions;
using Portfolio.Domain.Aggregates;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Services;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Tests.Services;

public class PortfolioDomainServiceTests
{
    [Fact]
    public void CanFeatureProject_WithActiveProject_ShouldReturnTrue()
    {
        PortfolioProjectAggregate aggregate = CreateActiveProjectAggregate();

        bool result = PortfolioDomainService.CanFeatureProject(aggregate);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void CanFeatureProject_WithArchivedProject_ShouldReturnFalse()
    {
        PortfolioProjectAggregate aggregate = CreateActiveProjectAggregate();
        aggregate.Archive();

        bool result = PortfolioDomainService.CanFeatureProject(aggregate);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void CanFeatureProject_WithInProgressProject_ShouldReturnFalse()
    {
        PortfolioProjectAggregate aggregate = CreateActiveProjectAggregate();
        aggregate.Project.Archive();
        aggregate.Project.Activate();

        bool result = PortfolioDomainService.CanFeatureProject(aggregate);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void CanFeatureProject_WithNullProject_ShouldThrowArgumentNullException()
    {
        Func<bool> action = () => PortfolioDomainService.CanFeatureProject(null!);

        _ = action.Should().Throw<ArgumentNullException>()
            .WithParameterName("project");
    }

    [Fact]
    public void CanArchiveProject_WithActiveProject_ShouldReturnTrue()
    {
        PortfolioProjectAggregate aggregate = CreateActiveProjectAggregate();

        bool result = PortfolioDomainService.CanArchiveProject(aggregate);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void CanArchiveProject_WithArchivedProject_ShouldReturnFalse()
    {
        PortfolioProjectAggregate aggregate = CreateActiveProjectAggregate();
        aggregate.Archive();

        bool result = PortfolioDomainService.CanArchiveProject(aggregate);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void CanArchiveProject_WithNullProject_ShouldThrowArgumentNullException()
    {
        Func<bool> action = () => PortfolioDomainService.CanArchiveProject(null!);

        _ = action.Should().Throw<ArgumentNullException>()
            .WithParameterName("project");
    }

    [Fact]
    public void CalculateTotalYearsOfExperience_WithNoExperiences_ShouldReturnZero()
    {
        IEnumerable<Experience> experiences = [];

        int result = PortfolioDomainService.CalculateTotalYearsOfExperience(experiences);

        _ = result.Should().Be(0);
    }

    [Fact]
    public void CalculateTotalYearsOfExperience_WithSingleExperience_ShouldReturnCorrectYears()
    {
        DateTime startDate = new(2020, 1, 1);
        DateTime endDate = new(2022, 12, 31);
        List<Experience> experiences =
        [
            new(Guid.NewGuid(), "Company", "Position", "Description", startDate, endDate)
        ];

        int result = PortfolioDomainService.CalculateTotalYearsOfExperience(experiences);

        _ = result.Should().Be(2);
    }

    [Fact]
    public void CalculateTotalYearsOfExperience_WithMultipleExperiences_ShouldReturnTotalYears()
    {
        List<Experience> experiences =
        [
            new(Guid.NewGuid(), "Company1", "Position1", "Description1", new DateTime(2020, 1, 1), new DateTime(2021, 12, 31)),
            new(Guid.NewGuid(), "Company2", "Position2", "Description2", new DateTime(2022, 1, 1), new DateTime(2023, 6, 30))
        ];

        int result = PortfolioDomainService.CalculateTotalYearsOfExperience(experiences);

        _ = result.Should().Be(3);
    }

    [Fact]
    public void CalculateTotalYearsOfExperience_WithCurrentExperience_ShouldUseCurrentDate()
    {
        DateTime startDate = DateTime.UtcNow.AddYears(-2);
        List<Experience> experiences =
        [
            new(Guid.NewGuid(), "Company", "Position", "Description", startDate)
        ];

        int result = PortfolioDomainService.CalculateTotalYearsOfExperience(experiences);

        _ = result.Should().Be(2);
    }

    [Fact]
    public void CalculateTotalYearsOfExperience_WithNullExperiences_ShouldThrowArgumentNullException()
    {
        Func<int> action = () => PortfolioDomainService.CalculateTotalYearsOfExperience(null!);

        _ = action.Should().Throw<ArgumentNullException>()
            .WithParameterName("experiences");
    }

    [Fact]
    public void CountActiveSkills_WithActiveSkills_ShouldReturnCount()
    {
        List<Skill> skills =
        [
            new(Guid.NewGuid(), "C#", SkillCategory.ProgrammingLanguage, SkillLevel.Advanced, 5),
            new(Guid.NewGuid(), "React", SkillCategory.Framework, SkillLevel.Intermediate, 3)
        ];

        int result = PortfolioDomainService.CountActiveSkills(skills);

        _ = result.Should().Be(2);
    }

    [Fact]
    public void CountActiveSkills_WithMixedActiveInactive_ShouldReturnOnlyActiveCount()
    {
        Skill skill1 = new(Guid.NewGuid(), "C#", SkillCategory.ProgrammingLanguage, SkillLevel.Advanced, 5);
        Skill skill2 = new(Guid.NewGuid(), "React", SkillCategory.Framework, SkillLevel.Intermediate, 3);
        skill2.Deactivate();

        List<Skill> skills = [skill1, skill2];

        int result = PortfolioDomainService.CountActiveSkills(skills);

        _ = result.Should().Be(1);
    }

    [Fact]
    public void CountActiveSkills_WithNoActiveSkills_ShouldReturnZero()
    {
        Skill skill1 = new(Guid.NewGuid(), "C#", SkillCategory.ProgrammingLanguage, SkillLevel.Advanced, 5);
        Skill skill2 = new(Guid.NewGuid(), "React", SkillCategory.Framework, SkillLevel.Intermediate, 3);
        skill1.Deactivate();
        skill2.Deactivate();

        List<Skill> skills = [skill1, skill2];

        int result = PortfolioDomainService.CountActiveSkills(skills);

        _ = result.Should().Be(0);
    }

    [Fact]
    public void CountActiveSkills_WithNullSkills_ShouldThrowArgumentNullException()
    {
        Func<int> action = () => PortfolioDomainService.CountActiveSkills(null!);

        _ = action.Should().Throw<ArgumentNullException>()
            .WithParameterName("skills");
    }

    [Fact]
    public void CountFeaturedProjects_WithFeaturedProjects_ShouldReturnCount()
    {
        PortfolioProject project1 = new(Guid.NewGuid(), "Project1", "Description1", ProjectType.Personal);
        project1.MarkAsFeatured();
        PortfolioProject project2 = new(Guid.NewGuid(), "Project2", "Description2", ProjectType.Personal);
        project2.MarkAsFeatured();

        List<PortfolioProject> projects = [project1, project2];

        int result = PortfolioDomainService.CountFeaturedProjects(projects);

        _ = result.Should().Be(2);
    }

    [Fact]
    public void CountFeaturedProjects_WithMixedFeaturedNonFeatured_ShouldReturnOnlyFeaturedCount()
    {
        PortfolioProject project1 = new(Guid.NewGuid(), "Project1", "Description1", ProjectType.Personal);
        project1.MarkAsFeatured();
        PortfolioProject project2 = new(Guid.NewGuid(), "Project2", "Description2", ProjectType.Personal);

        List<PortfolioProject> projects = [project1, project2];

        int result = PortfolioDomainService.CountFeaturedProjects(projects);

        _ = result.Should().Be(1);
    }

    [Fact]
    public void CountFeaturedProjects_WithArchivedFeaturedProject_ShouldNotCountIt()
    {
        PortfolioProject project1 = new(Guid.NewGuid(), "Project1", "Description1", ProjectType.Personal);
        project1.MarkAsFeatured();
        project1.Archive();

        List<PortfolioProject> projects = [project1];

        int result = PortfolioDomainService.CountFeaturedProjects(projects);

        _ = result.Should().Be(0);
    }

    [Fact]
    public void CountFeaturedProjects_WithNoFeaturedProjects_ShouldReturnZero()
    {
        List<PortfolioProject> projects =
        [
            new(Guid.NewGuid(), "Project1", "Description1", ProjectType.Personal),
            new(Guid.NewGuid(), "Project2", "Description2", ProjectType.Personal)
        ];

        int result = PortfolioDomainService.CountFeaturedProjects(projects);

        _ = result.Should().Be(0);
    }

    [Fact]
    public void CountFeaturedProjects_WithNullProjects_ShouldThrowArgumentNullException()
    {
        Func<int> action = () => PortfolioDomainService.CountFeaturedProjects(null!);

        _ = action.Should().Throw<ArgumentNullException>()
            .WithParameterName("projects");
    }

    private static PortfolioProjectAggregate CreateActiveProjectAggregate()
    {
        return new PortfolioProjectAggregate(
            Guid.NewGuid(),
            ProjectName.Create("Test Project"),
            "Description",
            ProjectType.Personal);
    }
}

