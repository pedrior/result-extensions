namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultTests
{
    [Fact]
    public void Else_WhenResultIsFailure_ShouldCallOnFailureAction()
    {
        // Arrange
        var onFailure = A.Fake<Action>();

        // Act
        FailureResult.Else(onFailure);

        // Assert
        A.CallTo(() => onFailure())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Else_WhenResultIsSuccess_ShouldNotInvokeOnFailureAction()
    {
        // Arrange
        var onFailure = A.Fake<Action>();

        // Act
        SuccessResult.Else(onFailure);

        // Assert
        A.CallTo(() => onFailure())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsFailure_ShouldCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Task>>();

        // Act
        await FailureResult.ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsSuccess_ShouldNotInvokeOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Task>>();

        // Act
        await SuccessResult.ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure())
            .MustNotHaveHappened();
    }

    [Fact]
    public void Else_WhenResultIsFailure_ShouldCallOnFailureActionWithFirstError()
    {
        // Arrange
        var onFailure = A.Fake<Action<Error>>();

        // Act
        FailureResult.Else(onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.FirstError))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Else_WhenResultIsSuccess_ShouldNotInvokeOnFailureActionWithFirstError()
    {
        // Arrange
        var onFailure = A.Fake<Action<Error>>();

        // Act
        SuccessResult.Else(onFailure);

        // Assert
        A.CallTo(() => onFailure(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsFailure_ShouldCallOnFailureFuncWithFirstError()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task>>();

        // Act
        await FailureResult.ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.FirstError))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsSuccess_ShouldNotInvokeOnFailureFuncWithFirstError()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task>>();

        // Act
        await SuccessResult.ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void Else_WhenResultIsFailure_ShouldCallOnFailureActionWithAllErrors()
    {
        // Arrange
        var onFailure = A.Fake<Action<ImmutableArray<Error>>>();

        // Act
        FailureResult.Else(onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.Errors))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Else_WhenResultIsSuccess_ShouldNotInvokeOnFailureActionWithAllErrors()
    {
        // Arrange
        var onFailure = A.Fake<Action<ImmutableArray<Error>>>();

        // Act
        SuccessResult.Else(onFailure);

        // Assert
        A.CallTo(() => onFailure(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsFailure_ShouldCallOnFailureFuncWithAllErrors()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task>>();

        // Act
        await FailureResult.ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.Errors))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsSuccess_ShouldNotInvokeOnFailureFuncWithAllErrors()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task>>();

        // Act
        await SuccessResult.ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }
}