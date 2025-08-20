using System;

namespace ECommerce.ApplicationCore.Exceptions;

/// <summary>
/// Custom exception for payment processing errors
/// </summary>
public class PaymentException : Exception
{
    public string PaymentErrorCode { get; }

    public PaymentException(string message, string errorCode = "UNKNOWN") : base(message)
    {
        PaymentErrorCode = errorCode;
    }
}
