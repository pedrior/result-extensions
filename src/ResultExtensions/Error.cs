using System.Collections.Frozen;
using System.Text;

namespace ResultExtensions;

/// <summary>
/// Represents an error that occurred during the execution of an operation. 
/// </summary>
public readonly struct Error : IEquatable<Error>
{
    private Error(
        ErrorType type,
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        Type = type;
        Message = message ?? type.Message;
        Code = code;
        Details = details;
    }

    /// <summary>
    /// Gets the type of the error. See <see cref="ErrorType"/> for possible values.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// Gets the error details.
    /// </summary>
    public FrozenDictionary<string, object?>? Details { get; }

    /// <summary>
    /// Determines whether the specified <see cref="Error"/> is equal to the current <see cref="Error"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Error"/> to compare.</param>
    /// <param name="right">The second <see cref="Error"/> to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="Error"/> is equal to the current <see cref="Error"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Error left, Error right) => left.Equals(right);

    /// <summary>
    /// Determines whether the specified <see cref="Error"/> is not equal to the current <see cref="Error"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Error"/> to compare.</param>
    /// <param name="right">The second <see cref="Error"/> to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="Error"/> is not equal to the current <see cref="Error"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Error left, Error right) => !left.Equals(right);

    /// <summary>
    /// Creates a new <see cref="ErrorType.Failure"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Failure"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Failure(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Failure, message ?? ErrorType.Failure.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.Validation"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Validation"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Validation(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Validation, message ?? ErrorType.Validation.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.Conflict"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Conflict"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Conflict(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Conflict, message ?? ErrorType.Conflict.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.NotFound"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.NotFound"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error NotFound(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.NotFound, message ?? ErrorType.NotFound.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.Unauthorized"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Unauthorized"/> will be
    /// used.</param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Unauthorized(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Unauthorized, message ?? ErrorType.Unauthorized.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.Forbidden"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Forbidden"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Forbidden(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Forbidden, message ?? ErrorType.Forbidden.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.Unavailable"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Unavailable"/> will be
    /// used.</param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Unavailable(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Unavailable, message ?? ErrorType.Unavailable.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.Locked"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Locked"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Locked(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Locked, message ?? ErrorType.Locked.Message, code, details);
    }

    /// <summary>
    /// Creates a new <see cref="ErrorType.Unexpected"/> error instance.
    /// </summary>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <see cref="ErrorType.Unexpected"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error Unexpected(
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(ErrorType.Unexpected, message ?? ErrorType.Unexpected.Message, code, details);
    }

    /// <summary>
    /// Creates a new error instance with the specified <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The error type.</param>
    /// <param name="message">
    /// The error message. If not provided, the default message for the <paramref name="type"/> will be used.
    /// </param>
    /// <param name="code">The error code.</param>
    /// <param name="details">The error details.</param>
    /// <returns></returns>
    public static Error Custom(
        ErrorType type,
        string? message = null,
        string? code = null,
        FrozenDictionary<string, object?>? details = null)
    {
        return new Error(type, message ?? type.Message, code, details);
    }

    /// <summary>
    /// Creates a new error instance from the specified <paramref name="exception"/>.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> from which to create the error.</param>
    /// <returns>A new <see cref="Error"/> instance.</returns>
    public static Error FromException(Exception exception) => new(ErrorType.Unexpected, exception.Message);

    /// <inheritdoc />
    public bool Equals(Error other)
    {
        return Type == other.Type
               && Message == other.Message
               && Code == other.Code
               && CompareDetails(Details, other.Details);

        static bool CompareDetails(IDictionary<string, object?>? x, IDictionary<string, object?>? y)
        {
            return (x, y) switch
            {
                (null, null) => true,
                (not null, null) => false,
                (null, not null) => false,
                _ => x.Count == y.Count && x.All(pair => y.TryGetValue(pair.Key, out var value)
                                                         && Equals(pair.Value, value))
            };
        }
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Error other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Type, Message, Code, Details);

    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new StringBuilder()
            .Append($"{(Code is not null ? $"({Code}) " : string.Empty)}")
            .Append(Message);

        if (Details is null)
        {
            return builder.ToString();
        }

        builder.Append(" | Details: {");
        foreach (var (key, value) in Details)
        {
            builder.Append($" {key}: {value} ");
        }

        return builder.Append('}')
            .ToString();
    }
}