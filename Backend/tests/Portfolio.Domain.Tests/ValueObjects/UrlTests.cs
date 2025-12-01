using FluentAssertions;
using Portfolio.Domain.Constants;
using Portfolio.Domain.ValueObjects;

namespace Portfolio.Domain.Tests.ValueObjects;

public class UrlTests
{
    [Fact]
    public void Create_WithValidHttpUrl_ShouldCreateUrl()
    {
        string value = "http://example.com";

        Url url = Url.Create(value);

        _ = url.Should().NotBeNull();
        _ = url.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithValidHttpsUrl_ShouldCreateUrl()
    {
        string value = "https://example.com";

        Url url = Url.Create(value);

        _ = url.Should().NotBeNull();
        _ = url.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithValidUrlWithPath_ShouldCreateUrl()
    {
        string value = "https://example.com/path/to/resource";

        Url url = Url.Create(value);

        _ = url.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithValidUrlWithQuery_ShouldCreateUrl()
    {
        string value = "https://example.com?param=value";

        Url url = Url.Create(value);

        _ = url.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithValidUrlWithWww_ShouldCreateUrl()
    {
        string value = "https://www.example.com";

        Url url = Url.Create(value);

        _ = url.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithNullValue_ShouldThrowArgumentException()
    {
        Func<Url> action = () => Url.Create(null!);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Url}*");
    }

    [Fact]
    public void Create_WithEmptyValue_ShouldThrowArgumentException()
    {
        Func<Url> action = () => Url.Create(string.Empty);

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Url}*");
    }

    [Fact]
    public void Create_WithWhitespaceValue_ShouldThrowArgumentException()
    {
        Func<Url> action = () => Url.Create("   ");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Url}*");
    }

    [Fact]
    public void Create_WithInvalidUrl_ShouldThrowArgumentException()
    {
        Func<Url> action = () => Url.Create("not-a-url");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Url}*");
    }

    [Fact]
    public void Create_WithUrlWithoutProtocol_ShouldThrowArgumentException()
    {
        Func<Url> action = () => Url.Create("example.com");

        _ = action.Should().Throw<ArgumentException>()
            .WithMessage($"*{FieldNames.Url}*");
    }

    [Fact]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        string value = "https://example.com";
        Url url1 = Url.Create(value);
        Url url2 = Url.Create(value);

        bool result = url1.Equals(url2);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        Url url1 = Url.Create("https://example1.com");
        Url url2 = Url.Create("https://example2.com");

        bool result = url1.Equals(url2);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        Url url = Url.Create("https://example.com");

        bool result = url.Equals(null);

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        Url url = Url.Create("https://example.com");

        bool result = url.Equals(url);

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_WithSameValue_ShouldReturnSameHashCode()
    {
        string value = "https://example.com";
        Url url1 = Url.Create(value);
        Url url2 = Url.Create(value);

        int hashCode1 = url1.GetHashCode();
        int hashCode2 = url2.GetHashCode();

        _ = hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void EqualityOperator_WithSameValue_ShouldReturnTrue()
    {
        string value = "https://example.com";
        Url url1 = Url.Create(value);
        Url url2 = Url.Create(value);

        bool result = url1 == url2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WithDifferentValue_ShouldReturnFalse()
    {
        Url url1 = Url.Create("https://example1.com");
        Url url2 = Url.Create("https://example2.com");

        bool result = url1 == url2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void EqualityOperator_WithBothNull_ShouldReturnTrue()
    {
        Url? url1 = null;
        Url? url2 = null;

        bool result = url1 == url2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WithOneNull_ShouldReturnFalse()
    {
        Url url1 = Url.Create("https://example.com");
        Url? url2 = null;

        bool result = url1 == url2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WithSameValue_ShouldReturnFalse()
    {
        string value = "https://example.com";
        Url url1 = Url.Create(value);
        Url url2 = Url.Create(value);

        bool result = url1 != url2;

        _ = result.Should().BeFalse();
    }

    [Fact]
    public void InequalityOperator_WithDifferentValue_ShouldReturnTrue()
    {
        Url url1 = Url.Create("https://example1.com");
        Url url2 = Url.Create("https://example2.com");

        bool result = url1 != url2;

        _ = result.Should().BeTrue();
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        string value = "https://example.com";
        Url url = Url.Create(value);

        string result = url.ToString();

        _ = result.Should().Be(value);
    }
}

