namespace ResultExtensions;

/// <summary>
/// Represents a successful value.
/// </summary>
public readonly struct Success : IEquatable<Success>
{
    private static readonly Success Instance = new();

    /// <summary>
    /// Gets the singleton instance of the <see cref="Success"/> type.
    /// </summary>
    public static ref readonly Success Value => ref Instance;

    /// <summary>
    /// Gets a task that represents a successful result.
    /// </summary>
    public static Task<Success> Task { get; } = System.Threading.Tasks.Task.FromResult(Value);

    /// <summary>
    /// Always returns true.
    /// </summary>
    public static bool operator ==(Success _, Success __) => true;

    /// <summary>
    /// Always returns false.
    /// </summary>
    public static bool operator !=(Success _, Success __) => false;

    /// <summary>
    /// Always returns true.
    /// </summary>
    public bool Equals(Success _) => true;

    /// <summary>
    /// Always returns true when the object is of type <see cref="Success"/>.
    /// </summary>
    public override bool Equals(object? obj) => obj is Success;

    /// <inheritdoc/>
    public override int GetHashCode() => 0;

    /// <inheritdoc/>
    public override string ToString() => nameof(Success);
}