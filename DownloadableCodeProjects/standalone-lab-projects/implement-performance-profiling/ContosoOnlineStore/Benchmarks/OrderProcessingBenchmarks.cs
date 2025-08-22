using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ContosoOnlineStore;
using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ContosoOnlineStore.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob]
    public class OrderProcessingBenchmarks
    {
        private IOrderProcessor? _orderProcessor;
        private IProductCatalog? _catalog;
        private IInventoryManager? _inventory;
        private Order? _testOrder;
        private ServiceProvider? _serviceProvider;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();

            // Configure logging
            services.AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Warning));

            // Configure settings
            var appSettings = new AppSettings();
            services.AddSingleton(Options.Create(appSettings));

            // Register services
            services.AddSingleton<ISecurityValidationService, SecurityValidationService>();
            services.AddSingleton<IProductCatalog, ProductCatalog>();
            services.AddSingleton<IInventoryManager, InventoryManager>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IOrderProcessor, OrderProcessor>();

            _serviceProvider = services.BuildServiceProvider();

            _catalog = _serviceProvider.GetRequiredService<IProductCatalog>();
            _inventory = _serviceProvider.GetRequiredService<IInventoryManager>();
            _orderProcessor = _serviceProvider.GetRequiredService<IOrderProcessor>();

            // Create test order
            _testOrder = new Order("customer@example.com", "123 Test Street, Test City, WA 98101");
            _testOrder.AddItem(new OrderItem(1, 2));
            _testOrder.AddItem(new OrderItem(5, 1));
            _testOrder.AddItem(new OrderItem(10, 3));
        }

        [Benchmark]
        public decimal CalculateOrderTotal()
        {
            return _orderProcessor!.CalculateOrderTotal(_testOrder!);
        }

        [Benchmark]
        public async Task<bool> ValidateOrderAsync()
        {
            return await _orderProcessor!.ValidateOrderAsync(_testOrder!);
        }

        [Benchmark]
        public List<Product> GetAllProducts()
        {
            return _catalog!.GetAllProducts();
        }

        [Benchmark]
        public Product? GetProductById()
        {
            return _catalog!.GetProductById(5);
        }

        [Benchmark]
        public List<Product> SearchProducts()
        {
            return _catalog!.SearchProducts("phone");
        }

        [Benchmark]
        public Dictionary<int, int> GetLowStockProducts()
        {
            return _inventory!.GetLowStockProducts(50);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            (_serviceProvider as IDisposable)?.Dispose();
        }
    }

    public class BenchmarkRunner
    {
        public static void RunBenchmarks()
        {
            Console.WriteLine("Starting performance benchmarks...");
            var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<OrderProcessingBenchmarks>();
            Console.WriteLine("Benchmarks completed!");
        }
    }
}
