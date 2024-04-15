using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;
using System.Diagnostics;
using IHttpResult = Microsoft.AspNetCore.Http.IResult;

namespace ResultExtensions.AspNetCore.Http;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/> to produce <see cref="IHttpResult"/> objects.
/// </summary>
public static class HttpResultExtensions
{
    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IHttpResult"/> using the specified response function.
    /// </summary>
    /// <param name="result">The result to convert from.</param>
    /// <param name="response">The response function to use when the <paramref name="result"/> is successful.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <paramref name="result"/>.</typeparam>
    /// <returns>An <see cref="IHttpResult"/> representing the result.</returns>
    public static IHttpResult ToResponse<T>(this Result<T> result, Func<T, IHttpResult> response,
        HttpContext? context = null) => result.IsSuccess ? response(result.Value) : Problem(result.Errors, context);
    
    /// <summary>
    /// Asynchronously converts a <see cref="Result{T}"/> to an <see cref="IHttpResult"/> using the specified response
    /// function.
    /// </summary>
    /// <param name="result">The result to convert from.</param>
    /// <param name="response">The response function to use when the <paramref name="result"/> is successful.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <paramref name="result"/>.</typeparam>
    /// <returns>An <see cref="IHttpResult"/> representing the result.</returns>
    public static async Task<IHttpResult> ToResponseAsync<T>(this Task<Result<T>> result, Func<T, IHttpResult> response,
        HttpContext? context = null) => ToResponse(await result, response, context);

    private static IHttpResult Problem(ImmutableArray<Error> errors, HttpContext? context = null)
    {
        if (errors.IsEmpty)
        {
            throw new ArgumentException("Errors must not be empty.", nameof(errors));
        }

        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        var error = errors[0];
        var errorDict = new Dictionary<string, object>
        {
            ["message"] = error.Message
        };

        if (error.Code is not null)
        {
            errorDict["code"] = error.Code;
        }

        if (error.Details is not null)
        {
            errorDict["details"] = error.Details;
        }

        return Results.Problem(
            statusCode: GlobalErrorMappings.Default.GetStatusCodeForErrorType(error.Type),
            extensions: new Dictionary<string, object?>
            {
                ["trace_id"] = Activity.Current?.Id ?? context?.TraceIdentifier,
                ["errors"] = new object[] { errorDict }
            });
    }

    private static IHttpResult ValidationProblem(ImmutableArray<Error> errors) =>
        Results.ValidationProblem(CreateValidationErrorsDictionary(errors),
            statusCode: GlobalErrorMappings.Default.GetStatusCodeForErrorType(ErrorType.Validation));
    
    private static IDictionary<string, string[]> CreateValidationErrorsDictionary(ImmutableArray<Error> errors)
    {
        return errors
            .GroupBy(e => string.IsNullOrWhiteSpace(e.Code) ? "failure" : e.Code)
            .ToDictionary(g => g.Key, g => g
                .Select(e => e.Message)
                .ToArray());
    }
}