using System;

namespace HeboTech.Voyager
{
    /// <summary>
    /// Estimates future progress based on previous progress
    /// </summary>
    public interface IReporter
    {
        /// <summary>
        /// The average processing time per item
        /// </summary>
        TimeSpan AverageProcessingTime { get; }

        /// <summary>
        /// Estimated end time based on total number of items and the total time spent so far
        /// </summary>
        DateTime EstimatedEndTime { get; }

        /// <summary>
        /// Estimated time left based on total number of items and the total time spent so far
        /// </summary>
        TimeSpan EstimatedTimeLeft { get; }

        /// <summary>
        /// Time spent between the last and previous reporting time
        /// </summary>
        TimeSpan LastBatchProcessingTime { get; }

        /// <summary>
        /// Point in time when the last report occured
        /// </summary>
        DateTime LastReportingTime { get; }

        /// <summary>
        /// Number of items processed so far
        /// </summary>
        int ProcessedItems { get; }

        /// <summary>
        /// Ratio of the total items processed so far
        /// </summary>
        double Progress { get; }

        /// <summary>
        /// Point in time when the reporter was started
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// Total number of items
        /// </summary>
        int TotalItems { get; }

        /// <summary>
        /// Report progress
        /// </summary>
        /// <param name="processedItems">The number of items processed</param>
        void ReportProgress(int processedItems = 1);

        /// <summary>
        /// Start progress calculation
        /// </summary>
        /// <param name="totalItems">The total number of items to process</param>
        void Start(int totalItems);

        /// <summary>
        /// Restart progress calculation
        /// </summary>
        /// <param name="totalItems">The total number of items to process</param>
        void Restart(int totalItems);
    }
}