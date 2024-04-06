using Microsoft.AspNetCore.Http;
using ResultExtensions.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
public sealed partial class HttpResultExtensionsTests
{
    [Fact]
    public void Created_WhenResultIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeTrue();
    }

    [Fact]
    public void Created_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void Created_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Created(new Uri("http://localhost/created", UriKind.Absolute),
            transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public void Created_WhenResultIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Created_WhenResultTaskIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeTrue();
    }
    
    [Fact]
    public async Task Created_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }
    
    [Fact]
    public async Task Created_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute),
            transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }
    
    [Fact]
    public async Task Created_WhenResultTaskIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeFalse();
    }
}