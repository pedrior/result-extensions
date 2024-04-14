namespace ResultExtensions;

/// <summary>
/// A static class that provides methods to combine errors from multiple <see cref="IResult"/> instances.
/// </summary>
public static class ErrorCombiner
{
    /// <summary>
    /// Combines the errors from the provided <see cref="IResult"/> instances into a single
    /// <see cref="ImmutableArray"/>.
    /// </summary>
    /// <param name="results">An array of <see cref="IResult"/> instances from which to combine errors.</param>
    /// <returns>An <see cref="ImmutableArray"/> of combined errors.</returns>
    public static ImmutableArray<Error> Combine(params IResult[] results)
    {
        var builder = ImmutableArray.CreateBuilder<Error>();
        foreach (var result in results)
        {
            builder.AddRange(result.Errors);
        }
        
        return builder.ToImmutable();
    }

    /// <summary>
    /// Combines the provided errors with the errors from the provided <see cref="IResult"/> instances into a single
    /// <see cref="ImmutableArray"/>.
    /// </summary>
    /// <param name="errors">
    /// An <see cref="ImmutableArray"/> of errors to combine with the errors from the IResult instances.
    /// </param>
    /// <param name="results">An array of <see cref="IResult"/> instances from which to combine errors.</param>
    /// <returns>An <see cref="ImmutableArray"/> of combined errors.</returns>
    public static ImmutableArray<Error> Combine(ImmutableArray<Error> errors, params IResult[] results)
    {
        var builder = ImmutableArray.CreateBuilder<Error>();
        builder.AddRange(errors);
        
        foreach (var result in results)
        {
            builder.AddRange(result.Errors);
        }
        
        return builder.ToImmutable();
    }
}