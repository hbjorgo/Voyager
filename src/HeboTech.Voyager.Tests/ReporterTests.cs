namespace HeboTech.Voyager.Tests
{
    public class ReporterTests
    {
        private readonly Reporter reporter;
        private readonly DateTime baseTime;
        private DateTime now;

        public ReporterTests()
        {
            baseTime = now = new DateTime(2022, 9, 1, 0, 0, 0, DateTimeKind.Utc);
            reporter = new(() => now);
        }

        [Fact]
        public void Start_data_is_zero()
        {
            Assert.Equal(0, reporter.ProcessedItems);
            Assert.Equal(0, reporter.Progress);
            Assert.Equal(0, reporter.TotalItems);

            Assert.Equal(TimeSpan.Zero, reporter.AverageProcessingTime);
            Assert.Equal(TimeSpan.Zero, reporter.EstimatedTimeLeft);
            Assert.Equal(TimeSpan.Zero, reporter.LastBatchProcessingTime);

            Assert.Equal(DateTime.MinValue, reporter.EstimatedEndTime);
            Assert.Equal(DateTime.MinValue, reporter.LastReportingTime);
            Assert.Equal(DateTime.MinValue, reporter.StartTime);
        }

        [Fact]
        public void Restart_resets_data()
        {
            reporter.Start(10);
            reporter.ReportProgress();
            reporter.Restart(100);

            Assert.Equal(0, reporter.ProcessedItems);
            Assert.Equal(0, reporter.Progress);
            Assert.Equal(100, reporter.TotalItems);

            Assert.Equal(TimeSpan.Zero, reporter.AverageProcessingTime);
            Assert.Equal(TimeSpan.Zero, reporter.EstimatedTimeLeft);
            Assert.Equal(TimeSpan.Zero, reporter.LastBatchProcessingTime);

            Assert.Equal(DateTime.MinValue, reporter.EstimatedEndTime);
            Assert.Equal(DateTime.MinValue, reporter.LastReportingTime);
            Assert.Equal(now, reporter.StartTime);
        }

        [Fact]
        public void Processed_items_are_registered()
        {
            reporter.Start(10);

            for (int i = 0; i < 4; i++)
            {
                now += TimeSpan.FromMinutes(1);
                reporter.ReportProgress();
            }

            Assert.Equal(4, reporter.ProcessedItems);
            Assert.Equal(0.4, reporter.Progress);
            Assert.Equal(10, reporter.TotalItems);

            Assert.Equal(TimeSpan.FromMinutes(1), reporter.AverageProcessingTime);
            Assert.Equal(TimeSpan.FromMinutes(6), reporter.EstimatedTimeLeft);
            Assert.Equal(TimeSpan.FromMinutes(1), reporter.LastBatchProcessingTime);

            Assert.Equal(now + TimeSpan.FromMinutes(6), reporter.EstimatedEndTime);
            Assert.Equal(now, reporter.LastReportingTime);
            Assert.Equal(baseTime, reporter.StartTime);
        }

        [Fact]
        public void All_processed_items_are_registered()
        {
            reporter.Start(10);

            for (int i = 0; i < 10; i++)
            {
                now += TimeSpan.FromMinutes(1);
                reporter.ReportProgress();
            }

            Assert.Equal(10, reporter.ProcessedItems);
            Assert.Equal(1, reporter.Progress);
            Assert.Equal(10, reporter.TotalItems);

            Assert.Equal(TimeSpan.FromMinutes(1), reporter.AverageProcessingTime);
            Assert.Equal(TimeSpan.Zero, reporter.EstimatedTimeLeft);
            Assert.Equal(TimeSpan.FromMinutes(1), reporter.LastBatchProcessingTime);

            Assert.Equal(now, reporter.EstimatedEndTime);
            Assert.Equal(now, reporter.LastReportingTime);
            Assert.Equal(baseTime, reporter.StartTime);
        }

        [Fact]
        public void All_processed_items_are_registered_when_processing_batches()
        {
            reporter.Start(10);

            for (int i = 0; i < 5; i++)
            {
                now += TimeSpan.FromMinutes(1);
                reporter.ReportProgress(2);
            }

            Assert.Equal(10, reporter.ProcessedItems);
            Assert.Equal(1, reporter.Progress);
            Assert.Equal(10, reporter.TotalItems);

            Assert.Equal(TimeSpan.FromSeconds(30), reporter.AverageProcessingTime);
            Assert.Equal(TimeSpan.Zero, reporter.EstimatedTimeLeft);
            Assert.Equal(TimeSpan.FromMinutes(1), reporter.LastBatchProcessingTime);

            Assert.Equal(now, reporter.EstimatedEndTime);
            Assert.Equal(now, reporter.LastReportingTime);
            Assert.Equal(baseTime, reporter.StartTime);
        }

        [Fact]
        public void Constructor_throws_if_timeprovider_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new Reporter(null));
        }

        [Fact]
        public void Report_throws_if_argument_is_negative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => reporter.ReportProgress(-1));
        }

        [Fact]
        public void Report_throws_if_argument_processedItems_are_greater_than_totalItems()
        {
            reporter.Start(10);
            Assert.Throws<ArgumentException>(() => reporter.ReportProgress(100));
        }

        [Fact]
        public void Report_is_not_altered_if_reporting_zero()
        {
            reporter.Start(10);

            for (int i = 0; i < 4; i++)
            {
                now += TimeSpan.FromMinutes(1);
                reporter.ReportProgress();
            }
            reporter.ReportProgress(0);

            Assert.Equal(4, reporter.ProcessedItems);
            Assert.Equal(0.4, reporter.Progress);
            Assert.Equal(10, reporter.TotalItems);

            Assert.Equal(TimeSpan.FromMinutes(1), reporter.AverageProcessingTime);
            Assert.Equal(TimeSpan.FromMinutes(6), reporter.EstimatedTimeLeft);
            Assert.Equal(TimeSpan.FromMinutes(1), reporter.LastBatchProcessingTime);

            Assert.Equal(now + TimeSpan.FromMinutes(6), reporter.EstimatedEndTime);
            Assert.Equal(now, reporter.LastReportingTime);
            Assert.Equal(baseTime, reporter.StartTime);
        }

        [Fact]
        public void Report_is_not_altered_if_processing_more_than_totalItems()
        {
            reporter.Start(10);

            for (int i = 0; i < 10; i++)
            {
                now += TimeSpan.FromMinutes(1);
                reporter.ReportProgress();
            }
            reporter.ReportProgress();

            Assert.Equal(10, reporter.ProcessedItems);
            Assert.Equal(1, reporter.Progress);
            Assert.Equal(10, reporter.TotalItems);

            Assert.Equal(TimeSpan.FromMinutes(1), reporter.AverageProcessingTime);
            Assert.Equal(TimeSpan.Zero, reporter.EstimatedTimeLeft);
            Assert.Equal(TimeSpan.FromMinutes(1), reporter.LastBatchProcessingTime);

            Assert.Equal(now, reporter.EstimatedEndTime);
            Assert.Equal(now, reporter.LastReportingTime);
            Assert.Equal(baseTime, reporter.StartTime);
        }
    }
}