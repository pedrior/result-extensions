using ResultExtensions.AspNetCore.Http;
using ResultExtensions.AspNetCore.UnitTests.Http.Fixtures;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
[Collection(nameof(HttpResultCollectionFixture))]
public sealed partial class HttpResultExtensionsTests
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

    private readonly HttpResultFixture fixture;

    public HttpResultExtensionsTests(HttpResultFixture fixture)
    {
        this.fixture = fixture;
    }
}