using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void CreatedAtAction_WhenResultIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public void CreatedAtAction_WhenResultIsSuccess_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtAction(
            actionName: "test",
            controllerName: "test-controller",
            routeValues: new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.ActionName.Should().Be("test");

        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.ControllerName.Should().Be("test-controller");

        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public void
        CreateAtAction_WhenResultIsSuccessAndCalledWithRouteValuesFunc_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtAction(
            actionName: "test",
            routeValues: _ => new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public void CreatedAtAction_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void
        CreatedAtAction_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.CreatedAtAction(
            actionName: "", controllerName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public void CreatedAtAction_WhenResultIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.CreatedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task CreatedAtAction_WhenResultTaskIsSuccess_ShouldReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask()
            .CreatedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task CreatedAtAction_WhenResultTaskIsSuccess_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtAction(
            actionName: "test",
            controllerName: "test-controller",
            routeValues: new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.ActionName.Should().Be("test");

        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.ControllerName.Should().Be("test-controller");

        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public async Task
        CreateAtAction_WhenResultTaskIsSuccessAndCalledWithRouteValuesFunc_ShouldReturnCreatedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtAction(
            actionName: "test",
            routeValues: _ => new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public async Task CreatedAtAction_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask()
            .CreatedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public async Task
        CreatedAtAction_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnCreatedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().CreatedAtAction(
            actionName: "", controllerName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public async Task CreatedAtAction_WhenResultTaskIsFailure_ShouldNotReturnCreatedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask()
            .CreatedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<CreatedAtActionResult>();
    }
}