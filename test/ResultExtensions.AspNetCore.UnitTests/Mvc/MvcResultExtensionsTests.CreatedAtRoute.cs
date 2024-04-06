using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void CreatedAtRoute_WhenResultIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<CreatedAtRouteResult>();
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.RouteName.Should().Be("test");

        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public void CreatedAtRoute_WhenResultIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<CreatedAtRouteResult>();
    }

    [Fact]
    public async Task CreatedAtRoute_WhenResultTaskIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<CreatedAtRouteResult>();
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.RouteName.Should().Be("test");

        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
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
        result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public async Task CreatedAtRoute_WhenResultTaskIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask().CreatedAtRoute(routeName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<CreatedAtRouteResult>();
    }
}