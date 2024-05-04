using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace ResultExtensions.FluentAssertions;

/// <summary>
/// Provides a set of custom assertions to assert <see cref="Result{T}"/>s.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ResultAssertions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResultAssertions{T}"/> class.
    /// </summary>
    /// <param name="subject">The <see cref="Result{T}"/> for which the assertions should be made.</param>
    public ResultAssertions(Result<T> subject) : base(subject)
    {
    }

    /// <inheritdoc/>
    protected override string Identifier => nameof(Result<T>);

    /// <summary>
    /// Asserts that the current <see cref="Result{T}"/> is equivalent to the provided <see cref="Result{T}"/>.
    /// Two <see cref="Result{T}"/> are considered equivalent if they have the same <see cref="Result{T}.IsSuccess"/>,
    /// <see cref="Result{T}.Value"/>, and <see cref="Result{T}.Errors"/>; otherwise, the assertion fails.
    /// </summary>
    /// <param name="value">The other <see cref="Result{T}"/> to compare to.</param>
    /// <param name="because">The reason why this assertion is needed.</param>
    /// <param name="becauseArgs">The arguments to format the <paramref name="because"/> message.</param>
    /// <returns>An <see cref="AndWhichConstraint{TAssertions, T}"/> object for chaining assertions.</returns>
    public AndWhichConstraint<ResultAssertions<T>, Result<T>> BeEquivalent(
        Result<T> value,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(Subject == value)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:Result} to be ({0}, {1}, {2}), but found ({3}, {4}, {5}).",
                value.IsSuccess, value.Value, value.Errors, Subject.IsSuccess, Subject.Value, Subject.Errors);

        return new AndWhichConstraint<ResultAssertions<T>, Result<T>>(this, Subject);
    }

    /// <summary>
    /// Asserts that the current <see cref="Result{T}"/> is a success. This means that <see cref="Result{T}.IsSuccess"/>
    /// is <see langword="true"/>; otherwise, the assertion fails.
    /// </summary>
    /// <param name="because">The reason why this assertion is needed.</param>
    /// <param name="becauseArgs">The arguments to format the <paramref name="because"/> message.</param>
    /// <returns>An <see cref="AndWhichConstraint{TAssertions, T}"/> object for chaining assertions.</returns>
    public AndWhichConstraint<ResultAssertions<T>, Result<T>> BeSuccess(
        string because = "",
        params object[] becauseArgs)
    {
        if (Subject.IsFailure)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a success result, but found a failure result with errors: {0}.", Subject.Errors);
        }

        return new AndWhichConstraint<ResultAssertions<T>, Result<T>>(this, Subject);
    }

    /// <summary>
    /// Asserts that the current <see cref="Result{T}"/> is a success and has the provided value. This means that
    /// <see cref="Result{T}.IsSuccess"/> is <see langword="true"/> and <see cref="Result{T}.Value"/> is equal to the
    /// provided <paramref name="value"/>; otherwise, the assertion fails.
    /// </summary>
    /// <param name="value">The value of type <typeparamref name="T"/> to compare to.</param>
    /// <param name="because">The reason why this assertion is needed.</param>
    /// <param name="becauseArgs">The arguments to format the <paramref name="because"/> message.</param>
    /// <returns>An <see cref="AndWhichConstraint{TAssertions, T}"/> object for chaining assertions.</returns>
    public AndWhichConstraint<ResultAssertions<T>, Result<T>> BeSuccess(
        T value,
        string because = "",
        params object[] becauseArgs)
    {
        Subject.Should().BeSuccess();

        Execute.Assertion
            .ForCondition(Subject.Value!.Equals(value))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:Value} to be {0}, but found {1}.", value, Subject.Value);

        return new AndWhichConstraint<ResultAssertions<T>, Result<T>>(this, Subject);
    }

    /// <summary>
    /// Asserts that the current <see cref="Result{T}"/> is a failure. This means that <see cref="Result{T}.IsFailure"/>
    /// is <see langword="true"/>; otherwise, the assertion fails.
    /// </summary>
    /// <param name="because">The reason why this assertion is needed.</param>
    /// <param name="becauseArgs">The arguments to format the <paramref name="because"/> message.</param>
    /// <returns>An <see cref="AndWhichConstraint{TAssertions, T}"/> object for chaining assertions.</returns>
    public AndWhichConstraint<ResultAssertions<T>, Result<T>> BeFailure(
        string because = "",
        params object[] becauseArgs)
    {
        if (Subject.IsSuccess)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a failure result, but found a success result with value: {0}.", Subject.Value);
        }

        return new AndWhichConstraint<ResultAssertions<T>, Result<T>>(this, Subject);
    }

    /// <summary>
    /// Asserts that the current <see cref="Result{T}"/> is a failure and contains the provided <see cref="Error"/>.
    /// This means that <see cref="Result{T}.IsFailure"/> is <see langword="true"/> and <see cref="Result{T}.Errors"/>
    /// contains the provided <paramref name="error"/>; otherwise, the assertion fails.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> that should be contained in the <see cref="Result{T}.Errors"/>.
    /// </param>
    /// <param name="because">The reason why this assertion is needed.</param>
    /// <param name="becauseArgs">The arguments to format the <paramref name="because"/> message.</param>
    /// <returns>An <see cref="AndWhichConstraint{TAssertions, T}"/> object for chaining assertions.</returns>
    public AndWhichConstraint<ResultAssertions<T>, Result<T>> BeFailure(
        Error error,
        string because = "",
        params object[] becauseArgs)
    {
        Subject.Should().BeFailure();

        Execute.Assertion
            .ForCondition(Subject.Errors.Contains(error))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:Errors} to contain {0}, but it did not.", error);

        return new AndWhichConstraint<ResultAssertions<T>, Result<T>>(this, Subject);
    }

    /// <summary>
    /// Asserts that the current <see cref="Result{T}"/> is a failure and contains the provided <see cref="Error"/>s.
    /// This means that <see cref="Result{T}.IsFailure"/> is <see langword="true"/> and <see cref="Result{T}.Errors"/>
    /// contains all the provided <paramref name="errors"/>; otherwise, the assertion fails.
    /// </summary>
    /// <param name="errors">The <see cref="Error"/>s that should be contained in the <see cref="Result{T}.Errors"/>.
    /// </param>
    /// <param name="because">The reason why this assertion is needed.</param>
    /// <param name="becauseArgs">The arguments to format the <paramref name="because"/> message.</param>
    /// <returns>An <see cref="AndWhichConstraint{TAssertions, T}"/> object for chaining assertions.</returns>
    public AndWhichConstraint<ResultAssertions<T>, Result<T>> BeFailure(
        IEnumerable<Error> errors,
        string because = "",
        params object[] becauseArgs)
    {
        Subject.Should().BeFailure();

        Execute.Assertion
            .ForCondition(Subject.Errors.All(e => errors.Contains(e)))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:Errors} to contain {0}, but it did not.", errors);

        return new AndWhichConstraint<ResultAssertions<T>, Result<T>>(this, Subject);
    }

    /// <summary>
    /// Asserts that the current <see cref="Result{T}"/> is a failure and contains the provided <see cref="Error"/>s.
    /// This means that <see cref="Result{T}.IsFailure"/> is <see langword="true"/> and <see cref="Result{T}.Errors"/>
    /// contains the provided <paramref name="errors"/> in the same order; otherwise, the assertion fails.
    /// </summary>
    /// <param name="errors">The <see cref="Error"/>s that should be contained in the <see cref="Result{T}.Errors"/>
    /// in the same order.</param>
    /// <param name="because">The reason why this assertion is needed.</param>
    /// <param name="becauseArgs">The arguments to format the <paramref name="because"/> message.</param>
    /// <returns>An <see cref="AndWhichConstraint{TAssertions, T}"/> object for chaining assertions.</returns>
    public AndWhichConstraint<ResultAssertions<T>, Result<T>> BeFailureSequentially(
        IEnumerable<Error> errors,
        string because = "",
        params object[] becauseArgs)
    {
        Subject.Should().BeFailure();

        Execute.Assertion
            .ForCondition(Subject.Errors.SequenceEqual(errors))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:Errors} to contain same elements in the same order as {0}, but it did not.",
                errors);

        return new AndWhichConstraint<ResultAssertions<T>, Result<T>>(this, Subject);
    }
}