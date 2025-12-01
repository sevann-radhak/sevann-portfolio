using FluentAssertions;
using Portfolio.Domain.Constants;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Tests.Entities;

public class ExperienceTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateExperience()
    {
        Guid id = Guid.NewGuid();
        string company = "Test Company";
        string position = "Software Engineer";
        string description = "Test Description";
        DateTime startDate = new(2020, 1, 1);

        Experience experience = new(id, company, position, description, startDate);

        _ = experience.Should().NotBeNull();
        _ = experience.Id.Should().Be(id);
        _ = experience.Company.Should().Be(company);
        _ = experience.Position.Should().Be(position);
        _ = experience.Description.Should().Be(description);
        _ = experience.StartDate.Should().Be(startDate);
        _ = experience.EndDate.Should().BeNull();
        _ = experience.IsCurrent.Should().BeTrue();
        _ = experience.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        _ = experience.UpdatedAt.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithEndDate_ShouldSetEndDateAndIsCurrentFalse()
    {
        DateTime startDate = new(2020, 1, 1);
        DateTime endDate = new(2022, 12, 31);

        Experience experience = new(
            Guid.NewGuid(),
            "Test Company",
            "Software Engineer",
            "Description",
            startDate,
            endDate);

        _ = experience.EndDate.Should().Be(endDate);
        _ = experience.IsCurrent.Should().BeFalse();
    }

    [Fact]
    public void Constructor_WithEmptyId_ShouldThrowArgumentException()
    {
        Func<Experience> action = () => new Experience(
            Guid.Empty,
            "Test Company",
            "Software Engineer",
            "Description",
            new DateTime(2020, 1, 1));

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ExperienceId}*");
    }

    [Fact]
    public void Constructor_WithNullCompany_ShouldThrowArgumentException()
    {
        Func<Experience> action = () => new Experience(
            Guid.NewGuid(),
            null!,
            "Software Engineer",
            "Description",
            new DateTime(2020, 1, 1));

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Company}*");
    }

    [Fact]
    public void Constructor_WithNullPosition_ShouldThrowArgumentException()
    {
        Func<Experience> action = () => new Experience(
            Guid.NewGuid(),
            "Test Company",
            null!,
            "Description",
            new DateTime(2020, 1, 1));

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Position}*");
    }

    [Fact]
    public void Constructor_WithEndDateBeforeStartDate_ShouldThrowArgumentException()
    {
        DateTime startDate = new(2022, 1, 1);
        DateTime endDate = new(2020, 1, 1);

        Func<Experience> action = () => new Experience(
            Guid.NewGuid(),
            "Test Company",
            "Software Engineer",
            "Description",
            startDate,
            endDate);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{ErrorMessages.EndDateBeforeStartDate}*");
    }

    [Fact]
    public void Constructor_WithNullDescription_ShouldSetEmptyString()
    {
        Experience experience = new(
            Guid.NewGuid(),
            "Test Company",
            "Software Engineer",
            null!,
            new DateTime(2020, 1, 1));

        _ = experience.Description.Should().BeEmpty();
    }

    [Fact]
    public void UpdateCompany_WithValidCompany_ShouldUpdateCompany()
    {
        Experience experience = CreateValidExperience("Old Company");

        experience.UpdateCompany("New Company");

        _ = experience.Company.Should().Be("New Company");
        _ = experience.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateCompany_WithNullCompany_ShouldThrowArgumentException()
    {
        Experience experience = CreateValidExperience();

        Action action = () => experience.UpdateCompany(null!);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Company}*");
    }

    [Fact]
    public void UpdatePosition_WithValidPosition_ShouldUpdatePosition()
    {
        Experience experience = CreateValidExperience(position: "Old Position");

        experience.UpdatePosition("New Position");

        _ = experience.Position.Should().Be("New Position");
        _ = experience.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdatePosition_WithNullPosition_ShouldThrowArgumentException()
    {
        Experience experience = CreateValidExperience();

        Action action = () => experience.UpdatePosition(null!);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Position}*");
    }

    [Fact]
    public void UpdateDescription_ShouldUpdateDescription()
    {
        Experience experience = CreateValidExperience(description: "Old Description");

        experience.UpdateDescription("New Description");

        _ = experience.Description.Should().Be("New Description");
        _ = experience.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void EndExperience_WithValidEndDate_ShouldSetEndDateAndIsCurrentFalse()
    {
        DateTime startDate = new(2020, 1, 1);
        Experience experience = CreateValidExperience(startDate: startDate);

        DateTime endDate = new(2022, 12, 31);
        experience.EndExperience(endDate);

        _ = experience.EndDate.Should().Be(endDate);
        _ = experience.IsCurrent.Should().BeFalse();
        _ = experience.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void EndExperience_WithEndDateBeforeStartDate_ShouldThrowArgumentException()
    {
        DateTime startDate = new(2022, 1, 1);
        Experience experience = CreateValidExperience(startDate: startDate);

        DateTime endDate = new(2020, 1, 1);
        Action action = () => experience.EndExperience(endDate);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{ErrorMessages.EndDateBeforeStartDate}*");
    }

    [Fact]
    public void MarkAsCurrent_ShouldSetEndDateToNullAndIsCurrentTrue()
    {
        DateTime startDate = new(2020, 1, 1);
        DateTime endDate = new(2022, 12, 31);
        Experience experience = CreateValidExperience(startDate: startDate, endDate: endDate);

        experience.MarkAsCurrent();

        _ = experience.EndDate.Should().BeNull();
        _ = experience.IsCurrent.Should().BeTrue();
        _ = experience.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    private static Experience CreateValidExperience(
        string company = "Test Company",
        string position = "Software Engineer",
        string description = "Description",
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        return new Experience(
            Guid.NewGuid(),
            company,
            position,
            description,
            startDate ?? new DateTime(2020, 1, 1),
            endDate);
    }
}

