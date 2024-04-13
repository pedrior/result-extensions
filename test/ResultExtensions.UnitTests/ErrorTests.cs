namespace ResultExtensions.UnitTests;

[TestSubject(typeof(Error))]
public sealed class ErrorTests
{
    private const string ErrorCode = "lorem-ipsum";
    private const string ErrorMessage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

    private static readonly Dictionary<string, object?> ErrorDetails = new()
    {
        { "key1", "value1" },
        { "key2", "value2" }
    };

    [Fact]
    public void Failure_WhenCalled_ShouldCreateFailureError()
    {
        // Arrange
        // Act
        var error = Error.Failure(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Failure);
    }

    [Fact]
    public void Validation_WhenCalled_ShouldCreateValidationError()
    {
        // Arrange
        // Act
        var error = Error.Validation(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Validation);
    }

    [Fact]
    public void Conflict_WhenCalled_ShouldCreateConflictError()
    {
        // Arrange
        // Act
        var error = Error.Conflict(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Conflict);
    }

    [Fact]
    public void NotFound_WhenCalled_ShouldCreateNotFoundError()
    {
        // Arrange
        // Act
        var error = Error.NotFound(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.NotFound);
    }

    [Fact]
    public void Unauthorized_WhenCalled_ShouldCreateUnauthorizedError()
    {
        // Arrange
        // Act
        var error = Error.Unauthorized(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Unauthorized);
    }

    [Fact]
    public void Forbidden_WhenCalled_ShouldCreateForbiddenError()
    {
        // Arrange
        // Act
        var error = Error.Forbidden(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Forbidden);
    }

    [Fact]
    public void Unavailable_WhenCalled_ShouldCreateUnavailableError()
    {
        // Arrange
        // Act
        var error = Error.Unavailable(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Unavailable);
    }

    [Fact]
    public void Locked_WhenCalled_ShouldCreateLockedError()
    {
        // Arrange
        // Act
        var error = Error.Locked(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Locked);
    }

    [Fact]
    public void Unexpected_WhenCalled_ShouldCreateUnexpectedError()
    {
        // Arrange
        // Act
        var error = Error.Unexpected(ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, ErrorType.Unexpected);
    }
    
    [Fact]
    public void Custom_WhenCalled_ShouldCreateCustomError()
    {
        // Arrange
        var errorType = CustomErrorTypes.Custom;
        
        // Act
        var error = Error.Custom(errorType, ErrorMessage, ErrorCode, ErrorDetails);

        // Assert
        AssertError(error, errorType);
    }

    [Fact]
    public void FromException_WhenCalled_ShouldCreateErrorWithUnexpectedType()
    {
        // Arrange
        var exception = new Exception(ErrorMessage);

        // Act
        var error = Error.FromException(exception);

        // Assert
        error.Type.Should().Be(ErrorType.Unexpected);
        error.Message.Should().Be(ErrorMessage);
    }

    private static void AssertError(Error error, ErrorType errorType)
    {
        error.Type.Should().Be(errorType);
        error.Message.Should().Be(ErrorMessage);
        error.Code.Should().Be(ErrorCode);
        error.Details.Should().BeEquivalentTo(ErrorDetails);
    }
}

internal sealed class CustomErrorTypes : ErrorType
{
    public static readonly CustomErrorTypes Custom = new("Custom", "Custom error message.");
    
    private CustomErrorTypes(string name, string message) : base(name, message)
    {
    }
}