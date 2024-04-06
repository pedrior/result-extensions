using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.Mvc;

public static partial class MvcResultExtensions
{
    /// <summary>
    /// Produces an <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status204NoContent"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status204NoContent"/>.
    /// </returns>
    public static IActionResult NoContent<T>(this Result<T> result, HttpContext? context = null)
    {
        return result.MatchAll(
            _ => new NoContentResult(),
            errors => Problem(errors, context));
    }

    /// <summary>
    /// Asynchronously produces an <see cref="IActionResult"/> object with a status code of
    /// <see cref="StatusCodes.Status204NoContent"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status204NoContent"/>.
    /// </returns>
    public static Task<IActionResult> NoContent<T>(this Task<Result<T>> result, HttpContext? context = null)
    {
        return result.MatchAll(
            _ => new NoContentResult(),
            errors => Problem(errors, context));
    }
}