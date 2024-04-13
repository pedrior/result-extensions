using Microsoft.AspNetCore.Mvc;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed class MvcResultExtensionsTests
{
    private static readonly Result<object> SuccessResult = new
    {
        Name = "John Doe",
        Email = "john@doe.com"
    }.ToResult();

    private static readonly Result<object> FailureResult = ImmutableArray.Create(
        Error.Conflict("Duplicate email address"));

    private static readonly Func<Task<Result<object>>> SuccessResultTask = () => Task.FromResult(SuccessResult);
    private static readonly Func<Task<Result<object>>> FailureResultTask = () => Task.FromResult(FailureResult);
    
    [Fact]
    public void ToResponse_WhenResultIsSuccess_ShouldCallResponseFunction()
    {
        // Arrange
        var func = A.Fake<Func<object, IActionResult>>();
        
        // Act
        SuccessResult.ToResponse(func);
        
        // Assert
        A.CallTo(() => func.Invoke(A<object>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void ToResponse_WhenResultIsFailure_ShouldNotCallResponseFunction()
    {
        // Arrange
        var func = A.Fake<Func<object, IActionResult>>();
        
        // Act
        FailureResult.ToResponse(func);
        
        // Assert
        A.CallTo(() => func.Invoke(A<object>._))
            .MustNotHaveHappened();
    }
    
    [Fact]
    public async Task ToResponseAsync_WhenResultIsSuccess_ShouldCallResponseFunction()
    {
        // Arrange
        var func = A.Fake<Func<object, IActionResult>>();
        
        // Act
        await SuccessResultTask().ToResponseAsync(func);
        
        // Assert
        A.CallTo(() => func.Invoke(A<object>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task ToResponseAsync_WhenResultIsFailure_ShouldNotCallResponseFunction()
    {
        // Arrange
        var func = A.Fake<Func<object, IActionResult>>();
        
        // Act
        await FailureResultTask().ToResponseAsync(func);
        
        // Assert
        A.CallTo(() => func.Invoke(A<object>._))
            .MustNotHaveHappened();
    }
}