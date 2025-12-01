using FluentAssertions;
using Portfolio.Domain.Constants;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Tests.ValueObjects;

public class ProjectNameTests
{
    [Fact]
    public void Create_WithValidName_ShouldCreateProjectName()
    {
        string value = "My Project";

        ProjectName projectName = ProjectName.Create(value);

        _ = projectName.Should().NotBeNull();
        _ = projectName.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithMinLength_ShouldCreateProjectName()
    {
        string value = "ABC";

        ProjectName projectName = ProjectName.Create(value);

        _ = projectName.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithMaxLength_ShouldCreateProjectName()
    {
        string value = new('A', ValidationConstants.ProjectNameMaxLength);

        ProjectName projectName = ProjectName.Create(value);

        _ = projectName.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithNullValue_ShouldThrowArgumentException()
    {
        Func<ProjectName> action = () => ProjectName.Create(null!);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Create_WithEmptyValue_ShouldThrowArgumentException()
    {
        Func<ProjectName> action = () => ProjectName.Create(string.Empty);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Create_WithWhitespaceValue_ShouldThrowArgumentException()
    {
        Func<ProjectName> action = () => ProjectName.Create("   ");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Create_WithLengthLessThanMin_ShouldThrowArgumentException()
    {
        string value = "AB";

        Func<ProjectName> action = () => ProjectName.Create(value);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Create_WithLengthGreaterThanMax_ShouldThrowArgumentException()
    {
        string value = new('A', ValidationConstants.ProjectNameMaxLength + 1);

        Func<ProjectName> action = () => ProjectName.Create(value);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.ProjectName}*");
    }

    [Fact]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        string value = "Test Project";
        ProjectName projectName1 = ProjectName.Create(value);
        ProjectName projectName2 = ProjectName.Create(value);

        bool result = projectName1.Equals(projectName2);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        ProjectName projectName1 = ProjectName.Create("Project 1");
        ProjectName projectName2 = ProjectName.Create("Project 2");

        bool result = projectName1.Equals(projectName2);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        ProjectName projectName = ProjectName.Create("Test Project");

        bool result = projectName.Equals(null);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        ProjectName projectName = ProjectName.Create("Test Project");

        bool result = projectName.Equals(projectName);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_WithSameValue_ShouldReturnSameHashCode()
    {
        string value = "Test Project";
        ProjectName projectName1 = ProjectName.Create(value);
        ProjectName projectName2 = ProjectName.Create(value);

        int hashCode1 = projectName1.GetHashCode();
        int hashCode2 = projectName2.GetHashCode();

        _ = hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void EqualityOperator_WithSameValue_ShouldReturnTrue()
    {
        string value = "Test Project";
        ProjectName projectName1 = ProjectName.Create(value);
        ProjectName projectName2 = ProjectName.Create(value);

        bool result = projectName1 == projectName2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WithDifferentValue_ShouldReturnFalse()
    {
        ProjectName projectName1 = ProjectName.Create("Project 1");
        ProjectName projectName2 = ProjectName.Create("Project 2");

        bool result = projectName1 == projectName2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void EqualityOperator_WithBothNull_ShouldReturnTrue()
    {
        ProjectName? projectName1 = null;
        ProjectName? projectName2 = null;

        bool result = projectName1 == projectName2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WithOneNull_ShouldReturnFalse()
    {
        ProjectName projectName1 = ProjectName.Create("Test Project");
        ProjectName? projectName2 = null;

        bool result = projectName1 == projectName2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WithSameValue_ShouldReturnFalse()
    {
        string value = "Test Project";
        ProjectName projectName1 = ProjectName.Create(value);
        ProjectName projectName2 = ProjectName.Create(value);

        bool result = projectName1 != projectName2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WithDifferentValue_ShouldReturnTrue()
    {
        ProjectName projectName1 = ProjectName.Create("Project 1");
        ProjectName projectName2 = ProjectName.Create("Project 2");

        bool result = projectName1 != projectName2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        string value = "Test Project";
        ProjectName projectName = ProjectName.Create(value);

        string result = projectName.ToString();

        _ = result.Should().Be(value);
    }
}

