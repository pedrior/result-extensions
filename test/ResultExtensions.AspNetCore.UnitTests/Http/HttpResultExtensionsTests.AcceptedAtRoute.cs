using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ResultExtensions.AspNetCore.Http;

namespace ResultExtensions.AspNetCore.UnitTests.Http;

[TestSubject(typeof(HttpResultExtensions))]
public sealed partial class HttpResultExtensionsTests
{
    [Fact]
    public void AcceptedAtRoute_WhenResultIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeTrue();
    }

    [Fact]
    public void AcceptedAtRoute_WhenResultIsSuccess_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtRoute(
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
        CreateAtRoute_WhenResultIsSuccessAndCalledWithRouteValuesFunc_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtRoute(
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
    public void AcceptedAtRoute_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void
        AcceptedAtRoute_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtRoute(
            routeName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public void AcceptedAtRoute_WhenResultIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeFalse();
    }

    [Fact]
    public async Task AcceptedAtRoute_WhenResultTaskIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeTrue();
    }

    [Fact]
    public async Task AcceptedAtRoute_WhenResultTaskIsSuccess_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtRoute(
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
        CreateAtRoute_WhenResultTaskIsSuccessAndCalledWithRouteValuesFunc_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtRoute(
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
    public async Task AcceptedAtRoute_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be(SuccessResult.Value);
    }

    [Fact]
    public async Task
        AcceptedAtRoute_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtRoute(
            routeName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        fixture.GetValueFromResult(result)
            .Should().Be("transformed value");
    }

    [Fact]
    public async Task AcceptedAtRoute_WhenResultTaskIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        fixture.IsResultForStatusCode(result, StatusCodes.Status202Accepted)
            .Should().BeFalse();
    }
}