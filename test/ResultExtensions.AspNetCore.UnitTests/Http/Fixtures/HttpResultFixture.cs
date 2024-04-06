using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http.Fixtures;

public sealed class HttpResultFixture
{
    private readonly Assembly httpResultsAssembly = Assembly.Load("Microsoft.AspNetCore.Http.Results");

    private static readonly Dictionary<int, string[]> StatusCodeToResultTypeNames = new()
    {
        { StatusCodes.Status200OK, new[] { "OkObjectResult" } },
        { StatusCodes.Status201Created, new[] { "CreatedResult", "CreatedAtRouteResult" } },
        { StatusCodes.Status202Accepted, new[] { "AcceptedResult", "AcceptedAtRouteResult" } },
        { StatusCodes.Status204NoContent, new[] { "NoContentResult" } }
    };

    public object? GetValueFromResult(object result) => httpResultsAssembly.GetType(
        "Microsoft.AspNetCore.Http.Result.ObjectResult")!.GetProperty("Value")?.GetValue(result);

    public T? GetPropertyValueFromResult<T>(object result, string propertyName) where T : class
    {
        return httpResultsAssembly.GetType(result.GetType().FullName!)!
            .GetProperty(propertyName)?.GetValue(result) as T;
    }

    public bool IsResultForStatusCode(object result, int statusCode)
    {
        return StatusCodeToResultTypeNames.TryGetValue(statusCode, out var resultTypeNames)
               && resultTypeNames.Any(name => result.GetType().FullName?
                   .Equals($"Microsoft.AspNetCore.Http.Result.{name}") ?? false);
    }
}