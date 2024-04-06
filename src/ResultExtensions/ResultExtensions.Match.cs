namespace ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>
    /// Executes the appropriate function based on the result state.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result of the executed function.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public static async Task<TResult> Match<T, TResult>(this Task<Result<T>> result, Func<T, TResult> onSuccess,
        Func<Error, TResult> onFailure)
    {
        return (await result.ConfigureAwait(false))
            .Match(onSuccess, onFailure);
    }

    /// <summary>
    /// Asynchronously executes the appropriate function based on the result state.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The async function to execute if the result is successful.</param>
    /// <param name="onFailure">The async function to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result of the executed function.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public static async Task<TResult> MatchAsync<T, TResult>(this Task<Result<T>> result,
        Func<T, Task<TResult>> onSuccess, Func<Error, Task<TResult>> onFailure)
    {
        return await (await result.ConfigureAwait(false))
            .MatchAsync(onSuccess, onFailure).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously executes the appropriate function based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> function takes the collection of errors.</remarks>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result of the executed function.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public static async Task<TResult> MatchAll<T, TResult>(this Task<Result<T>> result, Func<T, TResult> onSuccess,
        Func<ImmutableArray<Error>, TResult> onFailure)
    {
        return (await result.ConfigureAwait(false))
            .MatchAll(onSuccess, onFailure);
    }

    /// <summary>
    /// Asynchronously executes the appropriate function based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> function takes the collection of errors.</remarks>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The async function to execute if the result is successful.</param>
    /// <param name="onFailure">The async function to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result of the executed function.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public static async Task<TResult> MatchAllAsync<T, TResult>(this Task<Result<T>> result,
        Func<T, Task<TResult>> onSuccess, Func<ImmutableArray<Error>, Task<TResult>> onFailure)
    {
        return await (await result.ConfigureAwait(false))
            .MatchAllAsync(onSuccess, onFailure).ConfigureAwait(false);
    }
}