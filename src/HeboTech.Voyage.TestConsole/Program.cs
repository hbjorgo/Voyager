using HeboTech.Voyage;

int items = 5;
Reporter reporter = new();

reporter.Start(items);
for (int i = 0; i < items; i++)
{
    Thread.Sleep(5000);
    reporter.ReportProgress();
    Console.WriteLine(reporter);
}

Console.WriteLine("Done");