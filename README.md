# Voyage

Voyage is a C# library where you can report progress and it will estimate end time, time left, average processing time and more!

Feedback is very much welcome and please request features 🙂

## Usage
Install as NuGet package:
```shell
dotnet add package HeboTech.Voyage
```

Using it is easy:
```csharp
using HeboTech.Voyage;

int items = 5;

Reporter reporter = new();
reporter.Start(items);

for (int i = 0; i < items; i++)
{
    // Do some work here
    reporter.ReportProgress();
    Console.WriteLine(reporter);
}
```
Output:
```
020,00%, #1/5, End time: 01.01.2022 13:37:00, Time left: 00:00:20.1066568
040,00%, #2/5, End time: 01.01.2022 13:37:00, Time left: 00:00:15.1307739
060,00%, #3/5, End time: 01.01.2022 13:37:00, Time left: 00:00:10.0793724
080,00%, #4/5, End time: 01.01.2022 13:37:00, Time left: 00:00:05.0377757
100,00%, #5/5, End time: 01.01.2022 13:37:00, Time left: 00:00:00
```