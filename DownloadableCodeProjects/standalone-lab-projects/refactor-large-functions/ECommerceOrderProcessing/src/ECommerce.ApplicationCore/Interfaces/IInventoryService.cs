using ECommerce.ApplicationCore.Entities;

namespace ECommerce.ApplicationCore.Interfaces;

/// <summary>
/// Interface for inventory management operations
/// </summary>
public interface IInventoryService
{
    /// <summary>
    /// Check if sufficient stock is available for a product
    /// </summary>
    bool CheckStock(string productId, int quantity);
    
    /// <summary>
    /// Reserve stock for the items in an order
    /// </summary>
    bool ReserveStock(List<OrderItem> items);
    
    /// <summary>
    /// Release previously reserved stock
    /// </summary>
    void ReleaseStock(List<OrderItem> items);
}
