using System.Collections.Immutable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

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
        var problemDetailsFactory = context?.RequestServices.GetRequiredService<ProblemDetailsFactory>();

        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors, context, problemDetailsFactory);
        }

        var error = errors[0];
        var errorDict = new Dictionary<string, object>();
        if (error.Code is not null)
        {
            errorDict["code"] = error.Code;
        }
        
        errorDict["message"] = error.Message;

        if (error.Details is not null)
        {
            errorDict["details"] = error.Details;
        }

        ProblemDetails problemDetails;
        if (problemDetailsFactory is null)
        {
            problemDetails = new ProblemDetails
            {
                Status = GlobalErrorMappings.Default.GetStatusCodeForErrorType(error.Type),
                Extensions = { ["errors"] = new object[] { errorDict } }
            };
        }
        else
        {
            problemDetails = problemDetailsFactory.CreateProblemDetails(
                context!,
                GlobalErrorMappings.Default.GetStatusCodeForErrorType(error.Type));

            problemDetails.Extensions["errors"] = new object[] { errorDict };
        }

        return CreateProblemDetails(problemDetails);
    }

    private static ObjectResult ValidationProblem(
        ImmutableArray<Error> errors,
        HttpContext? context,
        ProblemDetailsFactory? problemDetailsFactory)
    {
        var modelStateDictionary = CreateModelStateDictionary(errors);

        ValidationProblemDetails validationProblemDetails;
        if (problemDetailsFactory is null)
        {
            validationProblemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = GlobalErrorMappings.Default.GetStatusCodeForErrorType(ErrorType.Validation)
            };
        }
        else
        {
            validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                httpContext: context!,
                modelStateDictionary: new ModelStateDictionary(modelStateDictionary),
                statusCode: GlobalErrorMappings.Default.GetStatusCodeForErrorType(ErrorType.Validation));
        }

        return CreateProblemDetails(validationProblemDetails);
    }

    private static ObjectResult CreateProblemDetails(ProblemDetails details) => new(details)
    {
        StatusCode = details.Status,
        ContentTypes = { "application/problem+json" }
    };

    private static ModelStateDictionary CreateModelStateDictionary(ImmutableArray<Error> errors)
    {
        var modelState = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelState.AddModelError(
                key: string.IsNullOrWhiteSpace(error.Code) ? "failure" : error.Code,
                error.Message);
        }

        return modelState;
    }
}