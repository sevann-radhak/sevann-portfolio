using Portfolio.Domain.Constants;
using System.Text.RegularExpressions;

namespace Portfolio.Domain.ValueObjects;

public class EmailAddress : IEquatable<EmailAddress>
{
    public string Value { get; private set; } = string.Empty;
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private EmailAddress() { }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public static EmailAddress Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(string.Format(ErrorMessages.CannotBeNullOrEmpty, FieldNames.EmailAddress), nameof(value));
        }

        return !EmailRegex.IsMatch(value)
            ? throw new ArgumentException(string.Format(ErrorMessages.InvalidFormat, FieldNames.EmailAddress), nameof(value))
            : new EmailAddress(value.ToLowerInvariant());
    }

    public bool Equals(EmailAddress? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as EmailAddress);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(EmailAddress? left, EmailAddress? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(EmailAddress? left, EmailAddress? right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return Value;
    }
}

