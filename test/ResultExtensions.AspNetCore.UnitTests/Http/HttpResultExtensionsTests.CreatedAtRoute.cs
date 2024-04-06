using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ResultExtensions.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
public sealed partial class HttpResultExtensionsTests
{
    [Fact]
    public void CreatedAtRoute_WhenResultIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeTrue();
    }

    [Fact]
    public void CreatedAtRoute_WhenResultIsSuccess_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtRoute(
            routeName: "test",
            routeValues: new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        fixture.GetPropertyValueFromResult<string>(result, "RouteName")
            .Should().Be("test");

        fixture.GetPropertyValueFromResult<RouteValueDictionary>(result, "RouteValues")
            .Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public void
        CreateAtRoute_WhenResultIsSuccessAndCalledWithRouteValuesFunc_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtRoute(
            routeName: "test",
            routeValues: _ => new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        fixture.GetPropertyValueFromResult<RouteValueDictionary>(result, "RouteValues")
            .Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public void CreatedAtRoute_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void CreatedAtRoute_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtRoute(
            routeName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public void CreatedAtRoute_WhenResultIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeFalse();
    }

    [Fact]
    public async Task CreatedAtRoute_WhenResultTaskIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeTrue();
    }

    [Fact]
    public async Task CreatedAtRoute_WhenResultTaskIsSuccess_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtRoute(
            routeName: "test",
            routeValues: new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        fixture.GetPropertyValueFromResult<string>(result, "RouteName")
            .Should().Be("test");

        fixture.GetPropertyValueFromResult<RouteValueDictionary>(result, "RouteValues")
            .Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public async Task
        CreateAtRoute_WhenResultTaskIsSuccessAndCalledWithRouteValuesFunc_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtRoute(
            routeName: "test",
            routeValues: _ => new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        fixture.GetPropertyValueFromResult<RouteValueDictionary>(result, "RouteValues")
            .Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public async Task CreatedAtRoute_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public async Task
        CreatedAtRoute_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtRoute(
            routeName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public async Task CreatedAtRoute_WhenResultTaskIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status201Created)
            .Should().BeFalse();
    }
}