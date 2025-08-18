using ECommerce.ApplicationCore.Entities;

namespace ECommerce.ApplicationCore.Interfaces;

/// <summary>
/// Interface for customer notification operations
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Send order confirmation to customer
    /// </summary>
    void SendOrderConfirmation(string email, string orderId, string? trackingNumber = null);
    
    /// <summary>
    /// Send high-value order alert to internal systems
    /// </summary>
    void SendHighValueOrderAlert(Order order);
}
