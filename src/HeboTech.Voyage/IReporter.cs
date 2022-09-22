namespace HeboTech.Voyage
{
    public interface IReporter
    {
        TimeSpan AverageProcessingTime { get; }
        DateTime EstimatedEndTime { get; }
        TimeSpan EstimatedTimeLeft { get; }
        TimeSpan LastItemProcessingTime { get; }
        DateTime LastReportingTime { get; }
        int ProcessedItems { get; }
        double ProgressPercentage { get; }
        DateTime StartTime { get; }
        int TotalItems { get; }

        void ReportProgress(int processedItems = 1);
        void Start(int totalItems);
        void Restart(int totalItems);
    }
}