using Microsoft.AspNetCore.Http;
using ResultExtensions.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
public sealed partial class HttpResultExtensionsTests
{
    [Fact]
    public void NoContent_WhenResultIsSuccess_ShouldReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.NoContent();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status204NoContent)
            .Should().BeTrue();
    }
    
    [Fact]
    public void NoContent_WhenResultIsFailure_ShouldNotReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = FailureResult.NoContent();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status204NoContent)
            .Should().BeFalse();
    }

    [Fact]
    public async Task NoContent_WhenResultTaskIsSuccess_ShouldReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().NoContent();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status204NoContent)
            .Should().BeTrue();
    }
    
    [Fact]
    public async Task NoContent_WhenResultTaskIsFailure_ShouldNotReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().NoContent();

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status204NoContent)
            .Should().BeFalse();
    }
}