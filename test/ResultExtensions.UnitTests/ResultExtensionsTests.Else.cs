namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultExtensionsTests
{
    [Fact]
    public async Task Else_WhenResultIsFailure_ShouldCallOnFailureAction()
    {
        // Arrange
        var onFailure = A.Fake<Action>();

        // Act
        await FailureResult
            .ElseAsync(() => Task.CompletedTask)
            .Else(onFailure);

        // Assert
        A.CallTo(() => onFailure())
            .MustHaveHappened();
    }

    [Fact]
    public async Task Else_WhenResultIsSuccess_ShouldNotCallOnFailureAction()
    {
        // Arrange
        var onFailure = A.Fake<Action>();

        // Act
        await SuccessResult
            .ElseAsync(() => Task.CompletedTask)
            .Else(onFailure);

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
        await FailureResult
            .ElseAsync(() => Task.CompletedTask)
            .ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure())
            .MustHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsSuccess_ShouldNotCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Task>>();

        // Act
        await SuccessResult
            .ElseAsync(() => Task.CompletedTask)
            .ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Else_WhenResultIsFailure_ShouldCallOnFailureActionWithErrors()
    {
        // Arrange
        var onFailure = A.Fake<Action<Error>>();

        // Act
        await FailureResult
            .ElseAsync(() => Task.CompletedTask)
            .Else(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<Error>._))
            .MustHaveHappened();
    }

    [Fact]
    public async Task Else_WhenResultIsSuccess_ShouldNotCallOnFailureActionWithErrors()
    {
        // Arrange
        var onFailure = A.Fake<Action<Error>>();

        // Act
        await SuccessResult
            .ElseAsync(() => Task.CompletedTask)
            .Else(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsFailure_ShouldCallOnFailureFuncWithErrors()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task>>();

        // Act
        await FailureResult
            .ElseAsync(() => Task.CompletedTask)
            .ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<Error>._))
            .MustHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsSuccess_ShouldNotCallOnFailureFuncWithErrors()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task>>();

        // Act
        await SuccessResult
            .ElseAsync(() => Task.CompletedTask)
            .ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Else_WhenResultIsFailure_ShouldCallOnFailureActionWithErrorsCollection()
    {
        // Arrange
        var onFailure = A.Fake<Action<ImmutableArray<Error>>>();

        // Act
        await FailureResult
            .ElseAsync(() => Task.CompletedTask)
            .Else(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<ImmutableArray<Error>>._))
            .MustHaveHappened();
    }

    [Fact]
    public async Task Else_WhenResultIsSuccess_ShouldNotCallOnFailureActionWithErrorsCollection()
    {
        // Arrange
        var onFailure = A.Fake<Action<ImmutableArray<Error>>>();

        // Act
        await SuccessResult
            .ElseAsync(() => Task.CompletedTask)
            .Else(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsFailure_ShouldCallOnFailureFuncWithErrorsCollection()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task>>();

        // Act
        await FailureResult
            .ElseAsync(() => Task.CompletedTask)
            .ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<ImmutableArray<Error>>._))
            .MustHaveHappened();
    }

    [Fact]
    public async Task ElseAsync_WhenResultIsSuccess_ShouldNotCallOnFailureFuncWithErrorsCollection()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task>>();

        // Act
        await SuccessResult
            .ElseAsync(() => Task.CompletedTask)
            .ElseAsync(onFailure);

        // Assert
        A.CallTo(() => onFailure.Invoke(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }
}