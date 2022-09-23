using HeboTech.Voyager;

int items = 100;
Reporter reporter = new();

reporter.Start(items);
for (int i = 0; i < items; i++)
{
    Thread.Sleep(100);
    reporter.ReportProgress();
    Console.WriteLine(reporter);
}

Console.WriteLine("Done");