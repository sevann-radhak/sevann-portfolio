using FluentAssertions;
using Portfolio.Domain.Constants;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Tests.ValueObjects;

public class EmailAddressTests
{
    [Fact]
    public void Create_WithValidEmail_ShouldCreateEmailAddress()
    {
        string value = "test@example.com";
        EmailAddress email = EmailAddress.Create(value);

        _ = email.Should().NotBeNull();
        _ = email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void Create_WithUppercaseEmail_ShouldConvertToLowercase()
    {
        string value = "TEST@EXAMPLE.COM";
        EmailAddress email = EmailAddress.Create(value);

        _ = email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void Create_WithMixedCaseEmail_ShouldConvertToLowercase()
    {
        string value = "Test@Example.Com";
        EmailAddress email = EmailAddress.Create(value);

        _ = email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void Create_WithNullValue_ShouldThrowArgumentException()
    {
        Func<EmailAddress> action = () => EmailAddress.Create(null!);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.EmailAddress}*");
    }

    [Fact]
    public void Create_WithEmptyValue_ShouldThrowArgumentException()
    {
        Func<EmailAddress> action = () => EmailAddress.Create(string.Empty);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.EmailAddress}*");
    }

    [Fact]
    public void Create_WithWhitespaceValue_ShouldThrowArgumentException()
    {
        Func<EmailAddress> action = () => EmailAddress.Create("   ");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.EmailAddress}*");
    }

    [Fact]
    public void Create_WithInvalidEmail_ShouldThrowArgumentException()
    {
        Func<EmailAddress> action = () => EmailAddress.Create("invalid-email");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.EmailAddress}*");
    }

    [Fact]
    public void Create_WithEmailWithoutAt_ShouldThrowArgumentException()
    {
        Func<EmailAddress> action = () => EmailAddress.Create("invalidemail.com");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.EmailAddress}*");
    }

    [Fact]
    public void Create_WithEmailWithoutDomain_ShouldThrowArgumentException()
    {
        Func<EmailAddress> action = () => EmailAddress.Create("test@");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.EmailAddress}*");
    }

    [Fact]
    public void Create_WithEmailWithoutTld_ShouldThrowArgumentException()
    {
        Func<EmailAddress> action = () => EmailAddress.Create("test@example");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.EmailAddress}*");
    }

    [Fact]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        string value = "test@example.com";
        EmailAddress email1 = EmailAddress.Create(value);
        EmailAddress email2 = EmailAddress.Create(value);

        bool result = email1.Equals(email2);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentCase_ShouldReturnTrue()
    {
        EmailAddress email1 = EmailAddress.Create("TEST@EXAMPLE.COM");
        EmailAddress email2 = EmailAddress.Create("test@example.com");

        bool result = email1.Equals(email2);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        EmailAddress email1 = EmailAddress.Create("test1@example.com");
        EmailAddress email2 = EmailAddress.Create("test2@example.com");

        bool result = email1.Equals(email2);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        EmailAddress email = EmailAddress.Create("test@example.com");

        bool result = email.Equals(null);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        EmailAddress email = EmailAddress.Create("test@example.com");

        bool result = email.Equals(email);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_WithSameValue_ShouldReturnSameHashCode()
    {
        string value = "test@example.com";
        EmailAddress email1 = EmailAddress.Create(value);
        EmailAddress email2 = EmailAddress.Create(value);

        int hashCode1 = email1.GetHashCode();
        int hashCode2 = email2.GetHashCode();

        _ = hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void EqualityOperator_WithSameValue_ShouldReturnTrue()
    {
        string value = "test@example.com";
        EmailAddress email1 = EmailAddress.Create(value);
        EmailAddress email2 = EmailAddress.Create(value);

        bool result = email1 == email2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WithDifferentValue_ShouldReturnFalse()
    {
        EmailAddress email1 = EmailAddress.Create("test1@example.com");
        EmailAddress email2 = EmailAddress.Create("test2@example.com");

        bool result = email1 == email2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void EqualityOperator_WithBothNull_ShouldReturnTrue()
    {
        EmailAddress? email1 = null;
        EmailAddress? email2 = null;

        bool result = email1 == email2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WithOneNull_ShouldReturnFalse()
    {
        EmailAddress email1 = EmailAddress.Create("test@example.com");
        EmailAddress? email2 = null;

        bool result = email1 == email2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WithSameValue_ShouldReturnFalse()
    {
        string value = "test@example.com";
        EmailAddress email1 = EmailAddress.Create(value);
        EmailAddress email2 = EmailAddress.Create(value);

        bool result = email1 != email2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WithDifferentValue_ShouldReturnTrue()
    {
        EmailAddress email1 = EmailAddress.Create("test1@example.com");
        EmailAddress email2 = EmailAddress.Create("test2@example.com");

        bool result = email1 != email2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        string value = "test@example.com";
        EmailAddress email = EmailAddress.Create(value);

        string result = email.ToString();

        _ = result.Should().Be("test@example.com");
    }
}

