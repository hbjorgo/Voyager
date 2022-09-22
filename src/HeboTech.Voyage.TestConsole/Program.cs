using HeboTech.Voyage;

int items = 100;
Reporter reporter = new();

reporter.Start(items);
for (int i = 0; i < items; i++)
{
    Thread.Sleep(20);
    reporter.ReportProgress();
    Console.WriteLine(reporter);
}

Console.WriteLine("Done");