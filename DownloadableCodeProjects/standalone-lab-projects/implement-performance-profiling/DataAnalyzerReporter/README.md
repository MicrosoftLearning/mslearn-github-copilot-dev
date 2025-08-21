# DataAnalyzerReporter

A C# console application that reads CSV data from a file, processes it by calculating sums for each record, and generates a report.

## Usage

```
DataAnalyzerReporter <input_file>
```

## Example

```
DataAnalyzerReporter data.txt
```

This will read the data.txt file, process each line as comma-separated values, calculate the sum of numeric values in each line, and output the results to output.txt.

## Performance Notes

The application includes performance monitoring and reports:
- Memory usage
- Processing time
- Number of records processed

## Files

- `Program.cs` - Main entry point
- `FileLoader.cs` - File reading utilities
- `DataAnalyzer.cs` - Data processing logic
- `ReportGenerator.cs` - Report generation
- `data.txt` - Sample input data
