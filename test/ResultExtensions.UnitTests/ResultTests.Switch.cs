namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultTests
{
    [Fact]
    public void Switch_WhenResultIsSuccess_ShouldCallOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        SuccessResult.Switch(onSuccess, _ => { });

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Switch_WhenResultIsSuccess_ShouldNotInvokeOnSuccessAction()
    {
        // Arrange
        var onFailure = A.Fake<Action<Error>>();

        // Act
        SuccessResult.Switch(_ => { }, onFailure);

        // Assert
        A.CallTo(() => onFailure(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void Switch_WhenResultIsFailure_ShouldCallOnFailureAction()
    {
        // Arrange
        var onFailure = A.Fake<Action<Error>>();

        // Act
        FailureResult.Switch(_ => { }, onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.FirstError))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Switch_WhenResultIsFailure_ShouldNotInvokeOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        FailureResult.Switch(onSuccess, _ => { });

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task SwitchAsync_WhenResultIsSuccess_ShouldCallOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await SuccessResult.SwitchAsync(onSuccess, _ => Task.CompletedTask);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task SwitchAsync_WhenResultIsSuccess_ShouldNotInvokeOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task>>();

        // Act
        await SuccessResult.SwitchAsync(_ => Task.CompletedTask, onFailure);

        // Assert
        A.CallTo(() => onFailure(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task SwitchAsync_WhenResultIsFailure_ShouldCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task>>();

        // Act
        await FailureResult.SwitchAsync(_ => Task.CompletedTask, onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.FirstError))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task SwitchAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await FailureResult.SwitchAsync(onSuccess, _ => Task.CompletedTask);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void SwitchAll_WhenResultIsSuccess_ShouldCallOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        SuccessResult.SwitchAll(onSuccess, _ => { });

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void SwitchAll_WhenResultIsSuccess_ShouldNotInvokeOnFailureAction()
    {
        // Arrange
        var onFailure = A.Fake<Action<ImmutableArray<Error>>>();

        // Act
        SuccessResult.SwitchAll(_ => { }, onFailure);

        // Assert
        A.CallTo(() => onFailure(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void SwitchAll_WhenResultIsFailure_ShouldCallOnFailureAction()
    {
        // Arrange
        var onFailure = A.Fake<Action<ImmutableArray<Error>>>();

        // Act
        FailureResult.SwitchAll(_ => { }, onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.Errors))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void SwitchAll_WhenResultIsFailure_ShouldNotInvokeOnSuccessAction()
    {
        // Arrange
        var onSuccess = A.Fake<Action<string>>();

        // Act
        FailureResult.SwitchAll(onSuccess, _ => { });

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task SwitchAllAsync_WhenResultIsSuccess_ShouldCallOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await SuccessResult.SwitchAllAsync(onSuccess, _ => Task.CompletedTask);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task SwitchAllAsync_WhenResultIsSuccess_ShouldNotGivenInvokeOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task>>();

        // Act
        await SuccessResult.SwitchAllAsync(_ => Task.CompletedTask, onFailure);

        // Assert
        A.CallTo(() => onFailure(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task SwitchAllAsync_WhenResultIsFailure_ShouldCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task>>();

        // Act
        await FailureResult.SwitchAllAsync(_ => Task.CompletedTask, onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.Errors))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task SwitchAllAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task>>();

        // Act
        await FailureResult.SwitchAllAsync(onSuccess, _ => Task.CompletedTask);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }
}