using Microsoft.AspNetCore.Mvc;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void NoContent_WhenResultIsSuccess_ShouldReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.NoContent();

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public void NoContent_WhenResultIsFailure_ShouldNotReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = FailureResult.NoContent();

        // Assert
        result.Should().NotBeOfType<NoContentResult>();
    }

    [Fact]
    public async Task NoContent_WhenResultTaskIsSuccess_ShouldReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().NoContent();

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task NoContent_WhenResultTaskIsFailure_ShouldNotReturnNoContentResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().NoContent();

        // Assert
        result.Should().NotBeOfType<NoContentResult>();
    }
}