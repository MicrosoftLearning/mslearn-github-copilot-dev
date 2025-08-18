using ECommerce.ApplicationCore.Entities;
using ECommerce.ApplicationCore.Services;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Console;

/// <summary>
/// Main entry point for the e-commerce order processing application
/// Demonstrates dependency injection and proper layered architecture
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("=== E-Commerce Order Processing System ===");
        System.Console.WriteLine("Starting order processing tests...\n");

        // Setup dependencies (In a real app, this would use DI container)
        var inventoryService = new InventoryService();
        var paymentGateway = new PaymentGateway();
        var shippingService = new ShippingService();
        var notificationService = new NotificationService();
        var securityValidator = new SecurityValidator();
        var auditLogger = new AuditLogger();

        // Create the main order processor with injected dependencies
        var processor = new OrderProcessor(
            inventoryService,
            paymentGateway,
            shippingService,
            notificationService,
            securityValidator,
            auditLogger);

        var testResults = new List<string>();

        // Test Case 1: Valid order with multiple items
        System.Console.WriteLine("--- Test Case 1: Valid Order ---");
        var validOrder = CreateSampleOrder("ORD-001", "john.doe@email.com", "123 Main St, City, State 12345", 
            new List<OrderItem> 
            { 
                new() { ProductId = "LAPTOP-001", Quantity = 1, Price = 999.99m },
                new() { ProductId = "MOUSE-001", Quantity = 2, Price = 25.00m }
            },
            new PaymentInfo 
            { 
                CardNumber = "4111111111111111", 
                CardCVV = "123", 
                CardHolderName = "Ane Pedersen",
                ExpiryMonth = "12",
                ExpiryYear = "2025",
                BillingAddress = "123 Main St, City, State 12345"
            });

        var result1 = processor.ProcessOrder(validOrder);
        testResults.Add($"Test 1: {(result1.IsSuccess ? "PASSED" : "FAILED")} - {(result1.IsSuccess ? result1.OrderId : result1.ErrorMessage)}");

        System.Console.WriteLine("\n--- Test Case 2: Invalid Email Address ---");
        var invalidEmailOrder = CreateSampleOrder("ORD-002", "invalid-email", "123 Main St", 
            new List<OrderItem> { new() { ProductId = "BOOK-001", Quantity = 1, Price = 15.99m } },
            new PaymentInfo { CardNumber = "4111111111111111", CardCVV = "123", CardHolderName = "Jennet Nazarowa", ExpiryMonth = "06", ExpiryYear = "2026", BillingAddress = "123 Main St" });

        var result2 = processor.ProcessOrder(invalidEmailOrder);
        testResults.Add($"Test 2: {(result2.IsSuccess ? "FAILED" : "PASSED")} - Should reject invalid email");

        System.Console.WriteLine("\n--- Test Case 3: Declined Payment ---");
        var declinedPaymentOrder = CreateSampleOrder("ORD-003", "customer@test.com", "456 Oak Ave", 
            new List<OrderItem> { new() { ProductId = "PHONE-001", Quantity = 1, Price = 699.99m } },
            new PaymentInfo { CardNumber = "0000000000000000", CardCVV = "999", CardHolderName = "Nikki Vestergaard", ExpiryMonth = "01", ExpiryYear = "2024", BillingAddress = "456 Oak Ave" });

        var result3 = processor.ProcessOrder(declinedPaymentOrder);
        testResults.Add($"Test 3: {(result3.IsSuccess ? "FAILED" : "PASSED")} - Should reject declined payment");

        System.Console.WriteLine("\n--- Test Case 4: Suspicious Order (Security Check) ---");
        var suspiciousOrder = CreateSampleOrder("ORD-004", "suspicious.user@valid.com", "123 Suspicious Ave, City, State 99999", 
            new List<OrderItem> { new() { ProductId = "EXPENSIVE-001", Quantity = 2, Price = 25000.00m } },
            new PaymentInfo { CardNumber = "4111111111111111", CardCVV = "123", CardHolderName = "AB", ExpiryMonth = "12", ExpiryYear = "2026", BillingAddress = "123 Suspicious Ave, City, State 99999" });

        var result4 = processor.ProcessOrder(suspiciousOrder);
        testResults.Add($"Test 4: {(result4.IsSuccess ? "FAILED" : "PASSED")} - Should flag suspicious order");

        // Display test summary
        System.Console.WriteLine("\n=== TEST SUMMARY ===");
        foreach (var testResult in testResults)
        {
            System.Console.WriteLine(testResult);
        }

        var passedTests = testResults.Count(r => r.Contains("PASSED"));
        System.Console.WriteLine($"\nTests Passed: {passedTests}/{testResults.Count}");
        System.Console.WriteLine("Order processing system test completed.");
    }

    private static Order CreateSampleOrder(string orderId, string email, string address, List<OrderItem> items, PaymentInfo payment)
    {
        var order = new Order
        {
            Id = orderId,
            CustomerEmail = email,
            ShippingAddress = address,
            Items = items,
            PaymentInfo = payment,
            Status = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow
        };
        order.TotalAmount = items.Sum(i => i.Price * i.Quantity) + 9.99m; // Add shipping
        return order;
    }
}
