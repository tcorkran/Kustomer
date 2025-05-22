using Kustomer.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Kustomer.Tests.FunctionalTests;

public class HealthControllerTests
{
    #region GetHealth
    [Fact]
    public void GetHealth_Returns_Ok()
    {
        // Arrange
        var contextMock = new Mock<HttpContext>();

        var controller = new HealthController
        {
            ControllerContext = { HttpContext = contextMock.Object }
        };

        // Act
        var result = controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkResult>(result);
    }
    #endregion
}