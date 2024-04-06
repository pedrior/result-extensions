using Microsoft.AspNetCore.Http;
using IHttpResult = Microsoft.AspNetCore.Http.IResult;

namespace ResultExtensions.AspNetCore.Http;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/> to produce <see cref="IHttpResult"/> objects.
/// </summary>
public static partial class HttpResultExtensions
{
    /// <summary>
    /// Produces an <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status201Created"/>
    /// with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="location">The <see cref="Uri"/> at which the content has been created.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status201Created"/>.
    /// </returns>
    public static IHttpResult Created<T>(
        this Result<T> result,
        Uri location,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => Results.Created(location, transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }

    /// <summary>
    /// Asynchronously produces an <see cref="IHttpResult"/> object with a status code of
    /// <see cref="StatusCodes.Status201Created"/> with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="location">The <see cref="Uri"/> at which the content has been created.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status201Created"/>.
    /// </returns>
    public static Task<IHttpResult> Created<T>(
        this Task<Result<T>> result,
        Uri location,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => Results.Created(location, transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }
}