namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultExtensionsTests
{
    private static readonly Result<string> SuccessResult = "something".ToResult();

    private static readonly Result<string> FailureResult = ImmutableArray.Create(
        Error.Unexpected("something went wrong 1"),
        Error.Unexpected("something went wrong 2"));
    
    [Fact]
    public void ToResult_WhenCalled_ShouldReturnNewResultForGivenValue()
    {
        // Arrange
        const int value = 42;

        // Act
        var result = value.ToResult();

        // Assert
        result.Should().BeOfType<Result<int>>();
    }
}