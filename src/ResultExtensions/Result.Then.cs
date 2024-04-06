namespace ResultExtensions;

public readonly partial struct Result<T>
{
    /// <summary>
    /// Executes an action if the result is successful.
    /// </summary>
    /// <param name="onSuccess">The action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public Result<T> Then(Action onSuccess)
    {
        if (IsSuccess)
        {
            onSuccess();
        }

        return this;
    }

    /// <summary>
    /// Asynchronously executes an action if the result is successful.
    /// </summary>
    /// <param name="onSuccess">The async action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public async Task<Result<T>> ThenAsync(Func<Task> onSuccess)
    {
        if (IsSuccess)
        {
            await onSuccess().ConfigureAwait(false);
        }

        return this;
    }

    /// <summary>
    /// Executes an action with the underlying value if the result is successful.
    /// </summary>
    /// <param name="onSuccess">The action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public Result<T> Then(Action<T> onSuccess)
    {
        if (IsSuccess)
        {
            onSuccess(Value);
        }

        return this;
    }

    /// <summary>
    /// Asynchronously executes an action with the underlying value if the result is successful.
    /// </summary>
    /// <param name="onSuccess">The async action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public async Task<Result<T>> ThenAsync(Func<T, Task> onSuccess)
    {
        if (IsSuccess)
        {
            await onSuccess(Value).ConfigureAwait(false);
        }

        return this;
    }

    /// <summary>
    /// Executes a function if the result is successful, and returns a new value.
    /// </summary>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public Result<TNew> Then<TNew>(Func<T, TNew> onSuccess)
    {
        return IsSuccess
            ? onSuccess(Value)
            : Errors;
    }

    /// <summary>
    /// Asynchronously executes a function if the result is successful, and returns a new value.
    /// </summary>
    /// <param name="onSuccess">The async function to execute.</param>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public async Task<Result<TNew>> ThenAsync<TNew>(Func<T, Task<TNew>> onSuccess)
    {
        return IsSuccess
            ? await onSuccess(Value).ConfigureAwait(false)
            : Errors;
    }

    /// <summary>
    /// Executes a function if the result is successful, and returns a new result.
    /// </summary>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public Result<TNew> Then<TNew>(Func<Result<TNew>> onSuccess)
    {
        return IsSuccess
            ? onSuccess()
            : Errors;
    }

    /// <summary>
    /// Asynchronously executes a function if the result is successful, and returns a new result.
    /// </summary>
    /// <param name="onSuccess">The async function to execute.</param>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public async Task<Result<TNew>> ThenAsync<TNew>(Func<Task<Result<TNew>>> onSuccess)
    {
        return IsSuccess
            ? await onSuccess().ConfigureAwait(false)
            : Errors;
    }

    /// <summary>
    /// Executes a function with the underlying value if the result is successful, and returns a new result.
    /// </summary>
    /// <param name="onSuccess">The function to execute.</param>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public Result<TNew> Then<TNew>(Func<T, Result<TNew>> onSuccess)
    {
        return IsSuccess
            ? onSuccess(Value)
            : Errors;
    }

    /// <summary>
    /// Asynchronously executes a function with the underlying value if the result is successful, and returns a new
    /// result.
    /// </summary>
    /// <param name="onSuccess">The async function to execute.</param>
    /// <typeparam name="TNew">The underlying type of the new result.</typeparam>
    /// <returns>A <see cref="Result{TNew}"/> instance for the <typeparamref name="TNew"/> value.</returns>
    public async Task<Result<TNew>> ThenAsync<TNew>(Func<T, Task<Result<TNew>>> onSuccess)
    {
        return IsSuccess
            ? await onSuccess(Value).ConfigureAwait(false)
            : Errors;
    }
}