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
