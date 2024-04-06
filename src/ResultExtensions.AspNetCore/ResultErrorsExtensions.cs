using System.Collections.Immutable;

namespace ResultExtensions.AspNetCore;

internal static class ResultErrorsExtensions
{
    public static IDictionary<string, string[]> ToValidationErrorsDictionary(this ImmutableArray<Error> errors)
    {
        return errors
            .GroupBy(e => string.IsNullOrWhiteSpace(e.Code) ? "failure" : e.Code)
            .ToDictionary(g => g.Key, g => g
                .Select(e => e.Message)
                .ToArray());
    }
}