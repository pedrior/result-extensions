using Microsoft.AspNetCore.Http;
using IHttpResult = Microsoft.AspNetCore.Http.IResult;

namespace ResultExtensions.AspNetCore.Http;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/> to produce <see cref="IHttpResult"/> objects.
/// </summary>
public static partial class HttpResultExtensions
{
    /// <summary>
    /// Produces an <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status204NoContent"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status204NoContent"/>.
    /// </returns>
    public static IHttpResult NoContent<T>(this Result<T> result, HttpContext? context = null)
    {
        return result.MatchAll(
            _ => Results.NoContent(),
            errors => Problem(errors, context));
    }

    /// <summary>
    /// Asynchronously produces an <see cref="IHttpResult"/> object with a status code of
    /// <see cref="StatusCodes.Status204NoContent"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status204NoContent"/>.
    /// </returns>
    public static Task<IHttpResult> NoContent<T>(this Task<Result<T>> result, HttpContext? context = null)
    {
        return result.MatchAll(
            _ => Results.NoContent(),
            errors => Problem(errors, context));
    }
}