namespace ECommerce.ApplicationCore.Entities;

/// <summary>
/// Contains payment information for order processing
/// </summary>
public class PaymentInfo
{
    public required string CardNumber { get; set; }
    public required string CardCVV { get; set; }
    public required string CardHolderName { get; set; }
    public required string ExpiryMonth { get; set; }
    public required string ExpiryYear { get; set; }
    public required string BillingAddress { get; set; }
    
    /// <summary>
    /// Security helper to get masked card number for logging
    /// </summary>
    /// <returns>Masked card number with only last 4 digits visible</returns>
    public string GetMaskedCardNumber()
    {
        if (string.IsNullOrEmpty(CardNumber) || CardNumber.Length < 4)
            return "****";
        return "****-****-****-" + CardNumber.Substring(CardNumber.Length - 4);
    }
}
