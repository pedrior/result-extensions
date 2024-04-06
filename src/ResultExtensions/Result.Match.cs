namespace ResultExtensions;

public readonly partial struct Result<T>
{
    /// <summary>
    /// Executes the appropriate function based on the result state.
    /// </summary>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <typeparam name="TResult">The type of the underlying value of the result.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(FirstError);
    }

    /// <summary>
    /// Asynchronously executes the appropriate function based on the result state.
    /// </summary>
    /// <param name="onSuccess">The async function to execute if the result is successful.</param>
    /// <param name="onFailure">The async function to execute if the result is a failure.</param>
    /// <typeparam name="TResult">The type of the underlying value of the result.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public async Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> onSuccess, Func<Error,
        Task<TResult>> onFailure)
    {
        return IsSuccess
            ? await onSuccess(Value).ConfigureAwait(false)
            : await onFailure(FirstError).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the appropriate function based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> function takes the collection of errors.</remarks>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <typeparam name="TResult">The type of the underlying value of the result.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public TResult MatchAll<TResult>(Func<T, TResult> onSuccess, Func<ImmutableArray<Error>, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Errors);
    }

    /// <summary>
    /// Asynchronously executes the appropriate function based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> function takes the collection of errors.</remarks>
    /// <param name="onSuccess">The async function to execute if the result is successful.</param>
    /// <param name="onFailure">The async function to execute if the result is a failure.</param>
    /// <typeparam name="TResult">The type of the underlying value of the result.</typeparam>
    /// <returns>The result of the executed function.</returns>
    public async Task<TResult> MatchAllAsync<TResult>(Func<T, Task<TResult>> onSuccess,
        Func<ImmutableArray<Error>, Task<TResult>> onFailure)
    {
        return IsSuccess
            ? await onSuccess(Value).ConfigureAwait(false)
            : await onFailure(Errors).ConfigureAwait(false);
    }
}