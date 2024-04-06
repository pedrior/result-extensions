namespace ResultExtensions;

/// <summary>
/// Represents a result exception.
/// </summary>
public sealed class ResultException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResultException"/> class.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> that caused the exception.</param>
    /// <param name="message">The message that describes the error.</param>
    public ResultException(Error error, string? message) : base(message)
    {
        Error = error;
    }
    
    /// <summary>
    /// Gets the <see cref="Error"/> that caused the exception.
    /// </summary>
    public Error Error { get; }
}