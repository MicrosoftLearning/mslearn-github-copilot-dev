using ContosoOnlineStore;
using ContosoOnlineStore.Benchmarks;
using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Exceptions;
using ContosoOnlineStore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ContosoOnlineStore
{
    class Program
    {
        private static ServiceProvider? _serviceProvider;
        private static ILogger<Program>? _logger;

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== Contoso Online Store - Performance Profiling Demo ===");
                Console.WriteLine();

                // Setup dependency injection and configuration
                SetupServices();
                _logger = _serviceProvider!.GetRequiredService<ILogger<Program>>();

                _logger.LogInformation("Starting Contoso Online Store application");

                // Check for benchmark argument
                if (args.Length > 0 && args[0].Equals("benchmark", StringComparison.OrdinalIgnoreCase))
                {
                    BenchmarkRunner.RunBenchmarks();
                    return;
                }

                // Run the main application demo
                await RunApplicationDemoAsync();

                // Run performance tests
                await RunPerformanceTestsAsync();

                _logger.LogInformation("Application completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application error: {ex.Message}");
                _logger?.LogError(ex, "Application failed with error");
                Environment.Exit(1);
            }
            finally
            {
                (_serviceProvider as IDisposable)?.Dispose();
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }

        private static void SetupServices()
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Setup dependency injection
            var services = new ServiceCollection();

            // Configure logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            // Configure settings
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // Register application services
            services.AddSingleton<ISecurityValidationService, SecurityValidationService>();
            services.AddSingleton<IProductCatalog, ProductCatalog>();
            services.AddSingleton<IInventoryManager, InventoryManager>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IOrderProcessor, OrderProcessor>();

            _serviceProvider = services.BuildServiceProvider();
        }

        private static async Task RunApplicationDemoAsync()
        {
            try
            {
                Console.WriteLine("🏪 Initializing store components...");

                var catalog = _serviceProvider!.GetRequiredService<IProductCatalog>();
                var inventory = _serviceProvider.GetRequiredService<IInventoryManager>();
                var processor = _serviceProvider.GetRequiredService<IOrderProcessor>();

                Console.WriteLine($"✅ Loaded {catalog.GetAllProducts().Count} products");

                // Create a realistic test order
                Console.WriteLine("\n📋 Creating test order...");
                var order = new Order("customer@example.com", "123 Main Street, Seattle, WA 98101");
                order.AddItem(new OrderItem(1, 2)); // iPhone 15 Pro x2
                order.AddItem(new OrderItem(5, 1)); // Anker PowerCore 20K x1
                order.AddItem(new OrderItem(10, 3)); // Apple AirPods Pro 2 x3
                order.AddItem(new OrderItem(15, 1)); // Echo Dot 5th Gen x1

                Console.WriteLine($"📦 Order {order.OrderId} created with {order.Items.Count} different products");

                // Display initial inventory
                Console.WriteLine("\n📊 Initial inventory levels:");
                foreach (var item in order.Items)
                {
                    var product = catalog.GetProductById(item.ProductId);
                    var stock = inventory.GetStockLevel(item.ProductId);
                    Console.WriteLine($"   - {product?.Name}: {stock} units available");
                }

                // Process order with timing
                Console.WriteLine("\n⏱️  Processing order...");
                var stopwatch = Stopwatch.StartNew();

                var result = await processor.ProcessOrderWithValidationAsync(order);

                stopwatch.Stop();

                // Display results
                if (result.Success)
                {
                    Console.WriteLine($"✅ Order processed successfully!");
                    Console.WriteLine($"💰 Total Cost: {result.TotalAmount:C}");
                    Console.WriteLine($"⏱️  Processing Time: {stopwatch.ElapsedMilliseconds} ms");
                    Console.WriteLine($"📋 Items Processed: {result.ProcessedItems}");

                    if (result.Warnings.Any())
                    {
                        Console.WriteLine("\n⚠️  Warnings:");
                        foreach (var warning in result.Warnings)
                        {
                            Console.WriteLine($"   - {warning}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"❌ Order processing failed: {result.ErrorMessage}");
                }

                // Display final inventory
                Console.WriteLine("\n📊 Final inventory levels:");
                foreach (var item in order.Items)
                {
                    var product = catalog.GetProductById(item.ProductId);
                    var stock = inventory.GetStockLevel(item.ProductId);
                    Console.WriteLine($"   - {product?.Name}: {stock} units remaining");
                }

                // Generate receipt
                if (result.Success)
                {
                    Console.WriteLine("\n🧾 Generating receipt...");
                    var receipt = await processor.GenerateOrderReceiptAsync(order);
                    Console.WriteLine(receipt);
                }

            }
            catch (Exception ex)
            {
                _logger!.LogError(ex, "Demo execution failed");
                Console.WriteLine($"❌ Demo failed: {ex.Message}");
                throw;
            }
        }

        private static async Task RunPerformanceTestsAsync()
        {
            Console.WriteLine("\n🔍 Running Performance Analysis...");
            Console.WriteLine("=" + new string('=', 49));

            var catalog = _serviceProvider!.GetRequiredService<IProductCatalog>();
            var inventory = _serviceProvider.GetRequiredService<IInventoryManager>();
            var processor = _serviceProvider.GetRequiredService<IOrderProcessor>();

            // Test 1: Product lookup performance
            await TestProductLookupPerformance(catalog);

            // Test 2: Search performance
            await TestSearchPerformance(catalog);

            // Test 3: Order processing performance
            await TestOrderProcessingPerformance(processor, catalog);

            // Test 4: Inventory operations performance
            await TestInventoryPerformance(inventory);

            // Test 5: Concurrent operations
            await TestConcurrentOperations(processor, catalog);

            Console.WriteLine("\n📈 Performance analysis completed!");
            Console.WriteLine("💡 Tip: Run with 'benchmark' argument for detailed BenchmarkDotNet analysis");
        }

        private static async Task TestProductLookupPerformance(IProductCatalog catalog)
        {
            Console.WriteLine("\n🔎 Testing product lookup performance...");

            var lookups = new List<int> { 1, 3, 5, 7, 9, 12, 15, 18 };
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 100; i++)
            {
                foreach (var productId in lookups)
                {
                    var product = catalog.GetProductById(productId);
                }
            }

            sw.Stop();
            Console.WriteLine($"   800 product lookups completed in {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"   Average: {(double)sw.ElapsedMilliseconds / 800:F2} ms per lookup");
        }

        private static async Task TestSearchPerformance(IProductCatalog catalog)
        {
            Console.WriteLine("\n🔍 Testing search performance...");

            var searchTerms = new[] { "phone", "laptop", "audio", "gaming", "apple" };
            var sw = Stopwatch.StartNew();

            foreach (var term in searchTerms)
            {
                var results = catalog.SearchProducts(term);
                Console.WriteLine($"   Search '{term}': {results.Count} results");
            }

            sw.Stop();
            Console.WriteLine($"   {searchTerms.Length} searches completed in {sw.ElapsedMilliseconds} ms");
        }

        private static async Task TestOrderProcessingPerformance(IOrderProcessor processor, IProductCatalog catalog)
        {
            Console.WriteLine("\n📋 Testing order processing performance...");

            var orders = CreateTestOrders(5);
            var sw = Stopwatch.StartNew();
            var successCount = 0;

            foreach (var order in orders)
            {
                try
                {
                    var result = await processor.ProcessOrderWithValidationAsync(order);
                    if (result.Success) successCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"   Order {order.OrderId} failed: {ex.Message}");
                }
            }

            sw.Stop();
            Console.WriteLine($"   {successCount}/{orders.Count} orders processed in {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"   Average: {(double)sw.ElapsedMilliseconds / orders.Count:F2} ms per order");
        }

        private static async Task TestInventoryPerformance(IInventoryManager inventory)
        {
            Console.WriteLine("\n📦 Testing inventory operations performance...");

            var sw = Stopwatch.StartNew();
            var lowStockProducts = inventory.GetLowStockProducts(50);
            sw.Stop();

            Console.WriteLine($"   Low stock check: {lowStockProducts.Count} products found in {sw.ElapsedMilliseconds} ms");
        }

        private static async Task TestConcurrentOperations(IOrderProcessor processor, IProductCatalog catalog)
        {
            Console.WriteLine("\n🔄 Testing concurrent operations...");

            var tasks = new List<Task>();
            var sw = Stopwatch.StartNew();

            // Simulate concurrent product lookups
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 1; j <= 20; j++)
                    {
                        catalog.GetProductById(j % 20 + 1);
                    }
                }));
            }

            await Task.WhenAll(tasks);
            sw.Stop();

            Console.WriteLine($"   10 concurrent tasks (200 total operations) completed in {sw.ElapsedMilliseconds} ms");
        }

        private static List<Order> CreateTestOrders(int count)
        {
            var orders = new List<Order>();
            var random = new Random(12345); // Fixed seed for consistent results

            for (int i = 0; i < count; i++)
            {
                var order = new Order($"customer{i}@example.com", $"{i + 100} Test Street, Test City, WA 98101");

                // Add random items to each order
                for (int j = 0; j < random.Next(1, 5); j++)
                {
                    var productId = random.Next(1, 21);
                    var quantity = random.Next(1, 4);
                    order.AddItem(new OrderItem(productId, quantity));
                }

                orders.Add(order);
            }

            return orders;
        }
    }
}
