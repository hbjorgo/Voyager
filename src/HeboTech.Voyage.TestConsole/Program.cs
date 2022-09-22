using HeboTech.Voyage;

int items = 200;
Reporter reporter = new();

reporter.Start(items);
for (int i = 0; i < items; i++)
{
    Thread.Sleep(100);
    reporter.ReportProgress(2);
    Console.WriteLine(reporter);
}

Console.WriteLine("Done");