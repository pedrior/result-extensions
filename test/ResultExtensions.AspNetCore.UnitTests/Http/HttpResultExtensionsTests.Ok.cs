using Microsoft.AspNetCore.Http;
using ResultExtensions.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
public sealed partial class HttpResultExtensionsTests
{
    [Fact]
    public void Ok_WhenResultIsSuccess_ShouldReturnOkResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.Ok();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status200OK)
            .Should().BeTrue();
    }

    [Fact]
    public void Ok_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Ok();

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void Ok_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnOkResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Ok(transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public void Ok_WhenResultIsFailure_ShouldNotReturnOkResult()
    {
        // Arrange
        // Act
        var result = FailureResult.Ok();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status200OK)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Ok_WhenResultTaskIsSuccess_ShouldReturnOkResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Ok();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status200OK)
            .Should().BeTrue();
    }
    
    [Fact]
    public async Task Ok_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Ok();

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }
    
    [Fact]
    public async Task Ok_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnOkResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Ok(transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }
    
    [Fact]
    public async Task Ok_WhenResultTaskIsFailure_ShouldNotReturnOkResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().Ok();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status200OK)
            .Should().BeFalse();
    }
}