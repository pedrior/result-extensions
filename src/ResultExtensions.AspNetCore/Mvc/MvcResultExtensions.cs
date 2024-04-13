using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.Mvc;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/> to produce <see cref="IActionResult"/> objects.
/// </summary>
public static class MvcResultExtensions
{
    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IActionResult"/> using the specified response function.
    /// </summary>
    /// <param name="result">The result to convert from.</param>
    /// <param name="response">The response function to use when the <paramref name="result"/> is successful.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <paramref name="result"/>.</typeparam>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static IActionResult ToResponse<T>(this Result<T> result, Func<T, IActionResult> response,
        HttpContext? context = null) => result.IsSuccess ? response(result.Value) : Problem(result.Errors, context);
    
    /// <summary>
    /// Asynchronously converts a <see cref="Result{T}"/> to an <see cref="IActionResult"/> using the specified response
    /// function.
    /// </summary>
    /// <param name="result">The result to convert from.</param>
    /// <param name="response">The response function to use when the <paramref name="result"/> is successful.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <paramref name="result"/>.</typeparam>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    public static async Task<IActionResult> ToResponseAsync<T>(this Task<Result<T>> result,
        Func<T, IActionResult> response, HttpContext? context = null) => ToResponse(await result, response, context);
    
    private static IActionResult Problem(ImmutableArray<Error> errors, HttpContext? context)
    {
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
        
        return CreateProblemDetails(new ProblemDetails
        {
            Status = GlobalErrorMappings.Default.GetStatusCodeForErrorType(error.Type),
            Extensions =
            {
                ["trace_id"] = Activity.Current?.Id ?? context?.TraceIdentifier,
                ["errors"] = new object[] { errorDict }
            }
        });
    }

    private static ObjectResult ValidationProblem(ImmutableArray<Error> errors) =>
        CreateProblemDetails(new ValidationProblemDetails(errors.ToValidationErrorsDictionary())
        {
            Status = GlobalErrorMappings.Default.GetStatusCodeForErrorType(ErrorType.Validation)
        });

    private static ObjectResult CreateProblemDetails(ProblemDetails details) => new(details)
    {
        StatusCode = details.Status,
        ContentTypes = { "application/problem+json" }
    };
}