using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    private static readonly Result<object> SuccessResult = new
    {
        Name = "John Doe",
        Email = "john@doe.com"
    }.ToResult();

    private static readonly Result<object> FailureResult = ImmutableArray.Create(
        Error.Conflict("Duplicate email address"));

    private static readonly Func<Task<Result<object>>> SuccessResultTask = () => Task.FromResult(SuccessResult);
    private static readonly Func<Task<Result<object>>> FailureResultTask = () => Task.FromResult(FailureResult);
}