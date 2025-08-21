# Contoso Online Store - Performance Profiling Training

## Overview

This project is a realistic e-commerce application designed for training developers on performance profiling and optimization using GitHub Copilot. The application simulates a real-world online store with intentional performance bottlenecks and security considerations.

## Project Structure

```
ContosoOnlineStore/
├── Program.cs                          # Main application entry point
├── appsettings.json                    # Configuration settings
├── ContosoOnlineStore.csproj          # Project file with dependencies
├── Configuration/
│   └── AppSettings.cs                 # Configuration models
├── Services/
│   ├── SecurityValidationService.cs   # Input validation and security
│   └── EmailService.cs                # Email notification service
├── Exceptions/
│   └── CustomExceptions.cs            # Domain-specific exceptions
├── Benchmarks/
│   └── OrderProcessingBenchmarks.cs   # BenchmarkDotNet performance tests
├── Product.cs                         # Product entity with validation
├── Order.cs                           # Order entity with business logic
├── OrderItem.cs                       # Order line item
├── ProductCatalog.cs                  # Product management with search/caching
├── InventoryManager.cs                # Inventory tracking and management
└── OrderProcessor.cs                  # Order processing workflow
```

## Features

### Core Business Logic
- **Product Catalog**: 20 realistic products across different categories
- **Inventory Management**: Stock tracking, reservations, and low-stock alerts
- **Order Processing**: Complete order workflow with validation and receipts
- **Email Notifications**: Order confirmations and shipping notifications
- **Security Validation**: Input sanitization and business rule enforcement

### Security Features (Training Appropriate)
- Input validation and sanitization
- SQL injection prevention patterns
- Business rule validation
- Error handling and logging
- Configuration-based security settings

### Performance Bottlenecks (Intentional for Training)
1. **Inefficient Database Queries**
   - Linear searches instead of indexed lookups
   - N+1 query patterns
   - Unnecessary delays simulating slow queries

2. **Caching Issues**
   - Inefficient cache key generation
   - Missing cache implementations
   - Cache invalidation problems

3. **Synchronous Operations**
   - Blocking async operations
   - Sequential processing where parallel would be better
   - Unnecessary Task.Delay calls

4. **Memory and Resource Issues**
   - Inefficient string building
   - Excessive object allocation
   - Resource leaks in loops

5. **Algorithmic Inefficiencies**
   - O(n) operations that could be O(1)
   - Redundant calculations
   - Inefficient data structures

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio Code or Visual Studio
- GitHub Copilot extension

### Running the Application

1. **Basic Demo**:
   ```bash
   dotnet run
   ```

2. **Performance Benchmarks**:
   ```bash
   dotnet run benchmark
   ```

3. **Build and Test**:
   ```bash
   dotnet build
   dotnet test
   ```

### Expected Output

The application will demonstrate:
- Order processing with performance timing
- Inventory management
- Email notifications (simulated)
- Performance metrics and bottleneck identification

## Performance Training Scenarios

### Scenario 1: Product Lookup Optimization
**Problem**: Linear search through product catalog
**Location**: `ProductCatalog.GetProductById()`
**Improvement**: Implement dictionary-based lookups

### Scenario 2: Search Performance
**Problem**: Inefficient string searching and caching
**Location**: `ProductCatalog.SearchProducts()`
**Improvement**: Optimize search algorithms and caching strategy

### Scenario 3: Order Processing Bottlenecks
**Problem**: Sequential processing and redundant operations
**Location**: `OrderProcessor.FinalizeOrderAsync()`
**Improvement**: Parallel processing and operation batching

### Scenario 4: Inventory Management
**Problem**: Individual stock checks and slow logging
**Location**: `InventoryManager.GetLowStockProducts()`
**Improvement**: Batch operations and async logging

### Scenario 5: Email Service Delays
**Problem**: Sequential email sending and content generation
**Location**: `EmailService.SendConfirmationAsync()`
**Improvement**: Parallel processing and content caching

## Security Considerations (Training Context)

This project includes basic security practices appropriate for training:

### Input Validation
- Email format validation
- Product name sanitization
- Quantity bounds checking
- Price validation ranges

### Business Logic Security
- Inventory overflow protection
- Order size limitations
- Negative inventory prevention
- Price calculation validation

### Error Handling
- Custom exception types
- Proper error logging
- Resource cleanup
- Transaction rollback simulation

## Configuration

The application uses `appsettings.json` for configuration:

```json
{
  "AppSettings": {
    "MaxOrderItems": 50,
    "EmailTimeoutMs": 2000,
    "SecuritySettings": {
      "MaxProductPrice": 10000.00,
      "AllowNegativeInventory": false
    },
    "PerformanceSettings": {
      "CacheExpirationMinutes": 30,
      "DatabaseTimeoutMs": 5000
    }
  }
}
```

## Performance Monitoring

### Built-in Metrics
The application tracks:
- Order processing times
- Product lookup performance
- Search operation timing
- Inventory update duration
- Email sending delays

### BenchmarkDotNet Integration
For detailed performance analysis:
```bash
dotnet run benchmark
```

This will generate detailed performance reports including:
- Method execution times
- Memory allocation patterns
- Garbage collection impact
- Performance comparisons

## Learning Objectives

By working with this project, developers will learn to:

1. **Identify Performance Bottlenecks**
   - Using profiling tools
   - Analyzing execution patterns
   - Understanding performance metrics

2. **Apply Optimization Techniques**
   - Algorithmic improvements
   - Caching strategies
   - Asynchronous programming patterns

3. **Use GitHub Copilot for Performance**
   - Generating optimized code
   - Suggesting performance improvements
   - Creating benchmark tests

4. **Implement Security Best Practices**
   - Input validation patterns
   - Error handling strategies
   - Configuration management

## Performance Improvement Checklist

When optimizing this application, consider:

- [ ] Replace linear searches with indexed lookups
- [ ] Implement efficient caching mechanisms
- [ ] Convert synchronous operations to asynchronous
- [ ] Batch database operations
- [ ] Optimize string operations and memory usage
- [ ] Add connection pooling (simulated)
- [ ] Implement parallel processing where appropriate
- [ ] Reduce unnecessary delays and waits
- [ ] Optimize logging and monitoring overhead

## Support and Resources

- [BenchmarkDotNet Documentation](https://benchmarkdotnet.org/)
- [.NET Performance Best Practices](https://docs.microsoft.com/en-us/dotnet/core/performance/)
- [GitHub Copilot for Performance](https://docs.github.com/en/copilot)

## License

This project is designed for educational purposes as part of Microsoft Learn training materials.
