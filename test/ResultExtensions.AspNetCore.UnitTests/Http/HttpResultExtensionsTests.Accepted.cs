using Microsoft.AspNetCore.Http;
using ResultExtensions.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
public sealed partial class HttpResultExtensionsTests
{
    [Fact]
    public void Accepted_WhenResultIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeTrue();
    }
    
    [Fact]
    public void Accepted_WhenResultIsSuccess_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted(location: "https://localhost.com/test");

        // Assert
        fixture.GetPropertyValueFromResult<string>(result, "Location")
            .Should().Be("https://localhost.com/test");
    }

    [Fact]
    public void Accepted_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted();

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void Accepted_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Accepted(transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public void Accepted_WhenResultIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.Accepted();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Accepted_WhenResultTaskIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeTrue();
    }
    
    [Fact]
    public async Task Accepted_WhenResultTaskIsSuccess_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted(location: "https://localhost.com/test");

        // Assert
        fixture.GetPropertyValueFromResult<string>(result, "Location")
            .Should().Be("https://localhost.com/test");
    }

    [Fact]
    public async Task Accepted_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted();

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public async Task
        Accepted_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Accepted(transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public async Task Accepted_WhenResultTaskIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().Accepted();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeFalse();
    }
}