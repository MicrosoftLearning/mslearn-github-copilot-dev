namespace ECommerce.ApplicationCore.Entities;

/// <summary>
/// Represents the various states an order can be in
/// </summary>
public enum OrderStatus
{
    Pending,
    Processing,
    PaymentConfirmed,
    Shipped,
    Delivered,
    Completed,
    Cancelled,
    Refunded
}
