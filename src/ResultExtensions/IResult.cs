namespace ResultExtensions;

/// <summary>
/// Represents the result of an operation that returns a value.
/// </summary>
/// <typeparam name="T">The type of the value returned by the operation.</typeparam>
public interface IResult<out T> : IResult
{
    /// <summary>
    /// Gets the value returned by the operation.
    /// If the operation was a failure, this will be the default value for the type.
    /// </summary>
    public T Value { get; }
}

/// <summary>
/// Represents the result of an operation.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation was a failure.
    /// </summary>
    public bool IsFailure { get; }

    /// <summary>
    /// Gets the errors that occurred during the operation. Empty if the operation was successful.
    /// </summary>
    public ImmutableArray<Error> Errors { get; }
}