using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FacilityAreaManagerApi.Middlewares.Tests
{
    [TestFixture]
    internal class ApiKeyMiddlewareTests
    {
        private Mock<RequestDelegate> nextMock;
        private Mock<IConfiguration> configurationMock;
        private ApiKeyMiddleware? middleware;

        [SetUp]
        public void SetUp()
        {
            nextMock = new Mock<RequestDelegate>();
            configurationMock = new Mock<IConfiguration>();
        }

        [Test]
        public async Task InvokeAsync_ValidApiKey_CallsNextDelegate()
        {
            // Arrange
            var validApiKey = "test-valid-api-key";
            configurationMock.Setup(c => c[Configuration.API_KEY]).Returns(validApiKey);

            middleware = new ApiKeyMiddleware(nextMock.Object, configurationMock.Object);

            var context = new DefaultHttpContext();
            context.Request.Headers.Append("X-Api-Key", validApiKey);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.That(context.Response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            nextMock.Verify(n => n(context), Times.Once);
        }

        [Test]
        public async Task InvokeAsync_InvalidApiKey_ReturnsUnauthorized()
        {
            // Arrange
            var validApiKey = "test-valid-api-key";
            configurationMock.Setup(c => c[Configuration.API_KEY]).Returns(validApiKey);

            middleware = new ApiKeyMiddleware(nextMock.Object, configurationMock.Object);

            var context = new DefaultHttpContext();
            context.Request.Headers.Append("X-Api-Key", "invalid-api-key");

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.That(context.Response.StatusCode, Is.EqualTo(StatusCodes.Status401Unauthorized));

            nextMock.Verify(n => n(It.IsAny<HttpContext>()), Times.Never);
        }

        [Test]
        public async Task InvokeAsync_MissingApiKeyHeader_ReturnsUnauthorized()
        {
            // Arrange
            var validApiKey = "test-valid-api-key";
            configurationMock.Setup(c => c[Configuration.API_KEY]).Returns(validApiKey);

            middleware = new ApiKeyMiddleware(nextMock.Object, configurationMock.Object);

            var context = new DefaultHttpContext();

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.That(context.Response.StatusCode, Is.EqualTo(StatusCodes.Status401Unauthorized));

            nextMock.Verify(n => n(It.IsAny<HttpContext>()), Times.Never);
        }
    }
}