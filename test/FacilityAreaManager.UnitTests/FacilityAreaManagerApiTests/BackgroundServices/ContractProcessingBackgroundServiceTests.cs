using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Concurrent;

namespace FacilityAreaManagerApi.BackgroundServices.Tests
{
    [TestFixture]
    internal class ContractProcessingBackgroundServiceTests
    {
        private Mock<ILogger<ContractProcessingBackgroundService>> loggerMock;
        private ContractProcessingBackgroundService backgroundService;
        private CancellationTokenSource cancellationTokenSource;

        [SetUp]
        public void SetUp()
        {
            loggerMock = new Mock<ILogger<ContractProcessingBackgroundService>>();

            backgroundService = new ContractProcessingBackgroundService(loggerMock.Object);
            cancellationTokenSource = new CancellationTokenSource();
        }

        [TearDown]
        public void TearDown()
        {
            cancellationTokenSource.Dispose();
            backgroundService.Dispose();
        }

        [Test]
        public void EnqueueLog_AddsMessageToQueue()
        {
            // Arrange
            var logMessage = "Test log message";

            // Act
            backgroundService.EnqueueLog(logMessage);

            // Assert
            var logQueueField = typeof(ContractProcessingBackgroundService).GetField("logQueue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var logQueue = (ConcurrentQueue<string>)logQueueField!.GetValue(backgroundService)!;

            Assert.That(logQueue, Is.Not.Null);
            Assert.That(logQueue.Count, Is.EqualTo(1));
            Assert.That(logQueue.TryDequeue(out var dequeuedMessage), Is.True);
            Assert.That(dequeuedMessage, Is.EqualTo(logMessage));
        }

        [Test]
        public async Task ExecuteAsync_ProcessesLogsUntilStopped()
        {
            // Arrange
            var logMessage1 = "First log message";
            var logMessage2 = "Second log message";

            backgroundService.EnqueueLog(logMessage1);
            backgroundService.EnqueueLog(logMessage2);

            // Act
            var task = backgroundService.StartAsync(cancellationTokenSource.Token);

            await Task.Delay(2000);
            await cancellationTokenSource.CancelAsync();

            await task;

            // Assert
            loggerMock.Verify(x => x.Log(
              LogLevel.Information,
              It.IsAny<EventId>(),
              It.IsAny<It.IsAnyType>(),
              null,
              It.IsAny<Func<It.IsAnyType, Exception?, string>>()
              ), Times.Exactly(3));
        }

        [Test]
        public async Task ExecuteAsync_NoLogs_DoesNotProcessLogs()
        {
            // Act
            var task = backgroundService.StartAsync(cancellationTokenSource.Token);

            // Wait briefly and cancel
            await Task.Delay(1500);
            await cancellationTokenSource.CancelAsync();

            await task;

            // Assert
            loggerMock.Verify(x => x.Log(
              LogLevel.Information,
              It.IsAny<EventId>(),
              It.IsAny<It.IsAnyType>(),
              null,
              It.IsAny<Func<It.IsAnyType, Exception?, string>>()
              ), Times.Once);
        }
    }
}