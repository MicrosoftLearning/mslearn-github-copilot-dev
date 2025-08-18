namespace ECommerce.ApplicationCore.Entities;

/// <summary>
/// Represents an individual item within an order
/// </summary>
public class OrderItem
{
    public required string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? ProductName { get; set; }
    public string? Category { get; set; }
}
