using ECommerce.ApplicationCore.Entities;

namespace ECommerce.ApplicationCore.Interfaces;

/// <summary>
/// Interface for security validation and risk assessment
/// </summary>
public interface ISecurityValidator
{
    /// <summary>
    /// Validate email address format and domain
    /// </summary>
    bool IsValidEmail(string email);
    
    /// <summary>
    /// Validate shipping address format
    /// </summary>
    bool IsValidShippingAddress(string address);
    
    /// <summary>
    /// Validate payment information
    /// </summary>
    bool IsValidPaymentInfo(PaymentInfo paymentInfo);
    
    /// <summary>
    /// Calculate risk score for an order
    /// </summary>
    int CalculateRiskScore(Order order);
    
    /// <summary>
    /// Validate order amounts and pricing
    /// </summary>
    bool ValidateOrderAmounts(Order order);
    
    /// <summary>
    /// Validate individual order item
    /// </summary>
    bool ValidateOrderItem(OrderItem item);
    
    /// <summary>
    /// Validate payment security
    /// </summary>
    bool ValidatePaymentSecurity(PaymentInfo payment, decimal amount);
    
    /// <summary>
    /// Validate shipping requirements
    /// </summary>
    bool ValidateShippingRequirements(Order order);
    
    /// <summary>
    /// Mask email address for logging
    /// </summary>
    string MaskEmail(string email);
}
