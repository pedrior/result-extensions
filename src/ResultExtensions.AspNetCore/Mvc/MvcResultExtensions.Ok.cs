using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.Mvc;

public static partial class MvcResultExtensions
{
    /// <summary>
    /// Produces an <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status200OK"/>
    /// with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status200OK"/>.
    /// </returns>
    public static IActionResult Ok<T>(
        this Result<T> result,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => new OkObjectResult(transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }
    
    /// <summary>
    /// Asynchronously produces an <see cref="IActionResult"/> object with a status code of
    /// <see cref="StatusCodes.Status200OK"/> with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status200OK"/>.
    /// </returns>
    public static Task<IActionResult> Ok<T>(
        this Task<Result<T>> result,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => new OkObjectResult(transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }
}