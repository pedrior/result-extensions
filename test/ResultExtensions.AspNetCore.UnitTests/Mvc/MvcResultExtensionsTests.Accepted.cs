using Microsoft.AspNetCore.Mvc;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void Accepted_WhenResultIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted();

        // Assert
        result.Should().BeOfType<AcceptedResult>();
    }

    [Fact]
    public void Accepted_WhenResultIsSuccess_ShouldReturnAcceptedResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted();

        // Assert
        result.Should().BeOfType<AcceptedResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void Accepted_WhenResultIsSuccessAndCalledWithLocation_ShouldReturnAcceptedResultWithLocation()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted(
            location: new Uri("https://localhost.com/test", UriKind.Absolute));

        // Assert
        result.Should().BeOfType<AcceptedResult>()
            .Which.Location.Should().Be("https://localhost.com/test");
    }
    
    [Fact]
    public void Accepted_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted(transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<AcceptedResult>()
            .Which.Value.Should().Be("transformed value");
    }
    
    [Fact]
    public void Accepted_WhenResultIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.Accepted();

        // Assert
        result.Should().NotBeOfType<AcceptedResult>();
    }
    
    [Fact]
    public async Task Accepted_WhenResultTaskIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted();

        // Assert
        result.Should().BeOfType<AcceptedResult>();
    }

    [Fact]
    public async Task Accepted_WhenResultTaskIsSuccess_ShouldReturnAcceptedResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted();

        // Assert
        result.Should().BeOfType<AcceptedResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public async Task Accepted_WhenResultTaskIsSuccessAndCalledWithLocation_ShouldReturnAcceptedResultWithLocation()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted(
            location: new Uri("https://localhost.com/test", UriKind.Absolute));

        // Assert
        result.Should().BeOfType<AcceptedResult>()
            .Which.Location.Should().Be("https://localhost.com/test");
    }
    
    [Fact]
    public async Task Accepted_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted(transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<AcceptedResult>()
            .Which.Value.Should().Be("transformed value");
    }
    
    [Fact]
    public async Task Accepted_WhenResultTaskIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().Accepted();

        // Assert
        result.Should().NotBeOfType<AcceptedResult>();
    }
}