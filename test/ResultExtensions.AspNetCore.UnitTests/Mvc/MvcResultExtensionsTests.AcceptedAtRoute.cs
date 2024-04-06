using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void AcceptedAtRoute_WhenResultIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<AcceptedAtRouteResult>();
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.RouteName.Should().Be("test");

        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public void AcceptedAtRoute_WhenResultIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<AcceptedAtRouteResult>();
    }

    [Fact]
    public async Task AcceptedAtRoute_WhenResultTaskIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<AcceptedAtRouteResult>();
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.RouteName.Should().Be("test");

        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
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
        result.Should().BeOfType<AcceptedAtRouteResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public async Task AcceptedAtRoute_WhenResultTaskIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().AcceptedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<AcceptedAtRouteResult>();
    }
}