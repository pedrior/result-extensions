namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultExtensionsTests
{
    [Fact]
    public async Task Then_WhenResultIsSuccess_ShouldCallOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action>();

        // Act
        await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action>();

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessTaskFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Task>>();

        // Act
        await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessTaskFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Task>>();

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Then_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedTaskFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedTaskFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Then_WhenResultIsSuccess_ShouldCallOnSuccessFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Result<int>>>();

        A.CallTo(() => onSuccess())
            .Returns(42);

        // Act
        var result = await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustHaveHappenedOnceExactly();

        result.Should().BeEquivalent(42.ToResult());
    }

    [Fact]
    public async Task Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Result<int>>>();

        A.CallTo(() => onSuccess())
            .Returns(42);

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessTaskFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Task<Result<int>>>>();

        A.CallTo(() => onSuccess())
            .Returns(42.ToResult());

        // Act
        var result = await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustHaveHappenedOnceExactly();

        result.Should().BeEquivalent(42.ToResult());
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessTaskFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Task<Result<int>>>>();

        A.CallTo(() => onSuccess())
            .Returns(42.ToResult());

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Then_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, int>>();

        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .Returns(42);

        // Act
        var result = await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeEquivalent(42.ToResult());
    }

    [Fact]
    public async Task Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, int>>();

        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .Returns(42);

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedTaskFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<int>>>();

        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .Returns(Task.FromResult(42));

        // Act
        var result = await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeEquivalent(42.ToResult());
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedTaskFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<int>>>();

        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .Returns(Task.FromResult(42));

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Then_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Result<int>>>();

        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .Returns(42.ToResult());

        // Act
        var result = await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeEquivalent(42.ToResult());
    }

    [Fact]
    public async Task Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Result<int>>>();

        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .Returns(42.ToResult());

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedTaskFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<Result<int>>>>();

        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .Returns(42.ToResult());

        // Act
        var result = await SuccessResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeEquivalent(42.ToResult());
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedTaskFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<Result<int>>>>();

        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .Returns(42.ToResult());

        // Act
        await FailureResult.ThenAsync(() => Task.CompletedTask)
            .ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess.Invoke(A<string>._))
            .MustNotHaveHappened();
    }
}