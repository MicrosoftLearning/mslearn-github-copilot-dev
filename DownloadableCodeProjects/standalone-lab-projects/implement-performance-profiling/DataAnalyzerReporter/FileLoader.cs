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
