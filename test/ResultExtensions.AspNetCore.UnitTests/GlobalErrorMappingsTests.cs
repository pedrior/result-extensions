using Microsoft.AspNetCore.Http;
using ResultExtensions.Common;

namespace ResultExtensions.AspNetCore.UnitTests;

[TestSubject(typeof(GlobalErrorMappings))]
public class GlobalErrorMappingsTests
{
    [Fact]
    public void Default_WhenCalled_ShouldReturnDefaultInstanceWithAllErrorTypesMapped()
    {
        // Arrange
        var allErrorsTypes = Enumeration.List<ErrorType>();

        // Act
        var actions = allErrorsTypes.Select(errorType => (Action)(() => GlobalErrorMappings.Default
            .GetStatusCodeForErrorType(errorType))).ToList();

        // Assert
        actions.ForEach(action => action.Should().NotThrow());
    }

    [Fact]
    public void MapStatusCode_WhenCalledWithSupportedErrorType_ShouldMapStatusCode()
    {
        // Arrange
        var errorType = ErrorType.Validation;
        const int statusCode = StatusCodes.Status500InternalServerError;

        // Act
        var act = () => GlobalErrorMappings.Default.MapToHttpStatusCode(errorType, statusCode);

        // Assert
        act.Should().NotThrow();
        GlobalErrorMappings.Default.GetStatusCodeForErrorType(errorType).Should().Be(statusCode);
    }
}