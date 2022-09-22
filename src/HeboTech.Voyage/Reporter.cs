namespace HeboTech.Voyage
{
    public class Reporter : IReporter
    {
        public int TotalItems { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime LastReportingTime { get; private set; }

        public int ProcessedItems { get; private set; }
        public double ProgressPercentage { get; private set; }
        public DateTime EstimatedEndTime { get; private set; }
        public TimeSpan EstimatedTimeLeft { get; private set; }
        public TimeSpan LastItemProcessingTime { get; private set; }
        public TimeSpan AverageProcessingTime { get; private set; }

        public void Start(int totalItems)
        {
            Restart(totalItems);
        }

        public void Restart(int totalItems)
        {
            TotalItems = totalItems;
            StartTime = DateTime.Now;
            LastReportingTime = StartTime;

            ProcessedItems = default;
            ProgressPercentage = default;
            EstimatedEndTime = default;
            EstimatedTimeLeft = default;
            LastItemProcessingTime = default;
            AverageProcessingTime = default;
        }

        public void ReportProgress(int processedItems = 1)
        {
            if (processedItems < 0)
                throw new ArgumentOutOfRangeException(nameof(processedItems), $"Cannot be negative");

            if (processedItems == 0)
                return;

            if (ProcessedItems == TotalItems)
                return;

            if (ProcessedItems + processedItems > TotalItems)
                throw new ArgumentException($"Cannot process more than {TotalItems} items");

            DateTime now = DateTime.Now;

            ProcessedItems += processedItems;
            ProgressPercentage = ProcessedItems * 100f / TotalItems;
            int remainingItems = TotalItems - ProcessedItems;

            LastItemProcessingTime = (now - LastReportingTime) / processedItems;
            LastReportingTime = now;

            TimeSpan timePerItem = (now - StartTime) / ProcessedItems;
            AverageProcessingTime = timePerItem;
            EstimatedEndTime = now.Add(timePerItem * remainingItems);
            EstimatedTimeLeft = EstimatedEndTime - now;
        }

        public override string ToString()
        {
            return $"{ProgressPercentage:000.00}%, #{ProcessedItems}/{TotalItems}, End time: {EstimatedEndTime}, Time left: {EstimatedTimeLeft}";
        }
    }
}