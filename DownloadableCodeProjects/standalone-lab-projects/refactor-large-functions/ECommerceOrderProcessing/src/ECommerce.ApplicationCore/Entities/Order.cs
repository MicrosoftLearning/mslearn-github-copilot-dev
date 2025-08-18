namespace ECommerce.ApplicationCore.Entities;

/// <summary>
/// Represents an e-commerce order with all associated data
/// </summary>
public class Order
{
    public required string Id { get; set; }
    public required List<OrderItem> Items { get; set; }
    public required string CustomerEmail { get; set; }
    public required string ShippingAddress { get; set; }
    public required PaymentInfo PaymentInfo { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? CompletionDate { get; set; }
    public TimeSpan ProcessingDuration { get; set; }
    public string? PaymentReference { get; set; }
    public string? TrackingNumber { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
}
