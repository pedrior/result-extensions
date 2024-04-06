namespace ResultExtensions;

public readonly partial struct Result<T>
{
    /// <summary>
    /// Executes the appropriate action based on the result state.
    /// </summary>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    /// <param name="onFailure">The action to execute if the result is a failure.</param>
    public void Switch(Action<T> onSuccess, Action<Error> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(Value);
        }
        else
        {
            onFailure(FirstError);
        }
    }

    /// <summary>
    /// Asynchronously executes the appropriate action based on the result state.
    /// </summary>
    /// <param name="onSuccess">The async action to execute if the result is successful.</param>
    /// <param name="onFailure">The async action to execute if the result is a failure.</param>
    public async Task SwitchAsync(Func<T, Task> onSuccess, Func<Error, Task> onFailure)
    {
        await (IsSuccess ? onSuccess(Value) : onFailure(FirstError))
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the appropriate action based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> function takes the collection of errors.</remarks>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    /// <param name="onFailure">The action to execute if the result is a failure.</param>
    public void SwitchAll(Action<T> onSuccess, Action<ImmutableArray<Error>> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(Value);
        }
        else
        {
            onFailure(Errors);
        }
    }

    /// <summary>
    /// Asynchronously executes the appropriate action based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> function takes the collection of errors.</remarks>
    /// <param name="onSuccess">The async action to execute if the result is successful.</param>
    /// <param name="onFailure">The async action to execute if the result is a failure.</param>
    public async Task SwitchAllAsync(Func<T, Task> onSuccess, Func<ImmutableArray<Error>, Task> onFailure)
    {
        await (IsSuccess ? onSuccess(Value) : onFailure(Errors))
            .ConfigureAwait(false);
    }
}