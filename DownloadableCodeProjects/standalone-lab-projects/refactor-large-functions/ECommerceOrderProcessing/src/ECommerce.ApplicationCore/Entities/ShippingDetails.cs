namespace ECommerce.ApplicationCore.Entities;

/// <summary>
/// Contains shipping details for an order
/// </summary>
public class ShippingDetails
{
    public required string TrackingNumber { get; set; }
    public DateTime EstimatedDelivery { get; set; }
    public string ShippingMethod { get; set; } = string.Empty;
    public decimal ShippingCost { get; set; }
}
