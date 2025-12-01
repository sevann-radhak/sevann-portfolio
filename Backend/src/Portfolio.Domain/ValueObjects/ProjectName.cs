using Portfolio.Domain.Constants;

namespace Portfolio.Domain.ValueObjects;

public class ProjectName : IEquatable<ProjectName>
{
    public string Value { get; private set; } = string.Empty;

    private ProjectName() { }

    private ProjectName(string value)
    {
        Value = value;
    }

    public static ProjectName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.ProjectName), nameof(value));

        if (value.Length < ValidationConstants.ProjectNameMinLength)
            throw new ArgumentException(string.Format(ErrorMessages.MustBeAtLeastCharacters, FieldNames.ProjectName, ValidationConstants.ProjectNameMinLength), nameof(value));

        if (value.Length > ValidationConstants.ProjectNameMaxLength)
            throw new ArgumentException(string.Format(ErrorMessages.CannotExceedCharacters, FieldNames.ProjectName, ValidationConstants.ProjectNameMaxLength), nameof(value));

        return new ProjectName(value);
    }

    public bool Equals(ProjectName? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ProjectName);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(ProjectName? left, ProjectName? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(ProjectName? left, ProjectName? right)
    {
        return !(left == right);
    }

    public override string ToString() => Value;
}

