namespace ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>
    /// Executes an action if the result is successful.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> Then<T>(this Task<Result<T>> result, Action onSuccess)
    {
        return (await result.ConfigureAwait(false))
            .Then(onSuccess);
    }

    /// <summary>
    /// Asynchronously executes an action if the result is successful.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> ThenAsync<T>(this Task<Result<T>> result, Func<Task> onSuccess)
    {
        return await (await result.ConfigureAwait(false))
            .ThenAsync(onSuccess).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes an action with the underlying value if the result is successful.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> Then<T>(this Task<Result<T>> result, Action<T> onSuccess)
    {
        return (await result.ConfigureAwait(false))
            .Then(onSuccess);
    }

    /// <summary>
    /// Asynchronously executes an action with the underlying value if the result is successful.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The action to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public static async Task<Result<T>> ThenAsync<T>(this Task<Result<T>> result, Func<T, Task> onSuccess)
    {
        return await (await result.ConfigureAwait(false))
            .ThenAsync(onSuccess).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a function if the result is successful, and returns a new result.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public static async Task<Result<TNew>> Then<T, TNew>(this Task<Result<T>> result, Func<Result<TNew>> onSuccess)
    {
        return (await result.ConfigureAwait(false))
            .Then(onSuccess);
    }

    /// <summary>
    /// Asynchronously executes a function if the result is successful, and returns a new result.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public static async Task<Result<TNew>> ThenAsync<T, TNew>(this Task<Result<T>> result,
        Func<Task<Result<TNew>>> onSuccess)
    {
        return await (await result.ConfigureAwait(false))
            .ThenAsync(onSuccess).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a function if the result is successful, and returns a new value.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public static async Task<Result<TNew>> Then<T, TNew>(this Task<Result<T>> result, Func<T, TNew> onSuccess)
    {
        return (await result.ConfigureAwait(false))
            .Then(onSuccess);
    }

    /// <summary>
    /// Asynchronously executes a function if the result is successful, and returns a new value.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public static async Task<Result<TNew>> ThenAsync<T, TNew>(this Task<Result<T>> result,
        Func<T, Task<TNew>> onSuccess)
    {
        return await (await result.ConfigureAwait(false))
            .ThenAsync(onSuccess).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a function if the result is successful, and returns a new result.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public static async Task<Result<TNew>> Then<T, TNew>(this Task<Result<T>> result, Func<T, Result<TNew>> onSuccess)
    {
        return (await result.ConfigureAwait(false))
            .Then(onSuccess);
    }

    /// <summary>
    /// Asynchronously executes a function if the result is successful, and returns a new result.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the function on.</param>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public static async Task<Result<TNew>> ThenAsync<T, TNew>(this Task<Result<T>> result,
        Func<T, Task<Result<TNew>>> onSuccess)
    {
        return await (await result.ConfigureAwait(false))
            .ThenAsync(onSuccess).ConfigureAwait(false);
    }
}