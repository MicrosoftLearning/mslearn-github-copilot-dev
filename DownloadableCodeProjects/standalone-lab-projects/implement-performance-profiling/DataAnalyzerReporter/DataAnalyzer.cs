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
