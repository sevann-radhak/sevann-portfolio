using FluentAssertions;
using Portfolio.Domain.Constants;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Tests.Entities;

public class SkillTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateSkill()
    {
        Guid id = Guid.NewGuid();
        string name = "C#";
        SkillCategory category = SkillCategory.ProgrammingLanguage;
        SkillLevel level = SkillLevel.Advanced;
        int years = 5;

        Skill skill = new(id, name, category, level, years);

        _ = skill.Should().NotBeNull();
        _ = skill.Id.Should().Be(id);
        _ = skill.Name.Should().Be(name);
        _ = skill.Category.Should().Be(category);
        _ = skill.Level.Should().Be(level);
        _ = skill.YearsOfExperience.Should().Be(years);
        _ = skill.IsActive.Should().BeTrue();
        _ = skill.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        _ = skill.UpdatedAt.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithEmptyId_ShouldThrowArgumentException()
    {
        Func<Skill> action = () => new Skill(
            Guid.Empty,
            "C#",
            SkillCategory.ProgrammingLanguage,
            SkillLevel.Advanced,
            5);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.SkillId}*");
    }

    [Fact]
    public void Constructor_WithNullName_ShouldThrowArgumentException()
    {
        Func<Skill> action = () => new Skill(
            Guid.NewGuid(),
            null!,
            SkillCategory.ProgrammingLanguage,
            SkillLevel.Advanced,
            5);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.SkillName}*");
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        Func<Skill> action = () => new Skill(
            Guid.NewGuid(),
            string.Empty,
            SkillCategory.ProgrammingLanguage,
            SkillLevel.Advanced,
            5);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.SkillName}*");
    }

    [Fact]
    public void Constructor_WithNegativeYears_ShouldThrowArgumentException()
    {
        Func<Skill> action = () => new Skill(
            Guid.NewGuid(),
            "C#",
            SkillCategory.ProgrammingLanguage,
            SkillLevel.Advanced,
            -1);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.YearsOfExperience}*");
    }

    [Fact]
    public void Constructor_WithZeroYears_ShouldCreateSkill()
    {
        Skill skill = new(
            Guid.NewGuid(),
            "C#",
            SkillCategory.ProgrammingLanguage,
            SkillLevel.Beginner,
            0);

        _ = skill.YearsOfExperience.Should().Be(0);
    }

    [Fact]
    public void UpdateName_WithValidName_ShouldUpdateName()
    {
        Skill skill = CreateValidSkill("Old Name");

        skill.UpdateName("New Name");

        _ = skill.Name.Should().Be("New Name");
        _ = skill.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateName_WithNullName_ShouldThrowArgumentException()
    {
        Skill skill = CreateValidSkill();

        Action action = () => skill.UpdateName(null!);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.SkillName}*");
    }

    [Fact]
    public void UpdateLevel_ShouldUpdateLevel()
    {
        Skill skill = CreateValidSkill(level: SkillLevel.Intermediate);

        skill.UpdateLevel(SkillLevel.Expert);

        _ = skill.Level.Should().Be(SkillLevel.Expert);
        _ = skill.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateYearsOfExperience_WithValidYears_ShouldUpdateYears()
    {
        Skill skill = CreateValidSkill();

        skill.UpdateYearsOfExperience(7);

        _ = skill.YearsOfExperience.Should().Be(7);
        _ = skill.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void UpdateYearsOfExperience_WithNegativeYears_ShouldThrowArgumentException()
    {
        Skill skill = CreateValidSkill();

        Action action = () => skill.UpdateYearsOfExperience(-1);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.YearsOfExperience}*");
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        Skill skill = CreateValidSkill();

        skill.Deactivate();

        _ = skill.IsActive.Should().BeFalse();
        _ = skill.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        Skill skill = CreateValidSkill();
        skill.Deactivate();

        skill.Activate();

        _ = skill.IsActive.Should().BeTrue();
        _ = skill.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    private static Skill CreateValidSkill(
        string name = "C#",
        SkillCategory category = SkillCategory.ProgrammingLanguage,
        SkillLevel level = SkillLevel.Advanced,
        int yearsOfExperience = 5)
    {
        return new Skill(
            Guid.NewGuid(),
            name,
            category,
            level,
            yearsOfExperience);
    }
}

