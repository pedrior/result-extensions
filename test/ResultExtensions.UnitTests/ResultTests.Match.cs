namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultTests
{
    [Fact]
    public void Match_WhenResultIsSuccess_ShouldCallOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, string>>();

        // Act
        SuccessResult.Match(onSuccess, _ => string.Empty);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Match_WhenResultIsSuccess_ShouldNotInvokeOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, string>>();

        // Act
        SuccessResult.Match(_ => string.Empty, onFailure);

        // Assert
        A.CallTo(() => onFailure(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void Match_WhenResultIsFailure_ShouldCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, string>>();

        // Act
        FailureResult.Match(_ => string.Empty, onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.FirstError))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Match_WhenResultIsFailure_ShouldNotInvokeOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, string>>();

        // Act
        FailureResult.Match(onSuccess, _ => string.Empty);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task MatchAsync_WhenResultIsSuccess_ShouldCallOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<string>>>();

        // Act
        await SuccessResult.MatchAsync(onSuccess, _ => Task.FromResult(string.Empty));

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task MatchAsync_WhenResultIsSuccess_ShouldNotInvokeOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task<string>>>();

        // Act
        await SuccessResult.MatchAsync(_ => Task.FromResult(string.Empty), onFailure);

        // Assert
        A.CallTo(() => onFailure(A<Error>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task MatchAsync_WhenResultIsFailure_ShouldCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<Error, Task<string>>>();

        // Act
        await FailureResult.MatchAsync(_ => Task.FromResult(string.Empty), onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.FirstError))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task MatchAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<string>>>();

        // Act
        await FailureResult.MatchAsync(onSuccess, _ => Task.FromResult(string.Empty));

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void MatchAll_WhenResultIsSuccess_ShouldCallOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, string>>();

        // Act
        SuccessResult.MatchAll(onSuccess, _ => string.Empty);

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void MatchAll_WhenResultIsSuccess_ShouldNotInvokeOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, string>>();

        // Act
        SuccessResult.MatchAll(_ => string.Empty, onFailure);

        // Assert
        A.CallTo(() => onFailure(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public void MatchAll_WhenResultIsFailure_ShouldCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, string>>();

        // Act
        FailureResult.MatchAll(_ => string.Empty, onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.Errors))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void MatchAll_WhenResultIsFailure_ShouldNotInvokeOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, string>>();

        // Act
        FailureResult.MatchAll(onSuccess, _ => string.Empty);

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task MatchAllAsync_WhenResultIsSuccess_ShouldCallOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<string>>>();

        // Act
        await SuccessResult.MatchAllAsync(onSuccess, _ => Task.FromResult(string.Empty));

        // Assert
        A.CallTo(() => onSuccess(SuccessResult.Value))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task MatchAllAsync_WhenResultIsSuccess_ShouldNotInvokeOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task<string>>>();

        // Act
        await SuccessResult.MatchAllAsync(_ => Task.FromResult(string.Empty), onFailure);

        // Assert
        A.CallTo(() => onFailure(A<ImmutableArray<Error>>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task MatchAllAsync_WhenResultIsFailure_ShouldCallOnFailureFunc()
    {
        // Arrange
        var onFailure = A.Fake<Func<ImmutableArray<Error>, Task<string>>>();

        // Act
        await FailureResult.MatchAllAsync(_ => Task.FromResult(string.Empty), onFailure);

        // Assert
        A.CallTo(() => onFailure(FailureResult.Errors))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task MatchAllAsync_WhenResultIsFailure_ShouldNotInvokeOnSuccessFunc()
    {
        // Arrange
        var onSuccess = A.Fake<Func<string, Task<string>>>();

        // Act
        await FailureResult.MatchAllAsync(onSuccess, _ => Task.FromResult(string.Empty));

        // Assert
        A.CallTo(() => onSuccess(A<string>._))
            .MustNotHaveHappened();
    }
}