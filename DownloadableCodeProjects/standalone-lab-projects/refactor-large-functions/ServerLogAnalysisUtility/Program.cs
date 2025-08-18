using System;
using System.Collections.Generic;
using System.IO;

namespace LogProcessing;

public class LogAnalyzer
{
    public void AnalyzeLogs(string[] logFiles)
    {
        // Step 1: File I/O Setup (open files, prepare to read)
        if (logFiles == null || logFiles.Length == 0)
        {
            Console.WriteLine("No log files specified. Exiting.");
            return;
        }
        int totalLines = 0;
        double totalResponseTime = 0.0;
        var errorTypeCounts = new Dictionary<string, int>();

        foreach (string filePath in logFiles)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Skipped an empty file path.");
                continue;
            }
            try
            {
                using var reader = new StreamReader(filePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    totalLines++;
                    // Step 2: Parsing and Filtering
                    // Assume log format: Date|Level|Message|...|Duration (fields separated by '|')
                    string[] parts = line.Split('|');
                    if (parts.Length < 3)
                    {
                        // Skip lines that don't have the expected format
                        continue;
                    }
                    string level = parts[1].Trim();    // e.g., "INFO" or "ERROR"
                    string message = parts[2].Trim();
                    // Filter: focus on ERROR level entries for error stats
                    if (level.Equals("ERROR", StringComparison.OrdinalIgnoreCase))
                    {
                        // Determine a simple error category from the message content
                        string errCategory;
                        if (message.Contains("NullReference")) errCategory = "NullReferenceException";
                        else if (message.Contains("OutOfMemory")) errCategory = "OutOfMemoryError";
                        else errCategory = "GeneralError";
                        if (errorTypeCounts.ContainsKey(errCategory))
                            errorTypeCounts[errCategory]++;
                        else
                            errorTypeCounts[errCategory] = 1;
                    }
                    // Step 3: Aggregation (accumulate response time if present)
                    if (parts.Length >= 5)
                    {
                        string durationStr = parts[4].Trim();
                        if (double.TryParse(durationStr, out double duration))
                        {
                            totalResponseTime += duration;
                        }
                        // If parsing fails, ignore that line's duration
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: Log file not found -> {filePath}");
                continue;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Error: Access denied to log file -> {filePath}");
                continue;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
                continue;
            }
        }

        // Step 4: Output/Reporting
        Console.WriteLine("=== Log Analysis Summary ===");
        Console.WriteLine($"Total Lines Processed: {totalLines}");
        if (totalLines > 0)
        {
            double avgResponseTime = totalResponseTime / totalLines;
            Console.WriteLine($"Average Response Time: {avgResponseTime:F3}");
        }
        else
        {
            Console.WriteLine("Average Response Time: N/A (no lines processed)");
        }
        Console.WriteLine("Error Type Counts:");
        if (errorTypeCounts.Count > 0)
        {
            foreach (var kv in errorTypeCounts)
            {
                Console.WriteLine($"  {kv.Key}: {kv.Value}");
            }
        }
        else
        {
            Console.WriteLine("  (No errors encountered)");
        }
    }
}

// A simple Program class to demonstrate usage of LogAnalyzer
public class Program
{
    public static void Main(string[] args)
    {
        var analyzer = new LogAnalyzer();
        analyzer.AnalyzeLogs(args);
        // Usage: pass log file paths as command-line arguments.
        // E.g., `dotnet run logs\\app.log logs\\db.log`
    }
}
