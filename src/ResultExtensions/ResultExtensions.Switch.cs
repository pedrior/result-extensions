namespace ResultExtensions;

public static partial class ResultExtensions
{
    /// <summary>
    /// Executes the appropriate action based on the result state.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    /// <param name="onFailure">The action to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    public static async Task Switch<T>(this Task<Result<T>> result, Action<T> onSuccess, Action<Error> onFailure)
    {
        (await result.ConfigureAwait(false))
            .Switch(onSuccess, onFailure);
    }

    /// <summary>
    /// Asynchronously executes the appropriate action based on the result state.
    /// </summary>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The async action to execute if the result is successful.</param>
    /// <param name="onFailure">The async action to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    public static async Task SwitchAsync<T>(this Task<Result<T>> result, Func<T, Task> onSuccess,
        Func<Error, Task> onFailure)
    {
        await (await result.ConfigureAwait(false))
            .SwitchAsync(onSuccess, onFailure)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the appropriate action based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> action takes the collection of errors.</remarks>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The action to execute if the result is successful.</param>
    /// <param name="onFailure">The action to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    public static async Task SwitchAll<T>(this Task<Result<T>> result, Action<T> onSuccess,
        Action<ImmutableArray<Error>> onFailure)
    {
        (await result.ConfigureAwait(false))
            .SwitchAll(onSuccess, onFailure);
    }

    /// <summary>
    /// Asynchronously executes the appropriate action based on the result state.
    /// </summary>
    /// <remarks>The <paramref name="onFailure"/> action takes the collection of errors.</remarks>
    /// <param name="result">The <see cref="Task{Result}"/> to execute the action on.</param>
    /// <param name="onSuccess">The async action to execute if the result is successful.</param>
    /// <param name="onFailure">The async action to execute if the result is a failure.</param>
    /// <typeparam name="T">The underlying type of the result.</typeparam>
    public static async Task SwitchAsyncAll<T>(this Task<Result<T>> result, Func<T, Task> onSuccess,
        Func<ImmutableArray<Error>, Task> onFailure)
    {
        await (await result.ConfigureAwait(false))
            .SwitchAllAsync(onSuccess, onFailure)
            .ConfigureAwait(false);
    }
}