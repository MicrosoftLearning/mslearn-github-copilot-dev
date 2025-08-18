using System.Text.RegularExpressions;
using ECommerce.ApplicationCore.Entities;
using ECommerce.ApplicationCore.Interfaces;

namespace ECommerce.Infrastructure.Services;

/// <summary>
/// Security validation service implementation with comprehensive input validation and risk assessment
/// </summary>
public class SecurityValidator : ISecurityValidator
{
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    private static readonly HashSet<string> SuspiciousDomains = new()
    {
        "temp.com", "tempmail.com", "10minutemail.com", "guerrillamail.com"
    };

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
            
        return EmailRegex.IsMatch(email) && !IsSuspiciousDomain(email);
    }
    
    private bool IsSuspiciousDomain(string email)
    {
        var domain = email.Split('@').LastOrDefault()?.ToLower();
        return domain != null && SuspiciousDomains.Contains(domain);
    }

    public bool IsValidShippingAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || address.Length < 10)
            return false;
            
        // Basic address validation - should contain numbers and letters
        return Regex.IsMatch(address, @"^\d+.*[a-zA-Z].*") && 
               !address.ToLower().Contains("unknown");
    }

    public bool IsValidPaymentInfo(PaymentInfo paymentInfo)
    {
        if (paymentInfo == null)
            return false;
            
        // Validate card number (basic length check)
        if (string.IsNullOrWhiteSpace(paymentInfo.CardNumber) || 
            paymentInfo.CardNumber.Length < 13 || paymentInfo.CardNumber.Length > 19)
            return false;
            
        // Validate CVV
        if (string.IsNullOrWhiteSpace(paymentInfo.CardCVV) || 
            !Regex.IsMatch(paymentInfo.CardCVV, @"^\d{3,4}$"))
            return false;
            
        // Validate cardholder name
        if (string.IsNullOrWhiteSpace(paymentInfo.CardHolderName) || 
            paymentInfo.CardHolderName.Trim().Length < 2)
            return false;
            
        // Validate expiry date
        if (!IsValidExpiryDate(paymentInfo.ExpiryMonth, paymentInfo.ExpiryYear))
            return false;
            
        return true;
    }
    
    private bool IsValidExpiryDate(string month, string year)
    {
        if (!int.TryParse(month, out int mm) || !int.TryParse(year, out int yy))
            return false;
            
        if (mm < 1 || mm > 12)
            return false;
            
        var expiry = new DateTime(yy, mm, DateTime.DaysInMonth(yy, mm));
        return expiry > DateTime.Now;
    }

    public int CalculateRiskScore(Order order)
    {
        int riskScore = 0;
        
        // High-value order risk
        if (order.TotalAmount > 5000)
            riskScore += 30;
        else if (order.TotalAmount > 2000)
            riskScore += 15;
            
        // Large quantity risk
        var totalQuantity = order.Items.Sum(i => i.Quantity);
        if (totalQuantity > 10)
            riskScore += 25;
        else if (totalQuantity > 5)
            riskScore += 10;
            
        // Suspicious email patterns
        if (IsSuspiciousDomain(order.CustomerEmail))
            riskScore += 40;
            
        // Short cardholder name (potential fake)
        if (order.PaymentInfo.CardHolderName.Trim().Length <= 2)
            riskScore += 30;
            
        // CVV all zeros (suspicious)
        if (order.PaymentInfo.CardCVV == "000")
            riskScore += 25;
            
        return riskScore;
    }

    public bool ValidateOrderAmounts(Order order)
    {
        if (order.TotalAmount <= 0)
            return false;
            
        decimal calculatedTotal = order.Items.Sum(i => i.Price * i.Quantity);
        
        // Allow for shipping, tax, etc. - total should be reasonable compared to item total
        return order.TotalAmount >= calculatedTotal && 
               order.TotalAmount <= calculatedTotal * 1.5m; // Max 50% markup for shipping/tax
    }

    public bool ValidateOrderItem(OrderItem item)
    {
        if (string.IsNullOrWhiteSpace(item.ProductId) || item.Quantity <= 0 || item.Price < 0)
            return false;
            
        // Reasonable quantity limits
        if (item.Quantity > 100)
            return false;
            
        // Reasonable price limits
        if (item.Price > 50000) // $50k max per item
            return false;
            
        return true;
    }

    public bool ValidatePaymentSecurity(PaymentInfo payment, decimal amount)
    {
        // Additional fraud detection logic
        if (payment.CardNumber.StartsWith("0000"))
            return false; // Test cards that should be declined
            
        if (amount > 10000 && payment.CardHolderName.Length < 5)
            return false; // High-value orders need realistic names
            
        return true;
    }

    public bool ValidateShippingRequirements(Order order)
    {
        // Validate shipping to known problematic addresses
        var address = order.ShippingAddress.ToLower();
        
        if (address.Contains("unknown") || address.Contains("invalid"))
            return false;
            
        return true;
    }

    public string MaskEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            return "***@***.***";
            
        var parts = email.Split('@');
        if (parts.Length != 2)
            return "***@***.***";
            
        var localPart = parts[0];
        var domainPart = parts[1];
        
        var maskedLocal = localPart.Length > 2 
            ? localPart[0] + "***" + localPart[^1]
            : "***";
            
        return $"{maskedLocal}@{domainPart}";
    }
}
