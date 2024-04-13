using ResultExtensions.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
public sealed class HttpResultExtensionsTests
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
        var func = A.Fake<Func<object, Microsoft.AspNetCore.Http.IResult>>();
        
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
        var func = A.Fake<Func<object, Microsoft.AspNetCore.Http.IResult>>();
        
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
        var func = A.Fake<Func<object, Microsoft.AspNetCore.Http.IResult>>();
        
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
        var func = A.Fake<Func<object, Microsoft.AspNetCore.Http.IResult>>();
        
        // Act
        await FailureResultTask().ToResponseAsync(func);
        
        // Assert
        A.CallTo(() => func.Invoke(A<object>._))
            .MustNotHaveHappened();
    }
}