namespace ResultExtensions;

/// <summary>
/// Provides a set of extension methods for <see cref="Result{T}"/>.
/// </summary>
public static partial class ResultExtensions
{
    /// <summary>
    /// Creates a new successful <see cref="Result{T}"/> given a value.
    /// </summary>
    /// <param name="value">The value underlying the result.</param>
    /// <typeparam name="T">The type of the underlying value of the result.</typeparam>
    /// <returns>A successful <see cref="Result{T}"/> with the given value.</returns>
    public static Result<T> ToResult<T>(this T value) => value;
}