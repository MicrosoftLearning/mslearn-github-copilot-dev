using System;

namespace ECommercePricing
{
    /// <summary>
    /// Security testing class to demonstrate the security measures in action
    /// </summary>
    public class SecurityTest
    {
        public static void RunSecurityTests()
        {
            Console.WriteLine("\n=== SECURITY TESTING ===");
            
            // Test 1: Null user protection
            Console.WriteLine("\n--- Test 1: Null User Protection ---");
            try
            {
                var validOrder = CreateValidOrder();
                PricingEngine.CalculateFinalPrice(null, validOrder);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"✓ Security measure working: {ex.Message}");
            }

            // Test 2: Null order protection
            Console.WriteLine("\n--- Test 2: Null Order Protection ---");
            try
            {
                var validUser = CreateValidUser();
                PricingEngine.CalculateFinalPrice(validUser, null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"✓ Security measure working: {ex.Message}");
            }

            // Test 3: Invalid order data (negative prices)
            Console.WriteLine("\n--- Test 3: Invalid Order Data (Negative Prices) ---");
            var userForInvalidTest = CreateValidUser();
            var invalidOrder = new Order
            {
                IsDomestic = true,
                ShippingRegion = RegionType.Domestic,
                ActiveEvent = SeasonalEvent.None,
                PaymentMethod = PaymentMethod.CreditCard,
                Items = new List<Item>
                {
                    new Item { Name = "Invalid Item", Category = "Electronics", Price = -100 } // Negative price
                }
            };
            Console.WriteLine("Testing with negative price item...");
            PricingEngine.CalculateFinalPrice(userForInvalidTest, invalidOrder);

            // Test 4: Empty order
            Console.WriteLine("\n--- Test 4: Empty Order ---");
            var emptyOrder = new Order
            {
                IsDomestic = true,
                ShippingRegion = RegionType.Domestic,
                ActiveEvent = SeasonalEvent.None,
                PaymentMethod = PaymentMethod.CreditCard,
                Items = new List<Item>() // Empty items list
            };
            Console.WriteLine("Testing with empty order...");
            PricingEngine.CalculateFinalPrice(userForInvalidTest, emptyOrder);

            // Test 5: Extreme coupon value (should be capped)
            Console.WriteLine("\n--- Test 5: Extreme Coupon Value Protection ---");
            var extremeCouponOrder = CreateValidOrder();
            extremeCouponOrder.Coupon = new Coupon 
            { 
                Code = "EXTREME99", 
                IsValid = true, 
                IsExpired = false, 
                Type = "percent", 
                Value = 99 // Extreme 99% discount
            };
            Console.WriteLine("Testing with 99% coupon (should be capped at 50%)...");
            PricingEngine.CalculateFinalPrice(userForInvalidTest, extremeCouponOrder);

            Console.WriteLine("\n=== SECURITY TESTING COMPLETE ===\n");
        }

        private static User CreateValidUser()
        {
            return new User
            {
                Membership = MembershipLevel.Silver,
                IsFirstTimeBuyer = false,
                YearsAsMember = 2,
                LifetimeSpent = 1000,
                HasActiveSubscription = false,
                IsStudent = false,
                IsEmployee = false,
                IsCorporateAccount = false
            };
        }

        private static Order CreateValidOrder()
        {
            return new Order
            {
                IsDomestic = true,
                ShippingRegion = RegionType.Domestic,
                ActiveEvent = SeasonalEvent.None,
                PaymentMethod = PaymentMethod.CreditCard,
                HasExpressShipping = false,
                IsPreOrder = false,
                IsBulkOrder = false,
                HasGiftWrap = false,
                OrderTime = DateTime.Now,
                Items = new List<Item>
                {
                    new Item { Name = "Test Item", Category = "Electronics", Price = 100 }
                }
            };
        }
    }
}
