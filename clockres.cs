using System;
using System.Diagnostics;
double resolution = 1E9 / Stopwatch.Frequency;
Console.WriteLine("The minimum measurable time on this system is: {0} nanoseconds", resolution);
