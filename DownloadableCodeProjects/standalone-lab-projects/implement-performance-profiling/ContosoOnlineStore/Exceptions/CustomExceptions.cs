using System;

namespace ContosoOnlineStore.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public int ProductId { get; }

        public ProductNotFoundException(int productId)
            : base($"Product with ID {productId} was not found.")
        {
            ProductId = productId;
        }

        public ProductNotFoundException(int productId, Exception innerException)
            : base($"Product with ID {productId} was not found.", innerException)
        {
            ProductId = productId;
        }
    }

    public class InsufficientInventoryException : Exception
    {
        public int ProductId { get; }
        public int RequestedQuantity { get; }
        public int AvailableQuantity { get; }

        public InsufficientInventoryException(int productId, int requestedQuantity, int availableQuantity)
            : base($"Insufficient inventory for product ID {productId}. Requested: {requestedQuantity}, Available: {availableQuantity}")
        {
            ProductId = productId;
            RequestedQuantity = requestedQuantity;
            AvailableQuantity = availableQuantity;
        }
    }

    public class InvalidOrderException : Exception
    {
        public InvalidOrderException(string message) : base(message) { }

        public InvalidOrderException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class SecurityValidationException : Exception
    {
        public SecurityValidationException(string message) : base(message) { }

        public SecurityValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
