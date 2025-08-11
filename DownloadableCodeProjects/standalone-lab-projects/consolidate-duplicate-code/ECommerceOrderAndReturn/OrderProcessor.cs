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
    private bool Validate(string id)
    {
        Console.WriteLine($"Validating order ID: {id}");
        
        // Basic validation
        if (string.IsNullOrWhiteSpace(id))
        {
            Console.WriteLine("Validation failed: ID is null or empty");
            return false;
        }

        // Security validation
        if (!SecurityValidator.IsValidId(id, "Order"))
        {
            Console.WriteLine("Validation failed: Security check failed");
            return false;
        }

        // Business rule validation
        if (id.Length < AppConfig.MinIdLength || id.Length > AppConfig.MaxIdLength)
        {
            Console.WriteLine("Validation failed: ID length is invalid");
            return false;
        }

        Console.WriteLine("Order ID validation successful");
        return true;
    }

    private decimal CalculateShipping(string id, Order order)
    {
        Console.WriteLine($"Calculating shipping for order ID: {id}");
        
        decimal shippingCost = AppConfig.BaseShippingRate;
        
        // Calculate weight-based shipping
        decimal totalWeight = 0;
        foreach (var item in order.Items)
        {
            totalWeight += item.Weight * item.Quantity;
        }
        
        if (totalWeight > 0)
        {
            shippingCost += totalWeight * AppConfig.WeightBasedRatePerPound;
        }
        
        // Free shipping for orders over threshold
        if (order.TotalAmount >= AppConfig.FreeShippingThreshold)
        {
            Console.WriteLine($"Free shipping applied - order total: ${order.TotalAmount:F2}");
            shippingCost = 0;
        }
        
        Console.WriteLine($"Shipping calculation: Base=${AppConfig.BaseShippingRate:F2}, Weight({totalWeight}lbs)=${totalWeight * AppConfig.WeightBasedRatePerPound:F2}, Total=${shippingCost:F2}");
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
