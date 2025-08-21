
DataAnalyzerReporter/
├── Program.cs
├── FileLoader.cs
├── DataAnalyzer.cs
├── ReportGenerator.cs
├── DataAnalyzer.csproj
├── data.txt
└── README.md

# 📊 DataAnalyzerReporter

## Program.cs
```csharp
using System;
using System.Diagnostics;
using System.IO;

namespace DataAnalyzerReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DataAnalyzerReporter ===");

            if (args.Length < 1 || !File.Exists(args[0]))
            {
                Console.WriteLine("Usage: DataAnalyzerReporter <input_file>");
                return;
            }

            string inputFile = args[0];
            string outputFile = "output.txt";

            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            string[] records = FileLoader.LoadAllData(inputFile);
            Console.WriteLine($"Loaded {records.Length} records");

            long memUsage = GC.GetTotalMemory(false);
            Console.WriteLine($"Memory usage: {memUsage / 1024} KB");

            Stopwatch sw = Stopwatch.StartNew();
            DataAnalyzer.ProcessAllRecords(records, outputFile);
            sw.Stop();

            Console.WriteLine($"Processing time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Results written to {outputFile}");
        }
    }
}
```

## FileLoader.cs
```csharp
using System;
using System.IO;

namespace DataAnalyzerReporter
{
    public static class FileLoader
    {
        public static string[] LoadAllData(string filePath)
        {
            Console.WriteLine($"Reading file: {filePath}");
            // Performance Issue: High memory usage
            return File.ReadAllLines(filePath);
        }
    }
}
```

## DataAnalyzer.cs
```csharp
using System;
using System.IO;

namespace DataAnalyzerReporter
{
    public static class DataAnalyzer
    {
        public static void ProcessAllRecords(string[] records, string outputPath)
        {
            int count = 0;

            foreach (string record in records)
            {
                if (string.IsNullOrWhiteSpace(record)) continue;

                string[] parts = record.Split(',');
                double sum = 0;

                foreach (string part in parts)
                {
                    if (double.TryParse(part, out double value))
                    {
                        sum += value;
                    }
                }

                string result = $"Sum={sum} for record: {record}";
                ReportGenerator.AppendLineToReport(outputPath, result);
                count++;
            }

            Console.WriteLine($"Processed {count} records");
        }
    }
}
```

## ReportGenerator.cs
```csharp
using System.IO;

namespace DataAnalyzerReporter
{
    public static class ReportGenerator
    {
        public static void AppendLineToReport(string filePath, string line)
        {
            // Performance Issue: Inefficient I/O
            File.AppendAllText(filePath, line + "\n");
        }
    }
}
```
