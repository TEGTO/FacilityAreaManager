namespace FacilityAreaManagerApi.BackgroundServices
{
    public interface IContractProcessingBackgroundService
    {
        public void EnqueueLog(string logMessage);
    }
}