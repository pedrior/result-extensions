using Microsoft.AspNetCore.Mvc;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void Created_WhenResultIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        result.Should().BeOfType<CreatedResult>();
    }
    
    [Fact]
    public void Created_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        result.Should().BeOfType<CreatedResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }
    
    [Fact]
    public void Created_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.Created(new Uri("http://localhost/created", UriKind.Absolute),
            transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<CreatedResult>()
            .Which.Value.Should().Be("transformed value");
    }
    
    [Fact]
    public void Created_WhenResultIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        result.Should().NotBeOfType<CreatedResult>();
    }
    
    [Fact]
    public async Task Created_WhenResultTaskIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        result.Should().BeOfType<CreatedResult>();
    }
    
    [Fact]
    public async Task Created_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        result.Should().BeOfType<CreatedResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }
    
    [Fact]
    public async Task Created_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute),
            transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<CreatedResult>()
            .Which.Value.Should().Be("transformed value");
    }
    
    [Fact]
    public async Task Created_WhenResultTaskIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().Created(new Uri("http://localhost/created", UriKind.Absolute));

        // Assert
        result.Should().NotBeOfType<CreatedResult>();
    }
}