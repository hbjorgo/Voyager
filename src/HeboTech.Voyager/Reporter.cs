using System;

namespace HeboTech.Voyager
{
    /// <summary>
    /// Estimates future progress based on previous progress
    /// </summary>
    public class Reporter : IReporter
    {
        private readonly object lockObject = new object();
        private readonly Func<DateTime> timeProvider;

        public Reporter()
        {
            timeProvider = () => DateTime.UtcNow;
        }

        public Reporter(Func<DateTime> timeProvider)
        {
            this.timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }

        public int TotalItems { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime LastReportingTime { get; private set; }

        public int ProcessedItems { get; private set; }
        public double Progress { get; private set; }
        public DateTime EstimatedEndTime { get; private set; }
        public TimeSpan EstimatedTimeLeft { get; private set; }
        public TimeSpan LastBatchProcessingTime { get; private set; }
        public TimeSpan AverageProcessingTime { get; private set; }

        public void Start(int totalItems)
        {
            Restart(totalItems);
        }

        public void Restart(int totalItems)
        {
            TotalItems = totalItems;
            StartTime = timeProvider();
            LastReportingTime = default;

            ProcessedItems = default;
            Progress = default;
            EstimatedEndTime = default;
            EstimatedTimeLeft = default;
            LastBatchProcessingTime = default;
            AverageProcessingTime = default;
        }

        public void ReportProgress(int processedItems = 1)
        {
            if (processedItems < 0)
                throw new ArgumentOutOfRangeException(nameof(processedItems), $"Cannot be negative");

            if (processedItems == 0)
                return;

            lock (lockObject)
            {
                if (ProcessedItems == TotalItems)
                    return;

                if (ProcessedItems + processedItems > TotalItems)
                    throw new ArgumentException($"Cannot process more than {TotalItems} items");

                DateTime now = timeProvider();

                ProcessedItems += processedItems;
                Progress = (double)ProcessedItems / TotalItems;
                int remainingItems = TotalItems - ProcessedItems;

                LastBatchProcessingTime = (now - LastReportingTime);
                LastReportingTime = now;

                TimeSpan timePerItem = (now - StartTime) / ProcessedItems;
                AverageProcessingTime = timePerItem;
                EstimatedEndTime = now.Add(timePerItem * remainingItems);
                EstimatedTimeLeft = EstimatedEndTime - now;
            }
        }

        public override string ToString()
        {
            return $"{Progress * 100:0.00}%, #{ProcessedItems}/{TotalItems}, End time: {EstimatedEndTime}, Time left: {EstimatedTimeLeft}";
        }
    }
}