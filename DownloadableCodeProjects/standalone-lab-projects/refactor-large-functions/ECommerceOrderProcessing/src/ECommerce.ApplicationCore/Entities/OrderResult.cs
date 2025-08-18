namespace ECommerce.ApplicationCore.Entities;

/// <summary>
/// Represents the result of order processing operations
/// </summary>
public class OrderResult
{
    public bool IsSuccess { get; private set; }
    public string OrderId { get; private set; } = string.Empty;
    public string TrackingNumber { get; private set; } = string.Empty;
    public string ErrorMessage { get; private set; } = string.Empty;
    
    private OrderResult(bool success, string orderId = "", string trackingNumber = "", string error = "")
    {
        IsSuccess = success;
        OrderId = orderId;
        TrackingNumber = trackingNumber;
        ErrorMessage = error;
    }
    
    public static OrderResult Success(string id, string trackingNumber = "") => 
        new(true, orderId: id, trackingNumber: trackingNumber);
    
    public static OrderResult Failure(string error) => 
        new(false, error: error);
}
