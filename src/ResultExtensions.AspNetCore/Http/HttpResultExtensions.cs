using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;
using System.Diagnostics;
using IHttpResult = Microsoft.AspNetCore.Http.IResult;

namespace ResultExtensions.AspNetCore.Http;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/> to produce <see cref="IHttpResult"/> objects.
/// </summary>
public static partial class HttpResultExtensions
{
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
        return Results.Problem(
            statusCode: GlobalErrorMappings.Default.GetStatusCodeForErrorType(error.Type),
            extensions: new Dictionary<string, object?>
            {
                ["trace_id"] = Activity.Current?.Id ?? context?.TraceIdentifier,
                ["errors"] = new
                {
                    message = error.Message,
                    code = error.Code,
                    details = error.Details
                }
            });
    }

    private static IHttpResult ValidationProblem(ImmutableArray<Error> errors) =>
        Results.ValidationProblem(errors.ToValidationErrorsDictionary(),
            statusCode: GlobalErrorMappings.Default.GetStatusCodeForErrorType(ErrorType.Validation));
}