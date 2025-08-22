using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Exceptions;
using ContosoOnlineStore.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace ContosoOnlineStore
{
    public interface IOrderProcessor
    {
        decimal CalculateOrderTotal(Order order);
        Task<decimal> FinalizeOrderAsync(Order order);
        Task<bool> ValidateOrderAsync(Order order);
        Task<string> GenerateOrderReceiptAsync(Order order);
        Task<OrderProcessingResult> ProcessOrderWithValidationAsync(Order order);
        decimal CalculateTax(decimal subtotal, string state = "WA");
        decimal CalculateShipping(Order order);
    }

    public class OrderProcessingResult
    {
        public bool Success { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ErrorMessage { get; set; }
        public List<string> Warnings { get; set; } = new();
        public TimeSpan ProcessingTime { get; set; }
        public int ProcessedItems { get; set; }
    }

    public class OrderProcessor : IOrderProcessor
    {
        private readonly IProductCatalog _catalog;
        private readonly IInventoryManager _inventory;
        private readonly IEmailService _emailService;
        private readonly ISecurityValidationService _securityValidation;
        private readonly ILogger<OrderProcessor> _logger;
        private readonly AppSettings _appSettings;
        private readonly ConcurrentDictionary<int, decimal> _priceCache;
        private static int _orderCounter = 0;

        public OrderProcessor(
            IProductCatalog catalog,
            IInventoryManager inventory,
            IEmailService emailService,
            ISecurityValidationService securityValidation,
            ILogger<OrderProcessor> logger,
            IOptions<AppSettings> appSettings)
        {
            _catalog = catalog;
            _inventory = inventory;
            _emailService = emailService;
            _securityValidation = securityValidation;
            _logger = logger;
            _appSettings = appSettings.Value;
            _priceCache = new ConcurrentDictionary<int, decimal>();
        }

        public decimal CalculateOrderTotal(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            decimal subtotal = 0;
            var productCache = new Dictionary<int, Product?>();

            foreach (OrderItem item in order.Items)
            {
                // Performance bottleneck: Individual product lookups instead of batch
                if (!productCache.ContainsKey(item.ProductId))
                {
                    Thread.Sleep(5); // Simulate database query delay
                    productCache[item.ProductId] = _catalog.GetProductById(item.ProductId);
                }

                var product = productCache[item.ProductId];
                if (product != null)
                {
                    // Security validation for each item
                    _securityValidation.ValidateOrderItem(item, product);

                    var itemTotal = product.Price * item.Quantity;
                    subtotal += itemTotal;

                    // Update order item with unit price for receipt
                    item.UnitPrice = product.Price;

                    _logger.LogDebug("Calculated item total for product {ProductId}: {ItemTotal:C}",
                        product.Id, itemTotal);
                }
                else
                {
                    _logger.LogWarning("Product {ProductId} not found in catalog", item.ProductId);
                    throw new ProductNotFoundException(item.ProductId);
                }
            }

            // Performance bottleneck: Recalculate tax and shipping every time
            var tax = CalculateTax(subtotal);
            var shipping = CalculateShipping(order);
            var total = subtotal + tax + shipping;

            _logger.LogInformation("Order total calculated: Subtotal={Subtotal:C}, Tax={Tax:C}, Shipping={Shipping:C}, Total={Total:C}",
                subtotal, tax, shipping, total);

            return total;
        }

        public async Task<decimal> FinalizeOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var startTime = DateTime.UtcNow;
            Interlocked.Increment(ref _orderCounter);

            try
            {
                _logger.LogInformation("Finalizing order {OrderId} (Processing #{ProcessingNumber})",
                    order.OrderId, _orderCounter);

                // Validate order first
                if (!await ValidateOrderAsync(order))
                {
                    throw new InvalidOrderException("Order validation failed");
                }

                // Reserve inventory
                _inventory.ReserveStock(order);

                // Calculate total with all fees
                decimal total = CalculateOrderTotal(order);
                order.TotalAmount = total;
                order.Status = OrderStatus.Processing;

                // Update actual inventory
                _inventory.UpdateStockLevels(order);

                // Send confirmation email
                bool emailSent = await _emailService.SendConfirmationAsync(order);
                if (!emailSent)
                {
                    _logger.LogWarning("Failed to send confirmation email for order {OrderId}", order.OrderId);
                }

                // Performance bottleneck: Generate receipt immediately (could be deferred)
                await GenerateOrderReceiptAsync(order);

                order.Status = OrderStatus.Shipped; // Simulate immediate shipping for demo

                var processingTime = DateTime.UtcNow - startTime;
                _logger.LogInformation("Order {OrderId} finalized successfully in {ProcessingTime}ms. Total: {Total:C}",
                    order.OrderId, processingTime.TotalMilliseconds, total);

                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to finalize order {OrderId}", order.OrderId);

                // Release reserved stock on failure
                try
                {
                    _inventory.ReleaseReservedStock(order);
                    order.Status = OrderStatus.Cancelled;
                }
                catch (Exception releaseEx)
                {
                    _logger.LogError(releaseEx, "Failed to release reserved stock for order {OrderId}", order.OrderId);
                }

                throw;
            }
        }

        public async Task<bool> ValidateOrderAsync(Order order)
        {
            if (order == null)
                return false;

            try
            {
                // Basic order validation
                _securityValidation.ValidateOrder(order);

                // Check inventory availability
                foreach (var item in order.Items)
                {
                    // Performance bottleneck: Individual inventory checks
                    await Task.Delay(10); // Simulate database query

                    if (!_inventory.IsInStock(item.ProductId, item.Quantity))
                    {
                        var availableStock = _inventory.GetStockLevel(item.ProductId);
                        _logger.LogWarning("Insufficient inventory for product {ProductId}. Requested: {Requested}, Available: {Available}",
                            item.ProductId, item.Quantity, availableStock);
                        return false;
                    }

                    // Validate product exists
                    var product = _catalog.GetProductById(item.ProductId);
                    if (product == null)
                    {
                        _logger.LogWarning("Product {ProductId} not found during validation", item.ProductId);
                        return false;
                    }

                    // Validate item against product
                    _securityValidation.ValidateOrderItem(item, product);
                }

                // Validate email format
                if (!await _emailService.ValidateEmailAsync(order.CustomerEmail))
                {
                    _logger.LogWarning("Invalid email format for order {OrderId}: {Email}", order.OrderId, order.CustomerEmail);
                    return false;
                }

                _logger.LogDebug("Order {OrderId} validation completed successfully", order.OrderId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Order validation failed for order {OrderId}", order.OrderId);
                return false;
            }
        }

        public async Task<string> GenerateOrderReceiptAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            // Performance bottleneck: Generate receipt synchronously with delays
            await Task.Delay(100); // Simulate receipt generation delay

            var receiptBuilder = new System.Text.StringBuilder();
            receiptBuilder.AppendLine("=".PadRight(50, '='));
            receiptBuilder.AppendLine("CONTOSO ONLINE STORE");
            receiptBuilder.AppendLine("Order Receipt");
            receiptBuilder.AppendLine("=".PadRight(50, '='));
            receiptBuilder.AppendLine($"Order ID: {order.OrderId}");
            receiptBuilder.AppendLine($"Date: {order.OrderDate:yyyy-MM-dd HH:mm:ss}");
            receiptBuilder.AppendLine($"Customer: {order.CustomerEmail}");
            receiptBuilder.AppendLine($"Shipping: {order.ShippingAddress}");
            receiptBuilder.AppendLine("-".PadRight(50, '-'));

            decimal subtotal = 0;
            foreach (var item in order.Items)
            {
                // Performance bottleneck: Individual product lookups for receipt
                await Task.Delay(5);
                var product = _catalog.GetProductById(item.ProductId);
                if (product != null)
                {
                    var itemTotal = product.Price * item.Quantity;
                    subtotal += itemTotal;
                    receiptBuilder.AppendLine($"{product.Name.PadRight(20)} {item.Quantity}x {product.Price:C} = {itemTotal:C}");
                }
            }

            var tax = CalculateTax(subtotal);
            var shipping = CalculateShipping(order);
            var total = subtotal + tax + shipping;

            receiptBuilder.AppendLine("-".PadRight(50, '-'));
            receiptBuilder.AppendLine($"Subtotal: {subtotal:C}".PadLeft(50));
            receiptBuilder.AppendLine($"Tax: {tax:C}".PadLeft(50));
            receiptBuilder.AppendLine($"Shipping: {shipping:C}".PadLeft(50));
            receiptBuilder.AppendLine($"TOTAL: {total:C}".PadLeft(50));
            receiptBuilder.AppendLine("=".PadRight(50, '='));

            var receipt = receiptBuilder.ToString();
            _logger.LogDebug("Generated receipt for order {OrderId}", order.OrderId);

            return receipt;
        }

        public async Task<OrderProcessingResult> ProcessOrderWithValidationAsync(Order order)
        {
            var startTime = DateTime.UtcNow;
            var result = new OrderProcessingResult();

            try
            {
                if (order == null)
                {
                    result.ErrorMessage = "Order cannot be null";
                    return result;
                }

                // Comprehensive validation
                if (!await ValidateOrderAsync(order))
                {
                    result.ErrorMessage = "Order validation failed";
                    return result;
                }

                // Process the order
                result.TotalAmount = await FinalizeOrderAsync(order);
                result.ProcessedItems = order.Items.Count;
                result.Success = true;

                // Check for warnings (low stock items)
                await CheckForLowStockWarningsAsync(order, result);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                _logger.LogError(ex, "Order processing failed for order {OrderId}", order?.OrderId);
            }
            finally
            {
                result.ProcessingTime = DateTime.UtcNow - startTime;
            }

            return result;
        }

        public decimal CalculateTax(decimal subtotal, string state = "WA")
        {
            // Performance bottleneck: Inefficient tax calculation with lookup
            Thread.Sleep(20); // Simulate tax service lookup delay

            var taxRates = new Dictionary<string, decimal>
            {
                ["WA"] = 0.095m,
                ["CA"] = 0.0875m,
                ["NY"] = 0.08m,
                ["TX"] = 0.0825m,
                ["FL"] = 0.06m
            };

            var rate = taxRates.GetValueOrDefault(state.ToUpper(), 0.05m);
            return Math.Round(subtotal * rate, 2);
        }

        public decimal CalculateShipping(Order order)
        {
            if (order == null)
                return 0;

            // Performance bottleneck: Complex shipping calculation
            Thread.Sleep(15); // Simulate shipping calculator delay

            var itemCount = order.GetTotalItemCount();
            var baseShipping = 5.99m;
            var perItemFee = 0.99m;

            var shippingCost = baseShipping + (perItemFee * Math.Max(0, itemCount - 1));

            // Free shipping for orders over $100
            if (order.TotalAmount > 100)
            {
                shippingCost = 0;
            }

            return Math.Round(shippingCost, 2);
        }

        private async Task CheckForLowStockWarningsAsync(Order order, OrderProcessingResult result)
        {
            foreach (var item in order.Items)
            {
                await Task.Delay(5); // Performance bottleneck
                var remainingStock = _inventory.GetStockLevel(item.ProductId);
                if (remainingStock <= 10) // Low stock threshold
                {
                    var product = _catalog.GetProductById(item.ProductId);
                    result.Warnings.Add($"Low stock warning: {product?.Name} has only {remainingStock} units remaining");
                }
            }
        }
    }
}
