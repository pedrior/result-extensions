namespace ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>
    /// Executes an action if the result is a failure.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onFailure">The action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> Else<T>(this Task<Result<T>> result, Action onFailure)
    {
        return (await result.ConfigureAwait(false))
            .Else(onFailure);
    }

    /// <summary>
    /// Asynchronously executes an action if the result is a failure.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onFailure">The async action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> ElseAsync<T>(this Task<Result<T>> result, Func<Task> onFailure)
    {
        return await (await result.ConfigureAwait(false))
            .ElseAsync(onFailure).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes an action with the first error if the result is a failure.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onFailure">The action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> Else<T>(this Task<Result<T>> result, Action<Error> onFailure)
    {
        return (await result.ConfigureAwait(false))
            .Else(onFailure);
    }

    /// <summary>
    /// Asynchronously executes an action with the first error if the result is a failure.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onFailure">The async action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> ElseAsync<T>(this Task<Result<T>> result, Func<Error, Task> onFailure)
    {
        return await (await result.ConfigureAwait(false))
            .ElseAsync(onFailure).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes an action with all the errors if the result is a failure.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onFailure">The async action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> Else<T>(this Task<Result<T>> result, Action<ImmutableArray<Error>> onFailure)
    {
        return (await result.ConfigureAwait(false))
            .Else(onFailure);
    }

    /// <summary>
    /// Asynchronously executes an action with all the errors if the result is a failure.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onFailure">The async action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> ElseAsync<T>(this Task<Result<T>> result,
        Func<ImmutableArray<Error>, Task> onFailure)
    {
        return await (await result.ConfigureAwait(false))
            .ElseAsync(onFailure).ConfigureAwait(false);
    }
}