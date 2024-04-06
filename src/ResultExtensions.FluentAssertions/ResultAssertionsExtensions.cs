namespace ResultExtensions.FluentAssertions;

/// <summary>
/// Provides extension methods to assert a <see cref="Result{T}"/> type.
/// </summary>
public static class ResultAssertionsExtensions
{
    /// <summary>
    /// Returns an <see cref="ResultAssertions{T}"/> object that can be used to assert the current
    /// <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="subject">The <see cref="Result{T}"/> for which the assertions should be made.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>An <see cref="ResultAssertions{T}"/> object that can be used to assert the current
    /// <see cref="Result{T}"/>.
    /// </returns>
    public static ResultAssertions<T> Should<T>(this Result<T> subject) => new(subject);
}