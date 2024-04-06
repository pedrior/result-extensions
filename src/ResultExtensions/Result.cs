using System.Diagnostics;

namespace ResultExtensions;

/// <summary>
/// Represents a discriminated union of a result <typeparamref name="T"/> or a collection of <see cref="Error"/>.
/// </summary>
/// <typeparam name="T">The underlying type of the result value.</typeparam>
/// <seealso cref="IResult{T}"/>
public readonly partial struct Result<T> : IResult<T>, IEquatable<Result<T>>
{
    private Result(T value) => Value = value;

    private Result(Error error) => Errors = ImmutableArray.Create(error);

    private Result(ImmutableArray<Error> errors) => Errors = errors;

    /// <inheritdoc />
    public T Value { get; } = default!;

    /// <inheritdoc />
    public bool IsSuccess => Errors.Length is 0;

    /// <inheritdoc />
    public bool IsFailure => !IsSuccess;

    /// <inheritdoc />
    public ImmutableArray<Error> Errors { get; } = ImmutableArray<Error>.Empty;

    /// <summary>
    /// Gets the first error in the collection of errors.
    /// </summary>
    /// <exception cref="InvalidOperationException">The result is successful.</exception>
    public Error FirstError => IsFailure
        ? Errors[0]
        : throw new InvalidOperationException("The result is successful.");

    /// <summary>
    /// Implicitly converts a <typeparamref name="T"/> value to a successful result.
    /// </summary>
    /// <param name="value">The <typeparamref name="T"/> value to be wrapped in a successful result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the successful result.</returns>
    public static implicit operator Result<T>(T value) => new(value);

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a failed result.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static implicit operator Result<T>(Error error) => new(error);

    /// <summary>
    /// Implicitly converts an <see cref="ImmutableArray{Error}"/> to a failed result.
    /// </summary>
    /// <param name="errors">The errors to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static implicit operator Result<T>(ImmutableArray<Error> errors) => new(errors);

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> array to a failed result.
    /// </summary>
    /// <param name="errors">The errors to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static implicit operator Result<T>(Error[] errors) => new(errors.ToImmutableArray());

    /// <summary>
    /// Implicitly converts a <see cref="List{Error}"/> to a failed result.
    /// </summary>
    /// <param name="errors">The errors to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static implicit operator Result<T>(List<Error> errors) => new(errors.ToImmutableArray());

    /// <summary>
    /// Creates a successful result with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to be wrapped in a successful result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the successful result.</returns>
    public static Result<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a failed result with the specified <paramref name="error"/>.
    /// </summary>
    /// <param name="error">The error to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static Result<T> Failure(Error error) => new(error);

    /// <summary>
    /// Creates a failed result with the specified <paramref name="errors"/>.
    /// </summary>
    /// <param name="errors">The errors to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static Result<T> Failure(ImmutableArray<Error> errors) => new(errors);

    /// <summary>
    /// Creates a failed result with the specified <paramref name="errors"/>.
    /// </summary>
    /// <param name="errors">The errors to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static Result<T> Failure(IEnumerable<Error> errors) => new(errors.ToImmutableArray());

    /// <summary>
    /// Creates a failed result with the specified <paramref name="errors"/>.
    /// </summary>
    /// <param name="errors">The errors to be wrapped in a failed result.</param>
    /// <returns>A <see cref="Result{T}"/> representing the failed result.</returns>
    public static Result<T> Failure(params Error[] errors) => new(errors.ToImmutableArray());

    /// <summary>
    /// Throws an exception if the result is a failure.
    /// </summary>
    /// <param name="message">The message to include in the exception.</param>
    /// <exception cref="ResultException">The exception representing the failure.</exception>
    [DebuggerStepThrough]
    public Result<T> ThrowIfFailure(string? message = null)
    {
        return IsSuccess
            ? this
            : throw new ResultException(FirstError, message ?? $"{FirstError.Type} result");
    }

    /// <summary>
    /// Compares two <see cref="Result{T}"/> objects for equality.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Result<T> left, Result<T> right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Result{T}"/> objects for inequality.
    /// </summary>
    /// <param name="left">The first <see cref="Result{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="Result{T}"/> to compare.</param>
    /// <returns><see langword="true"/> if the objects are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Result<T> left, Result<T> right) => !left.Equals(right);

    /// <summary>
    /// Compares the current <see cref="Result{T}"/> objects with another instance for equality.
    /// </summary>
    /// <param name="other">The objects to compare with this instance.</param>
    /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Result<T> other)
    {
        return EqualityComparer<T>.Default.Equals(Value, other.Value)
               && IsSuccess == other.IsSuccess
               && Errors.SequenceEqual(other.Errors);
    }

    /// <summary>
    /// Compares the current <see cref="Result{T}"/> objects with another object for equality.
    /// </summary>
    /// <param name="obj">The object to compare with this objects.</param>
    /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Result<T> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Value, IsSuccess, Errors);

    /// <inheritdoc />
    public override string ToString() => IsSuccess ? "Success" : FirstError.ToString();
}