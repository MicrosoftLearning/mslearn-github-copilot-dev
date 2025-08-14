// OrderProcessor.cs – Processes customer orders.
using System;
using EcommerceApp.Models;
using EcommerceApp.Security;
using EcommerceApp.Configuration;
using EcommerceApp.Services;

namespace EcommerceApp;

public class OrderProcessor
{
    public void ProcessOrder(string orderId)
    {
        try
        {
            Console.WriteLine($"=== Starting Order Processing ===");
            Console.WriteLine($"Processing order: {orderId}");
            
            // Before proceeding, ensure the order ID is valid.
            if (Validate(orderId))
            {
                // Get order details (simulated)
                var order = GetOrderDetails(orderId);
                
                // Audit the start of order processing
                AuditService.LogOrderActivity(order, "PROCESSING_STARTED", "Order validation passed");
                
                // Calculate shipping costs based on order details
                decimal shippingCost = CalculateShipping(orderId, order);
                Console.WriteLine($"Order {orderId} shipping cost: ${shippingCost:F2}");
                
                // Update order status
                order.Status = OrderStatus.Processing;
                Console.WriteLine($"Order {orderId} status updated to: {order.Status}");
                
                // Additional order processing logic
                ProcessPayment(order);
                
                // Handle inventory reservation
                InventoryService.ReserveOrderInventory(order);
                
                // Send confirmation email
                EmailService.SendOrderConfirmation(order);
                
                // Final audit log
                AuditService.LogOrderActivity(order, "PROCESSING_COMPLETED", $"Total amount: ${order.TotalAmount:F2}, Shipping: ${shippingCost:F2}");
                
                Console.WriteLine($"Order {orderId} processed successfully!");
            }
            else
            {
                Console.WriteLine($"Order {orderId} is invalid and cannot be processed.");
                throw new ArgumentException($"Invalid order ID: {orderId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing order {orderId}: {ex.Message}");
            // In a real application, this would be logged to a proper logging system
        }
        finally
        {
            Console.WriteLine($"=== Order Processing Complete ===\n");
        }
    }

    // Validation logic
    private bool Validate(string orderId)
    {
        Console.WriteLine("[VALIDATION] Validating order ID...");

        // Check for null or whitespace
        if (string.IsNullOrWhiteSpace(orderId))
        {
            Console.WriteLine("[VALIDATION] Order ID cannot be empty.");
            return false;
        }

        // Security validation
        if (!SecurityValidator.IsValidId(orderId, "Order"))
        {
            Console.WriteLine("[VALIDATION] Order ID failed security validation.");
            return false;
        }

        // Length validation (evolved logic)
        if (orderId.Length < AppConfig.MinIdLength || orderId.Length > AppConfig.MaxIdLength)
        {
            Console.WriteLine("[VALIDATION] Order ID length is invalid.");
            return false;
        }

        // Additional prefix validation (evolutionary change)
        if (!orderId.StartsWith("ORD"))
        {
            Console.WriteLine("[VALIDATION] Order ID must start with 'ORD'.");
            return false;
        }

        return true;
    }

    private decimal CalculateShipping(string orderId, Order order)
    {
        Console.WriteLine("[SHIPPING] Calculating shipping cost...");

        // Base shipping cost
        decimal shippingCost = 5.00m;

        // Weight-based adjustment (evolved logic)
        if (order.TotalWeight > 10)
        {
            shippingCost += 2.00m;
        }

        // Value-based adjustment (evolved logic)
        if (order.TotalAmount > 50)
        {
            shippingCost -= 1.00m; // Discount for high-value orders
        }

        // Additional handling fee for fragile items (evolutionary change)
        if (order.ContainsFragileItems)
        {
            shippingCost += 3.00m;
        }

        return shippingCost;
    }

    private Order GetOrderDetails(string orderId)
    {
        // Simulate retrieving order from database
        return new Order
        {
            OrderId = orderId,
            CustomerId = "CUST001",
            OrderDate = DateTime.Now.AddDays(-1),
            TotalAmount = 75.50m,
            ShippingAddress = "123 Main St, Anytown, ST 12345",
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>
            {
                new OrderItem { ProductId = "PROD001", ProductName = "Laptop", Quantity = 1, UnitPrice = 699.99m, Weight = 4.5m },
                new OrderItem { ProductId = "PROD002", ProductName = "Mouse", Quantity = 2, UnitPrice = 25.99m, Weight = 0.2m }
            }
        };
    }

    private void ProcessPayment(Order order)
    {
        Console.WriteLine($"Processing payment for order {order.OrderId}, amount: ${order.TotalAmount:F2}");
        
        // Audit payment processing start
        AuditService.LogOrderActivity(order, "PAYMENT_STARTED", $"Amount: ${order.TotalAmount:F2}");
        
        // Simulate payment processing
        System.Threading.Thread.Sleep(100); // Simulate processing time
        Console.WriteLine("Payment processed successfully");
        
        // Audit payment completion
        AuditService.LogOrderActivity(order, "PAYMENT_COMPLETED", "Payment successful");
    }
}
