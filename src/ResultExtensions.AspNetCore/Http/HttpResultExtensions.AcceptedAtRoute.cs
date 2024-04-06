using Microsoft.AspNetCore.Http;
using IHttpResult = Microsoft.AspNetCore.Http.IResult;

namespace ResultExtensions.AspNetCore.Http;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/> to produce <see cref="IHttpResult"/> objects.
/// </summary>
public static partial class HttpResultExtensions
{
    /// <summary>
    /// Produces an <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>
    /// with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="routeName">The name of the route to use for generating the URL.</param>
    /// <param name="routeValues">The route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static IHttpResult AcceptedAtRoute<T>(
        this Result<T> result,
        string? routeName = null,
        object? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.AcceptedAtRoute(
            routeName,
            _ => routeValues,
            context,
            transform);
    }

    /// <summary>
    /// Produces an <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>
    /// with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="routeName">The name of the route to use for generating the URL.</param>
    /// <param name="routeValues">The function providing the route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static IHttpResult AcceptedAtRoute<T>(
        this Result<T> result,
        string? routeName = null,
        Func<T, object?>? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => Results.AcceptedAtRoute(
                routeName,
                routeValues?.Invoke(value),
                transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }

    /// <summary>
    /// Asynchronously produces an <see cref="IHttpResult"/> object with a status code of
    /// <see cref="StatusCodes.Status202Accepted"/>with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="routeName">The name of the route to use for generating the URL.</param>
    /// <param name="routeValues">The route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static Task<IHttpResult> AcceptedAtRoute<T>(
        this Task<Result<T>> result,
        string? routeName = null,
        object? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.AcceptedAtRoute(
            routeName,
            _ => routeValues,
            context,
            transform);
    }

    /// <summary>
    /// Asynchronously produces an <see cref="IHttpResult"/> object with a status code of
    /// <see cref="StatusCodes.Status202Accepted"/>with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IHttpResult"/>.</param>
    /// <param name="routeName">The name of the route to use for generating the URL.</param>
    /// <param name="routeValues">The function providing the route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IHttpResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static Task<IHttpResult> AcceptedAtRoute<T>(
        this Task<Result<T>> result,
        string? routeName = null,
        Func<T, object?>? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => Results.AcceptedAtRoute(
                routeName,
                routeValues?.Invoke(value),
                transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }
}