using ContosoShopEasy.Models;
using ContosoShopEasy.Data;

namespace ContosoShopEasy.Services
{
    public class PaymentService
    {
        // Security vulnerability: Hardcoded configuration values (but won't trigger GitHub Secret Scanning)
        private const string PAYMENT_GATEWAY_URL = "https://api.contoso-payments.com";
        private const string MERCHANT_NAME = "ContosoShopEasy";
        private const string GATEWAY_VERSION = "v2.1";

        private readonly OrderRepository _orderRepository;

        public PaymentService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Vulnerable payment processing method
        public bool ProcessPayment(string cardNumber, string cardHolderName, string expiryDate, string cvv, decimal amount)
        {
            // Security vulnerability: Log sensitive payment information
            Console.WriteLine($"[DEBUG] Processing payment for card: {cardNumber}");
            Console.WriteLine($"[DEBUG] Card holder: {cardHolderName}");
            Console.WriteLine($"[DEBUG] Expiry: {expiryDate}, CVV: {cvv}");
            Console.WriteLine($"[DEBUG] Amount: ${amount}");
            
            // Security vulnerability: Log configuration details
            Console.WriteLine($"[DEBUG] Using payment gateway: {PAYMENT_GATEWAY_URL}");
            Console.WriteLine($"[DEBUG] Merchant: {MERCHANT_NAME}");
            Console.WriteLine($"[DEBUG] Gateway version: {GATEWAY_VERSION}");

            // Simulate payment validation (vulnerable)
            if (!ValidateCardNumber(cardNumber))
            {
                Console.WriteLine($"[ERROR] Invalid card number: {cardNumber}");
                return false;
            }

            if (!ValidateExpiryDate(expiryDate))
            {
                Console.WriteLine($"[ERROR] Invalid or expired date: {expiryDate}");
                return false;
            }

            // Simulate payment processing
            Console.WriteLine("[INFO] Connecting to payment gateway...");
            Thread.Sleep(1000); // Simulate network delay

            // Security vulnerability: Generate predictable transaction IDs
            string transactionId = GenerateTransactionId(cardNumber, amount);
            
            // Security vulnerability: Store sensitive card data
            var paymentInfo = new PaymentInfo
            {
                Method = PaymentMethod.CreditCard,
                CardNumber = cardNumber, // Should never store full card numbers
                CardHolderName = cardHolderName,
                ExpiryDate = expiryDate,
                CVV = cvv, // Should never store CVV
                Amount = amount,
                ProcessedDate = DateTime.UtcNow,
                Status = PaymentStatus.Approved,
                TransactionId = transactionId
            };

            Console.WriteLine($"[SUCCESS] Payment processed successfully!");
            Console.WriteLine($"[DEBUG] Transaction ID: {transactionId}");
            
            // Security vulnerability: Log complete payment details
            Console.WriteLine($"[LOG] Payment completed - Card: {cardNumber}, Amount: ${amount}, Transaction: {transactionId}");

            return true;
        }

        // Vulnerable card validation
        private bool ValidateCardNumber(string cardNumber)
        {
            // Security vulnerability: Weak validation - only checks length
            if (string.IsNullOrEmpty(cardNumber))
                return false;

            // Remove spaces and dashes
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // Security vulnerability: Accept any 13-19 digit number
            return cardNumber.Length >= 13 && cardNumber.Length <= 19 && cardNumber.All(char.IsDigit);
        }

        private bool ValidateExpiryDate(string expiryDate)
        {
            // Security vulnerability: Basic validation only
            if (string.IsNullOrEmpty(expiryDate) || !expiryDate.Contains("/"))
                return false;

            var parts = expiryDate.Split('/');
            if (parts.Length != 2)
                return false;

            if (int.TryParse(parts[0], out int month) && int.TryParse(parts[1], out int year))
            {
                if (year < 100) year += 2000; // Convert YY to YYYY
                var expiry = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                return expiry >= DateTime.Now;
            }

            return false;
        }

        // Security vulnerability: Predictable transaction ID generation
        private string GenerateTransactionId(string cardNumber, decimal amount)
        {
            // Vulnerable: Using predictable pattern
            string lastFour = cardNumber.Length >= 4 ? cardNumber.Substring(cardNumber.Length - 4) : cardNumber;
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
            string amountStr = amount.ToString("F2").Replace(".", "");
            
            return $"TXN_{timestamp}_{lastFour}_{amountStr}";
        }

        public bool RefundPayment(string transactionId, decimal amount)
        {
            // Security vulnerability: Log refund details
            Console.WriteLine($"[DEBUG] Processing refund for transaction: {transactionId}, Amount: ${amount}");
            Console.WriteLine($"[DEBUG] Using payment gateway: {PAYMENT_GATEWAY_URL}");

            // Simulate refund processing
            Console.WriteLine("[INFO] Processing refund...");
            Thread.Sleep(500);

            Console.WriteLine($"[SUCCESS] Refund processed for transaction: {transactionId}");
            return true;
        }

        // Method to get payment history - with security issues
        public List<PaymentInfo> GetPaymentHistory(int userId)
        {
            Console.WriteLine($"[DEBUG] Retrieving payment history for user: {userId}");
            
            // In a real app, this would query the database
            // For demo purposes, we'll return empty list
            return new List<PaymentInfo>();
        }
    }
}