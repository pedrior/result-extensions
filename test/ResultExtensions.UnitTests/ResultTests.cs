namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Result<>))]
public sealed partial class ResultTests
{
    private static readonly Result<string> SuccessResult = "something".ToResult();

    private static readonly Result<string> FailureResult = ImmutableArray.Create(
        Error.Unexpected("something went wrong 1"),
        Error.Unexpected("something went wrong 2"));

    [Fact]
    public void ImplicitConversion_WhenGivenValue_ShouldCreateSuccessResult()
    {
        // Arrange
        const string value = "something";

        // Act
        Result<string> result = value;

        // Assert
        result.Should().BeSuccess(value);
    }

    [Fact]
    public void ImplicitConversion_WhenGivenError_ShouldCreateFailureResult()
    {
        // Arrange
        var error = Error.Unavailable("Something went wrong");

        // Act
        Result<string> result = error;

        // Assert
        result.Should().BeFailure(error);
    }

    [Fact]
    public void ImplicitConversion_WhenGivenImmutableArrayOfErrors_ShouldCreateFailureResult()
    {
        // Arrange
        var errors = ImmutableArray.Create(
            Error.Unexpected("Something went wrong"),
            Error.Unexpected("Something else went wrong"));

        // Act
        Result<string> result = errors;

        // Assert
        result.Should().BeFailure(errors);
    }

    [Fact]
    public void ImplicitConversion_WhenGivenArrayOfErrors_ShouldCreateFailureResult()
    {
        // Arrange
        var errors = new[]
        {
            Error.Unexpected("Something went wrong"),
            Error.Unexpected("Something else went wrong")
        };

        // Act
        Result<string> result = errors;

        // Assert
        result.Should().BeFailure(errors);
    }

    [Fact]
    public void ImplicitConversion_WhenGivenListOfErrors_ShouldCreateFailureResult()
    {
        // Arrange
        var errors = new List<Error>
        {
            Error.Unexpected("Something went wrong"),
            Error.Unexpected("Something else went wrong")
        };

        // Act
        Result<string> result = errors;

        // Assert
        result.Should().BeFailure(errors);
    }

    [Fact]
    public void IsSuccess_WhenResultIsSuccess_ShouldReturnTrue()
    {
        // Arrange
        // Act
        var isSuccess = SuccessResult.IsSuccess;

        // Assert
        isSuccess.Should().BeTrue();
    }

    [Fact]
    public void IsSuccess_WhenResultIsFailure_ShouldReturnFalse()
    {
        // Arrange
        // Act
        var isSuccess = FailureResult.IsSuccess;

        // Assert
        isSuccess.Should().BeFalse();
    }

    [Fact]
    public void IsFailure_WhenResultIsSuccess_ShouldReturnFalse()
    {
        // Arrange
        // Act
        var isFailure = SuccessResult.IsFailure;

        // Assert
        isFailure.Should().BeFalse();
    }

    [Fact]
    public void IsFailure_WhenResultIsFailure_ShouldReturnTrue()
    {
        // Arrange
        // Act
        var isFailure = FailureResult.IsFailure;

        // Assert
        isFailure.Should().BeTrue();
    }

    [Fact]
    public void Value_WhenResultIsSuccess_ShouldReturnValue()
    {
        // Arrange
        // Act
        var value = SuccessResult.Value;

        // Assert
        value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void Value_WhenResultIsFailure_ShouldReturnDefaultValue()
    {
        // Arrange
        Result<int> r1 = Error.Unexpected("Something went wrong for int");
        Result<Uri> r2 = Error.Unexpected("Something went wrong for Uri");

        // Act
        var r1Value = r1.Value;
        var r2Value = r2.Value;

        // Assert
        r1Value.Should().Be(0);
        r2Value.Should().Be(null);
    }

    [Fact]
    public void Errors_WhenResultIsSuccess_ShouldReturnEmptyImmutableArray()
    {
        // Arrange
        // Act
        var errors = SuccessResult.Errors;

        // Assert
        errors.Should().BeEmpty();
    }

    [Fact]
    public void Errors_WhenResultIsFailure_ShouldReturnImmutableArrayWithErrors()
    {
        // Arrange
        // Act
        var errors = FailureResult.Errors;

        // Assert
        errors.Should().BeEquivalentTo(FailureResult.Errors);
    }

    [Fact]
    public void FirstError_WhenResultIsSuccess_ShouldThrowInvalidOperationException()
    {
        // Arrange
        // Act
        var act = () => SuccessResult.FirstError;
        
        // Assert
        act.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("The result is successful.");
    }
    
    [Fact]
    public void FirstError_WhenResultIsFailure_ShouldNotThrownException()
    {
        // Arrange
        // Act
        var act = () => FailureResult.FirstError;
        
        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void FirstError_WhenResultIsFailure_ShouldReturnFirstError()
    {
        // Arrange
        // Act
        var firstError = FailureResult.FirstError;

        // Assert
        firstError.Should().Be(FailureResult.Errors[0]);
    }

    [Fact]
    public void ThrowOnFailure_WhenResultIsSuccess_ShouldNotThrowException()
    {
        // Arrange
        // Act
        var act = () => SuccessResult.ThrowIfFailure();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ThrowOnFailure_WhenResultIsFailureAndCalledWithoutCustomMessage_ShouldThrowExceptionWithErrorToString()
    {
        // Arrange
        // Act
        var act = () => FailureResult.ThrowIfFailure();

        // Assert
        act.Should().ThrowExactly<ResultException>()
            .And.Error.Should().Be(FailureResult.FirstError);
    }

    [Fact]
    public void ThrowOnFailure_WhenResultIsFailureAndCalledWithCustomMessage_ShouldThrowExceptionWithCustomMessage()
    {
        // Arrange
        // Act
        var act = () => FailureResult.ThrowIfFailure("I didn't expect this to happen");

        // Assert
        act.Should().ThrowExactly<ResultException>()
            .WithMessage("I didn't expect this to happen")
            .And.Error.Should().Be(FailureResult.FirstError);
    }

    [Fact]
    public void ToString_WhenResultIsSuccess_ShouldReturnSuccess()
    {
        // Arrange
        // Act
        var result = SuccessResult.ToString();

        // Assert
        result.Should().Be("Success");
    }

    [Fact]
    public void ToString_WhenResultIsFailure_ShouldReturnFirstErrorToString()
    {
        // Arrange
        // Act
        var result = FailureResult.ToString();

        // Assert
        result.Should().Be(FailureResult.FirstError.ToString());
    }
}