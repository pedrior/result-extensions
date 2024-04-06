namespace ResultExtensions;

public readonly partial struct Result<T>
{
    /// <summary>
    /// Executes an action if the result is a failure.
    /// </summary>
    /// <param name="onFailure">The action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public Result<T> Else(Action onFailure)
    {
        if (IsFailure)
        {
            onFailure();
        }

        return this;
    }

    /// <summary>
    /// Asynchronously executes an action if the result is a failure.
    /// </summary>
    /// <param name="onFailure">The async action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public async Task<Result<T>> ElseAsync(Func<Task> onFailure)
    {
        if (IsFailure)
        {
            await onFailure().ConfigureAwait(false);
        }

        return this;
    }

    /// <summary>
    /// Executes an action with the first error if the result is a failure.
    /// </summary>
    /// <param name="onFailure">The action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public Result<T> Else(Action<Error> onFailure)
    {
        if (IsFailure)
        {
            onFailure(FirstError);
        }

        return this;
    }

    /// <summary>
    /// Asynchronously executes an action with the first error if the result is a failure.
    /// </summary>
    /// <param name="onFailure">The async action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public async Task<Result<T>> ElseAsync(Func<Error, Task> onFailure)
    {
        if (IsFailure)
        {
            await onFailure(FirstError).ConfigureAwait(false);
        }

        return this;
    }

    /// <summary>
    /// Executes an action with all the errors if the result is a failure.
    /// </summary>
    /// <param name="onFailure">The action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public Result<T> Else(Action<ImmutableArray<Error>> onFailure)
    {
        if (IsFailure)
        {
            onFailure(Errors);
        }

        return this;
    }

    /// <summary>
    /// Asynchronously executes an action with all the errors if the result is a failure.
    /// </summary>
    /// <param name="onFailure">The async action to execute.</param>
    /// <returns>The current <see cref="Result{T}"/> instance.</returns>
    public async Task<Result<T>> ElseAsync(Func<ImmutableArray<Error>, Task> onFailure)
    {
        if (IsFailure)
        {
            await onFailure(Errors).ConfigureAwait(false);
        }

        return this;
    }
}