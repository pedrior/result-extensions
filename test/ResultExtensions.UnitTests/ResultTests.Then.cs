namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultTests
{
    [Fact]
    public void Then_WhenResultIsSuccess_ShouldCallOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action>();

        // Act
        SuccessResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action>();

        // Act
        FailureResult.Then(onSuccess);

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
        await SuccessResult.ThenAsync(onSuccess);

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
        await FailureResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustNotHaveHappened();
    }

    [Fact]
    public void Then_WhenResultIsSuccess_ShouldCallOnSuccessOfActionWithValue()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        SuccessResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessActionWithValue()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        FailureResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedTaskFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await SuccessResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedTaskFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await FailureResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void Then_WhenResultIsSuccess_ShouldCallOnSuccessFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, int>>();

        A.CallTo(() => onSuccess(SuccessResult.Value))
            .Returns(42);

        // Act
        var result = SuccessResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeSuccess(42);
    }

    [Fact]
    public void Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, int>>();

        // Act
        FailureResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessTaskFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<int>>>();

        A.CallTo(() => onSuccess(SuccessResult.Value))
            .Returns(42);

        // Act
        var result = await SuccessResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeSuccess(42);
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessTaskFuncTransform()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<int>>>();

        // Act
        await FailureResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void Then_WhenResultIsSuccess_ShouldCallOnSuccessFuncTransformWithResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Result<int>>>();

        A.CallTo(() => onSuccess())
            .Returns(42.ToResult());

        // Act
        var result = SuccessResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustHaveHappenedOnceExactly();

        result.Should().BeSuccess(42);
    }

    [Fact]
    public void Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessFuncTransformWithResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Result<int>>>();

        // Act
        FailureResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessTaskFuncTransformWithResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Task<Result<int>>>>();

        A.CallTo(() => onSuccess())
            .Returns(42.ToResult());

        // Act
        var result = await SuccessResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustHaveHappenedOnceExactly();

        result.Should().BeSuccess(42);
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessTaskFuncTransformWithResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<Task<Result<int>>>>();

        // Act
        await FailureResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess())
            .MustNotHaveHappened();
    }

    [Fact]
    public void Then_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Result<int>>>();

        A.CallTo(() => onSuccess(SuccessResult.Value))
            .Returns(42.ToResult());

        // Act
        var result = SuccessResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeSuccess(42);
    }

    [Fact]
    public void Then_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Result<int>>>();

        // Act
        FailureResult.Then(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsSuccess_ShouldCallOnSuccessValueBasedTaskFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<Result<int>>>>();

        A.CallTo(() => onSuccess(SuccessResult.Value))
            .Returns(42.ToResult());

        // Act
        var result = await SuccessResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();

        result.Should().BeSuccess(42);
    }

    [Fact]
    public async Task ThenAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessValueBasedTaskFuncTransformWithNewResult()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<Result<int>>>>();

        // Act
        await FailureResult.ThenAsync(onSuccess);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }
}