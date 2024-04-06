using ResultExtensions.Common;

namespace ResultExtensions;

/// <summary>
/// Identifies the type of an error.
/// </summary>
public class ErrorType : Enumeration
{
    /// <summary>
    /// Identifies the <see cref="ErrorType.Failure"/> error type.
    /// </summary>
    public static readonly ErrorType Failure = new(nameof(Failure), "An error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.Validation"/> error type.
    /// </summary>
    public static readonly ErrorType Validation = new(nameof(Validation), "A validation error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.Conflict"/> error type.
    /// </summary>
    public static readonly ErrorType Conflict = new(nameof(Conflict), "A conflict error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.NotFound"/> error type.
    /// </summary>
    public static readonly ErrorType NotFound = new(nameof(NotFound), "A not found error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.Unauthorized"/> error type.
    /// </summary>
    public static readonly ErrorType Unauthorized = new(nameof(Unauthorized), "An unauthorized error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.Forbidden"/> error type.
    /// </summary>
    public static readonly ErrorType Forbidden = new(nameof(Forbidden), "A forbidden error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.Unavailable"/> error type.
    /// </summary>
    public static readonly ErrorType Unavailable = new(nameof(Unavailable), "An unavailable error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.Locked"/> error type.
    /// </summary>
    public static readonly ErrorType Locked = new(nameof(Locked), "A locked error has occurred.");

    /// <summary>
    /// Identifies the <see cref="ErrorType.Unexpected"/> error type.
    /// </summary>
    public static readonly ErrorType Unexpected = new(nameof(Unexpected), "An unexpected error has occurred.");

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorType"/> class.
    /// </summary>
    /// <param name="name">The unique name of the error type.</param>
    /// <param name="message">The default message of the error type.</param>
    protected ErrorType(string name, string message) : base(name) => Message = message;

    /// <summary>
    /// Gets the default message of the error type.
    /// </summary>
    public string Message { get; }
}