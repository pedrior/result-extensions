using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.Mvc;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/> to produce <see cref="IActionResult"/> objects.
/// </summary>
public static partial class MvcResultExtensions
{
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