using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineStore;

// Main order processing logic with a large method
public class OrderProcessor
{
    // Simulated external services (stubs for inventory, payment, shipping, notification)
    private static InventoryService InventoryService = new InventoryService();
    private static PaymentGateway PaymentGateway = new PaymentGateway();
    private static ShippingService ShippingService = new ShippingService();
    private static NotificationService NotificationService = new NotificationService();

    public OrderResult ProcessOrder(Order order)
    {
        // Step 1: Validate Order Details (basic checks on input)
        if (order == null)
        {
            return OrderResult.Failure("No order provided");
        }
        if (order.Items == null || order.Items.Count == 0)
        {
            return OrderResult.Failure("Order has no items");
        }
        if (string.IsNullOrWhiteSpace(order.ShippingAddress) || string.IsNullOrWhiteSpace(order.CustomerEmail))
        {
            return OrderResult.Failure("Missing shipping address or customer email");
        }
        if (order.PaymentInfo == null || string.IsNullOrWhiteSpace(order.PaymentInfo.CardNumber))
        {
            return OrderResult.Failure("Payment information is incomplete");
        }
        Console.WriteLine($"Processing Order {order.Id} for {order.CustomerEmail}...");

        // Step 2: Check and Reserve Inventory
        foreach (OrderItem item in order.Items)
        {
            bool inStock = InventoryService.CheckStock(item.ProductId, item.Quantity);
            if (!inStock)
            {
                Console.WriteLine($"Item {item.ProductId} is out of stock. Aborting order.");
                return OrderResult.Failure($"Item {item.ProductId} is out of stock");
            }
        }
        bool inventoryReserved = InventoryService.ReserveStock(order.Items);
        if (!inventoryReserved)
        {
            Console.WriteLine("Failed to reserve inventory for the order.");
            return OrderResult.Failure("Inventory reservation failed");
        }
        Console.WriteLine("Inventory reserved successfully.");

        // Step 3: Process Payment
        try
        {
            // Attempt to charge the customer's card
            PaymentGateway.Charge(order.PaymentInfo, order.TotalAmount);
            Console.WriteLine("Payment processed successfully.");
        }
        catch (PaymentException ex)
        {
            // If payment fails, release reserved stock and return failure
            InventoryService.ReleaseStock(order.Items);
            Console.WriteLine($"Payment failed for Order {order.Id}: {ex.Message}");
            return OrderResult.Failure("Payment processing failed");
        }

        // Step 4: Schedule Shipping
        try
        {
            ShippingService.ScheduleShipment(order);
            Console.WriteLine("Shipping scheduled successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scheduling shipment: {ex.Message}");
            return OrderResult.Failure("Shipping scheduling failed");
        }

        // Step 5: Send Confirmation
        try
        {
            NotificationService.SendOrderConfirmation(order.CustomerEmail, order.Id);
            Console.WriteLine($"Confirmation sent to {order.CustomerEmail}.");
        }
        catch (Exception ex)
        {
            // If confirmation fails, log a warning but do not abort the order
            Console.WriteLine($"Warning: failed to send confirmation email: {ex.Message}");
        }

        // Step 6: Finalize Order
        order.Status = OrderStatus.Completed;
        // (In a real app, update the order record in a database here)
        Console.WriteLine($"Order {order.Id} is completed and closed out in the system.");

        return OrderResult.Success(order.Id);
    }
}

// Supporting data classes and result type
public class Order
{
    public string Id { get; set; }
    public List<OrderItem> Items { get; set; }
    public string CustomerEmail { get; set; }
    public string ShippingAddress { get; set; }
    public PaymentInfo PaymentInfo { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}
public class OrderItem
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    // Price, etc., could be added for a full implementation
}
public class PaymentInfo
{
    public string CardNumber { get; set; }
    public string CardCVV { get; set; }
    public string CardHolderName { get; set; }
    public string BillingAddress { get; set; }
    // Expiry date, etc., could be included
}
public enum OrderStatus
{
    Pending, Completed, Cancelled
}
public class OrderResult
{
    public bool IsSuccess { get; private set; }
    public string OrderId { get; private set; }
    public string ErrorMessage { get; private set; }
    private OrderResult(bool success, string orderId = null, string error = null)
    {
        IsSuccess = success; OrderId = orderId; ErrorMessage = error;
    }
    public static OrderResult Success(string id) => new OrderResult(true, orderId: id);
    public static OrderResult Failure(string error) => new OrderResult(false, error: error);
}

// Custom exception for payment errors
public class PaymentException : Exception
{
    public PaymentException(string message) : base(message) { }
}
// Simulated external services:
public class InventoryService
{
    // In a real app, this would check a database or API
    public bool CheckStock(string productId, int quantity)
    {
        // Fake logic: let's assume everything is in stock for this demo
        return true;
    }
    public bool ReserveStock(List<OrderItem> items)
    {
        // Fake reservation always succeeds (would actually decrement stock)
        return true;
    }
    public void ReleaseStock(List<OrderItem> items)
    {
        // Would add the items back to stock in a real system
    }
}
public class PaymentGateway
{
    public void Charge(PaymentInfo payment, decimal amount)
    {
        // Very basic simulation: throw if card data looks invalid or a specific test card
        if (string.IsNullOrEmpty(payment.CardNumber) || payment.CardNumber.Length < 44)
        {
            throw new PaymentException("Invalid card data");
        }
        if (payment.CardNumber.StartsWith("0000"))
        {
            // Simulate a card decline for test purposes
            throw new PaymentException("Card declined");
        }
        // Otherwise, pretend the charge succeeded
    }
}
public class ShippingService
{
    public void ScheduleShipment(Order order)
    {
        // Dummy implementation: in reality would call a shipping API
        if (string.IsNullOrWhiteSpace(order.ShippingAddress))
        {
            throw new Exception("Invalid shipping address");
        }
        // Simulate success (no return value needed)
    }
}
public class NotificationService
{
    public void SendOrderConfirmation(string email, string orderId)
    {
        // Dummy implementation: in reality would send an email
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            throw new Exception("Invalid email address");
        }
        // Simulate success (e.g., email sent)
    }
}

