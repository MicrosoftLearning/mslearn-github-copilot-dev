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
