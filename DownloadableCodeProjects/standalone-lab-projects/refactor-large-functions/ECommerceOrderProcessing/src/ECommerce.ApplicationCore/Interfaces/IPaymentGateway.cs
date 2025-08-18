using ECommerce.ApplicationCore.Entities;

namespace ECommerce.ApplicationCore.Interfaces;

/// <summary>
/// Interface for payment processing operations
/// </summary>
public interface IPaymentGateway
{
    /// <summary>
    /// Process a payment charge
    /// </summary>
    /// <param name="payment">Payment information</param>
    /// <param name="amount">Amount to charge</param>
    /// <returns>Payment reference ID</returns>
    string Charge(PaymentInfo payment, decimal amount);
}
