using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.Mvc;

public static partial class MvcResultExtensions
{
    /// <summary>
    /// Produces an <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>
    /// with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="actionName">The name of the action to use for generating the URL.</param>
    /// <param name="controllerName">The name of the controller to use for generating the URL.</param>
    /// <param name="routeValues">The route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static IActionResult AcceptedAtAction<T>(
        this Result<T> result,
        string? actionName = null,
        string? controllerName = null,
        object? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.AcceptedAtAction(
            actionName,
            controllerName,
            _ => routeValues,
            context,
            transform);
    }
    
    /// <summary>
    /// Produces an <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>
    /// with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="actionName">The name of the action to use for generating the URL.</param>
    /// <param name="controllerName">The name of the controller to use for generating the URL.</param>
    /// <param name="routeValues">The function providing the route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static IActionResult AcceptedAtAction<T>(
        this Result<T> result,
        string? actionName = null,
        string? controllerName = null,
        Func<T, object?>? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => new AcceptedAtActionResult(
                actionName,
                controllerName,
                routeValues?.Invoke(value),
                transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }
    
    /// <summary>
    /// Asynchronously produces an <see cref="IActionResult"/> object with a status code of
    /// <see cref="StatusCodes.Status202Accepted"/> with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="actionName">The name of the action to use for generating the URL.</param>
    /// <param name="controllerName">The name of the controller to use for generating the URL.</param>
    /// <param name="routeValues">The route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static Task<IActionResult> AcceptedAtAction<T>(
        this Task<Result<T>> result,
        string? actionName = null,
        string? controllerName = null,
        object? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.AcceptedAtAction(
            actionName,
            controllerName,
            _ => routeValues,
            context,
            transform);
    }

    /// <summary>
    /// Asynchronously produces an <see cref="IActionResult"/> object with a status code of
    /// <see cref="StatusCodes.Status202Accepted"/> with the <paramref name="result"/>'s value as the response body.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to produce the <see cref="IActionResult"/>.</param>
    /// <param name="actionName">The name of the action to use for generating the URL.</param>
    /// <param name="controllerName">The name of the controller to use for generating the URL.</param>
    /// <param name="routeValues">The function providing the route data to use for generating the URL.</param>
    /// <param name="context">The <see cref="HttpContext"/> associated with the request.</param>
    /// <param name="transform">The response body transformation function.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// An <see cref="IActionResult"/> object with a status code of <see cref="StatusCodes.Status202Accepted"/>.
    /// </returns>
    public static Task<IActionResult> AcceptedAtAction<T>(
        this Task<Result<T>> result,
        string? actionName = null,
        string? controllerName = null,
        Func<T, object?>? routeValues = null,
        HttpContext? context = null,
        Func<T, object?>? transform = null)
    {
        return result.MatchAll(
            value => new AcceptedAtActionResult(
                actionName,
                controllerName,
                routeValues?.Invoke(value),
                transform?.Invoke(value) ?? value),
            errors => Problem(errors, context));
    }
}