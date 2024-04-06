using Microsoft.AspNetCore.Http;

namespace ResultExtensions.AspNetCore;

/// <summary>
/// Provides methods to map <see cref="ErrorType"/>s to HTTP status codes.
/// </summary>
public sealed class GlobalErrorMappings
{
    private static readonly Lazy<GlobalErrorMappings> Instance =
        new(() => new GlobalErrorMappings());

    private readonly Dictionary<ErrorType, int> statusCodeMappings = new()
    {
        [ErrorType.Validation] = StatusCodes.Status400BadRequest,
        [ErrorType.Unauthorized] = StatusCodes.Status401Unauthorized,
        [ErrorType.Forbidden] = StatusCodes.Status403Forbidden,
        [ErrorType.NotFound] = StatusCodes.Status404NotFound,
        [ErrorType.Conflict] = StatusCodes.Status409Conflict,
        [ErrorType.Failure] = StatusCodes.Status422UnprocessableEntity,
        [ErrorType.Locked] = StatusCodes.Status423Locked,
        [ErrorType.Unexpected] = StatusCodes.Status500InternalServerError,
        [ErrorType.Unavailable] = StatusCodes.Status503ServiceUnavailable
    };

    /// <summary>
    /// Gets the default instance of the <see cref="GlobalErrorMappings"/>.
    /// </summary>
    public static GlobalErrorMappings Default => Instance.Value;

    /// <summary>
    /// Maps an <see cref="ErrorType"/> to an HTTP status code.
    /// </summary>
    /// <param name="errorType">The <see cref="ErrorType"/> which the HTTP status code will be mapped to.</param>
    /// <param name="statusCode">The HTTP status code to be mapped to the provided <paramref name="errorType"/>.</param>
    /// <returns>The current instance of the <see cref="GlobalErrorMappings"/>.</returns>
    /// <exception cref="InvalidOperationException">Unsupported <paramref name="errorType"/> provided.</exception>
    public GlobalErrorMappings MapToHttpStatusCode(ErrorType errorType, int statusCode)
    {
        statusCodeMappings[errorType] = statusCode;
        return this;
    }

    internal int GetStatusCodeForErrorType(ErrorType errorType) =>
        statusCodeMappings.TryGetValue(errorType, out var statusCode)
            ? statusCode
            : throw new InvalidOperationException($"Error type '{errorType}' is not supported by the convention.");
}