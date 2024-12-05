using System.Collections.Concurrent;

namespace FacilityAreaManagerApi.BackgroundServices
{
    public class ContractProcessingBackgroundService : BackgroundService, IContractProcessingBackgroundService
    {
        private readonly ConcurrentQueue<string> logQueue = new();
        private readonly ILogger<ContractProcessingBackgroundService> logger;

        public ContractProcessingBackgroundService(ILogger<ContractProcessingBackgroundService> logger)
        {
            this.logger = logger;
        }

        public void EnqueueLog(string logMessage)
        {
            logQueue.Enqueue(logMessage);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Contract Processing Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                while (logQueue.TryDequeue(out var logMessage))
                {
                    logger.LogInformation("Processing log: {LogMessage}", logMessage);
                    await Task.Delay(500, stoppingToken); // Simulate work
                }

                await Task.Delay(1000, stoppingToken);
            }

            logger.LogInformation("Contract Processing Background Service is stopping.");
        }
    }
}
