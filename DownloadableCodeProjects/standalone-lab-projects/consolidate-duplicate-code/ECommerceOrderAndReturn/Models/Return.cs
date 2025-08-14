// Return.cs - Represents a product return
using System;

namespace EcommerceApp.Models;

public class Return
{
    public string ReturnId { get; set; } = string.Empty;
    public string OriginalOrderId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public DateTime ReturnDate { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal RefundAmount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public ReturnStatus Status { get; set; }
    public decimal Weight { get; set; } // in pounds, for return shipping calculation
    public double TotalWeight { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsOversized { get; set; }
}

public enum ReturnStatus
{
    Pending,
    Approved,
    Rejected,
    Processing,
    Completed
}
