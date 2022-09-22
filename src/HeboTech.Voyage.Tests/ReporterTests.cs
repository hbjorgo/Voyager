namespace HeboTech.Voyage.Tests
{
    public class ReporterTests
    {
        private readonly Reporter reporter;

        public ReporterTests()
        {
            reporter = new();
        }

        [Fact]
        public void Start_data_is_zero()
        {
            Assert.Equal(0, reporter.TotalItems);
            Assert.Equal(0, reporter.ProcessedItems);
            Assert.Equal(0, reporter.ProgressPercentage, 1);
        }

        [Fact]
        public void Processed_items_are_registered()
        {
            int items = 10;

            reporter.Start(items);
            for (int i = 0; i < 3; i++)
            {
                reporter.ReportProgress();
            }

            Assert.Equal(items, reporter.TotalItems);
            Assert.Equal(3, reporter.ProcessedItems);
            Assert.Equal(30, reporter.ProgressPercentage, 1);
        }

        [Fact]
        public void All_processed_items_are_registered()
        {
            int items = 10;

            reporter.Start(items);
            for (int i = 0; i < items; i++)
            {
                reporter.ReportProgress();
            }

            Assert.Equal(items, reporter.TotalItems);
            Assert.Equal(items, reporter.ProcessedItems);
            Assert.Equal(100, reporter.ProgressPercentage, 1);
        }
    }
}