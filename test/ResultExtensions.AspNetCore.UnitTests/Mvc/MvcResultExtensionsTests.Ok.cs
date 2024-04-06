using Microsoft.AspNetCore.Mvc;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void Ok_WhenResultIsSuccess_ShouldReturnOkResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.Ok();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void Ok_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Ok();

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void Ok_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnOkResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Ok(transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public void Ok_WhenResultIsFailure_ShouldNotReturnOkResult()
    {
        // Arrange
        // Act
        var result = FailureResult.Ok();

        // Assert
        result.Should().NotBeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Ok_WhenResultTaskIsSuccess_ShouldReturnOkResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Ok();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Ok_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Ok();

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public async Task Ok_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnOkResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Ok(transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public async Task Ok_WhenResultTaskIsFailure_ShouldNotReturnOkResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().Ok();

        // Assert
        result.Should().NotBeOfType<OkObjectResult>();
    }
}