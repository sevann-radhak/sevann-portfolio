using System.Text.RegularExpressions;
using Portfolio.Domain.Constants;

namespace Portfolio.Domain.ValueObjects;

public class Url : IEquatable<Url>
{
    public string Value { get; private set; } = string.Empty;
    private static readonly Regex UrlRegex = new(
        @"^https?://(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Url() { }

    private Url(string value)
    {
        Value = value;
    }

    public static Url Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.Url), nameof(value));

        if (!UrlRegex.IsMatch(value))
            throw new ArgumentException(string.Format(ErrorMessages.InvalidFormat, FieldNames.Url), nameof(value));

        return new Url(value);
    }

    public bool Equals(Url? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Url);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Url? left, Url? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Url? left, Url? right)
    {
        return !(left == right);
    }

    public override string ToString() => Value;
}

