using ECommerce.ApplicationCore.Entities;
using ECommerce.ApplicationCore.Exceptions;
using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Services;

namespace ECommerce.Infrastructure.Services;

/// <summary>
/// Inventory service implementation with realistic stock management
/// </summary>
public class InventoryService : IInventoryService
{
    private static readonly Dictionary<string, int> StockLevels = new()
    {
        { "LAPTOP-001", 5 },
        { "MOUSE-001", 25 },
        { "BOOK-001", 100 },
        { "PHONE-001", 3 },
        { "EXPENSIVE-001", 2 }
    };
    
    public bool CheckStock(string productId, int quantity)
    {
        // Simulate database/API call delay
        Thread.Sleep(50);
        
        if (!StockLevels.TryGetValue(productId, out int availableStock))
        {
            // Unknown product - assume out of stock
            return false;
        }
        
        return availableStock >= quantity;
    }
    
    public bool ReserveStock(List<OrderItem> items)
    {
        Thread.Sleep(100);
        
        // In a real system, this would be an atomic transaction
        foreach (var item in items)
        {
            if (!StockLevels.TryGetValue(item.ProductId, out int currentStock))
                return false;
                
            if (currentStock < item.Quantity)
                return false;
                
            StockLevels[item.ProductId] = currentStock - item.Quantity;
        }
        
        return true;
    }
    
    public void ReleaseStock(List<OrderItem> items)
    {
        Thread.Sleep(50);
        
        // Return items to stock
        foreach (var item in items)
        {
            if (StockLevels.ContainsKey(item.ProductId))
            {
                StockLevels[item.ProductId] += item.Quantity;
            }
        }
    }
}

/// <summary>
/// Payment gateway implementation with enhanced validation and fraud detection
/// </summary>
public class PaymentGateway : IPaymentGateway
{
    public string Charge(PaymentInfo payment, decimal amount)
    {
        // Simulate processing time
        Thread.Sleep(200);
        
        // Enhanced validation and fraud detection
        if (string.IsNullOrEmpty(payment.CardNumber) || payment.CardNumber.Length < 13)
        {
            throw new PaymentException("Invalid card number format", "INVALID_CARD");
        }
        
        if (payment.CardNumber.StartsWith("0000"))
        {
            throw new PaymentException("Card declined by issuer", "CARD_DECLINED");
        }
        
        if (amount > 10000 && payment.CardHolderName.Length < 3)
        {
            throw new PaymentException("High-value transaction requires valid cardholder name", "FRAUD_DETECTED");
        }
        
        // Simulate different card types and responses
        if (payment.CardNumber.StartsWith("4111"))
        {
            // Valid Visa test card
            return $"PAY_{DateTime.UtcNow:yyyyMMddHHmmss}_{Random.Shared.Next(1000, 9999)}";
        }
        
        if (payment.CardNumber.StartsWith("5555"))
        {
            // Valid MasterCard test card
            return $"PAY_MC_{DateTime.UtcNow:yyyyMMddHHmmss}_{Random.Shared.Next(1000, 9999)}";
        }
        
        // Generic successful payment
        return $"PAY_GENERIC_{DateTime.UtcNow:yyyyMMddHHmmss}_{Random.Shared.Next(1000, 9999)}";
    }
}

/// <summary>
/// Shipping service implementation with realistic logistics management
/// </summary>
public class ShippingService : IShippingService
{
    public ShippingDetails ScheduleShipment(Order order)
    {
        Thread.Sleep(150);
        
        if (string.IsNullOrWhiteSpace(order.ShippingAddress))
        {
            throw new Exception("Invalid shipping address provided");
        }
        
        if (order.ShippingAddress.ToLower().Contains("invalid"))
        {
            throw new Exception("Cannot ship to specified address");
        }
        
        // Generate tracking number and estimated delivery
        var trackingNumber = $"TRK{DateTime.UtcNow:yyyyMMdd}{Random.Shared.Next(100000, 999999)}";
        var estimatedDelivery = DateTime.UtcNow.AddDays(Random.Shared.Next(3, 8));
        
        return new ShippingDetails
        {
            TrackingNumber = trackingNumber,
            EstimatedDelivery = estimatedDelivery,
            ShippingMethod = DetermineShippingMethod(order.TotalAmount),
            ShippingCost = CalculateShippingCost(order)
        };
    }
    
    private string DetermineShippingMethod(decimal orderValue)
    {
        return orderValue > 1000 ? "Express Shipping" : "Standard Shipping";
    }
    
    private decimal CalculateShippingCost(Order order)
    {
        // Simple shipping cost calculation
        var baseCost = 9.99m;
        var itemCount = order.Items.Sum(i => i.Quantity);
        
        if (order.TotalAmount > 100)
            baseCost = 0; // Free shipping over $100
        else if (itemCount > 3)
            baseCost += 5.00m; // Extra charge for multiple items
            
        return baseCost;
    }
}

/// <summary>
/// Notification service implementation for customer communications
/// </summary>
public class NotificationService : INotificationService
{
    public void SendOrderConfirmation(string email, string orderId, string? trackingNumber = null)
    {
        Thread.Sleep(100);
        
        // Basic email validation
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            throw new Exception("Invalid email address for notification");
        }
        
        // Simulate email sending
        var message = $"Order {orderId} confirmed.";
        if (!string.IsNullOrEmpty(trackingNumber))
        {
            message += $" Tracking: {trackingNumber}";
        }
        
        Console.WriteLine($"[EMAIL] Sent to {MaskEmail(email)}: {message}");
    }
    
    public void SendHighValueOrderAlert(Order order)
    {
        Thread.Sleep(50);
        
        // Internal notification for high-value orders
        Console.WriteLine($"[ALERT] High-value order {order.Id} processed: ${order.TotalAmount:F2}");
    }
    
    private string MaskEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            return "***@***.***";
            
        var parts = email.Split('@');
        return parts.Length == 2 
            ? $"{parts[0][0]}***@{parts[1]}"
            : "***@***.***";
    }
}
