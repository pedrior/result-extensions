using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ResultExtensions.AspNetCore.Mvc;

namespace ResultExtensions.AspNetCore.UnitTests.Mvc;

[TestSubject(typeof(MvcResultExtensions))]
public sealed partial class MvcResultExtensionsTests
{
    [Fact]
    public void AcceptedAtAction_WhenResultIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>();
    }

    [Fact]
    public void AcceptedAtAction_WhenResultIsSuccess_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtAction(
            actionName: "test",
            controllerName: "test-controller",
            routeValues: new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.ActionName.Should().Be("test");

        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.ControllerName.Should().Be("test-controller");

        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public void
        CreateAtAction_WhenResultIsSuccessAndCalledWithActionValuesFunc_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtAction(
            actionName: "test",
            routeValues: _ => new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public void AcceptedAtAction_WhenResultIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public void
        AcceptedAtAction_WhenResultIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = SuccessResult.AcceptedAtAction(
            actionName: "", controllerName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public void AcceptedAtAction_WhenResultIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = FailureResult.AcceptedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<AcceptedAtActionResult>();
    }

    [Fact]
    public async Task AcceptedAtAction_WhenResultTaskIsSuccess_ShouldReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask()
            .AcceptedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>();
    }

    [Fact]
    public async Task AcceptedAtAction_WhenResultTaskIsSuccess_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtAction(
            actionName: "test",
            controllerName: "test-controller",
            routeValues: new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.ActionName.Should().Be("test");

        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.ControllerName.Should().Be("test-controller");

        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public async Task
        CreateAtAction_WhenResultTaskIsSuccessAndCalledWithActionValuesFunc_ShouldReturnAcceptedResultWithCorrectValues()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtAction(
            actionName: "test",
            routeValues: _ => new
            {
                id = 1,
                order = "asc"
            });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.RouteValues.Should().BeEquivalentTo(new RouteValueDictionary(new
            {
                id = 1,
                order = "asc"
            }));
    }

    [Fact]
    public async Task AcceptedAtAction_WhenResultTaskIsSuccess_ShouldReturnResultWithValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask()
            .AcceptedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.Value.Should().Be(SuccessResult.Value);
    }

    [Fact]
    public async Task
        AcceptedAtAction_WhenResultTaskIsSuccessAndCalledWithTransform_ShouldReturnAcceptedResultWithTransformedValue()
    {
        // Arrange
        // Act
        var result = await SuccessResultTask().AcceptedAtAction(
            actionName: "", controllerName: "",
            routeValues: new { },
            transform: _ => "transformed value");

        // Assert
        result.Should().BeOfType<AcceptedAtActionResult>()
            .Which.Value.Should().Be("transformed value");
    }

    [Fact]
    public async Task AcceptedAtAction_WhenResultTaskIsFailure_ShouldNotReturnAcceptedResult()
    {
        // Arrange
        // Act
        var result = await FailureResultTask()
            .AcceptedAtAction(actionName: "", controllerName: "", routeValues: new { });

        // Assert
        result.Should().NotBeOfType<AcceptedAtActionResult>();
    }
}