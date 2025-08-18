using ECommerce.ApplicationCore.Entities;

namespace ECommerce.ApplicationCore.Interfaces;

/// <summary>
/// Interface for shipping and logistics operations
/// </summary>
public interface IShippingService
{
    /// <summary>
    /// Schedule a shipment for an order
    /// </summary>
    /// <param name="order">Order to ship</param>
    /// <returns>Shipping details including tracking number</returns>
    ShippingDetails ScheduleShipment(Order order);
}
