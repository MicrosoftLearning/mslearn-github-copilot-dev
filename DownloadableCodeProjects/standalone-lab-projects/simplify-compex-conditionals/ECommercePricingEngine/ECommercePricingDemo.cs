using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommercePricing
{
    public enum MembershipLevel { Guest, Silver, Gold, Premium }
    public enum SeasonalEvent { None, BlackFriday, CyberMonday, HolidayWeek, NewYear, BackToSchool }
    public enum RegionType { Domestic, International, PremiumZone }
    public enum PaymentMethod { CreditCard, DebitCard, PayPal, BankTransfer, Cryptocurrency }

    public class User
    {
        public MembershipLevel Membership { get; set; }
        public bool IsFirstTimeBuyer { get; set; }
        public int YearsAsMember { get; set; }
        public decimal LifetimeSpent { get; set; }
        public bool HasActiveSubscription { get; set; }
        public bool IsStudent { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsCorporateAccount { get; set; }
    }

    public class Coupon
    {
        public string? Code { get; set; }
        public bool IsValid { get; set; }
        public bool IsExpired { get; set; }
        public string? Type { get; set; } // "percent" or "shipping"
        public decimal Value { get; set; } // e.g., 10 for 10% off
    }

    public class Item
    {
        public string? Name { get; set; }
        public string? Category { get; set; } // e.g., "Electronics", "Clothing"
        public decimal Price { get; set; }
    }

    public class Order
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public bool IsDomestic { get; set; }
        public RegionType ShippingRegion { get; set; }
        public Coupon? Coupon { get; set; }
        public SeasonalEvent ActiveEvent { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool HasExpressShipping { get; set; }
        public bool IsPreOrder { get; set; }
        public DateTime OrderTime { get; set; }
        public bool IsBulkOrder { get; set; }
        public bool HasGiftWrap { get; set; }

        public decimal GetSubtotal() => Items.Sum(i => i.Price);
        public decimal GetSubtotalForCategory(string category) =>
            Items.Where(i => i.Category == category).Sum(i => i.Price);
        public bool ContainsCategory(string category) =>
            Items.Any(i => i.Category == category);
        public int GetCategoryItemCount(string category) =>
            Items.Count(i => i.Category == category);
        public bool IsHighValueOrder() => GetSubtotal() > 1000m;
        public bool HasMixedCategories() => Items.Select(i => i.Category).Distinct().Count() >= 3;
    }

    public class PricingEngine
    {
        // Security: Constants for validation bounds
        private const decimal MAX_DISCOUNT_PERCENT = 95m; // Maximum 95% discount
        private const decimal MIN_FINAL_PRICE = 0.01m; // Minimum $0.01 final price
        private const decimal MAX_ORDER_VALUE = 1_000_000m; // Maximum $1M order value

        public static void CalculateFinalPrice(User user, Order order)
        {
            // Security: Input validation to prevent null reference attacks
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }

            if (!IsValidOrder(order))
            {
                Console.WriteLine("Error: Invalid order data detected. Pricing calculation aborted.");
                return;
            }

            decimal baseTotal = order.GetSubtotal();
            
            // Security: Validate base total is within reasonable bounds
            if (baseTotal <= 0 || baseTotal > MAX_ORDER_VALUE)
            {
                Console.WriteLine($"Error: Order total ${baseTotal:F2} is outside valid range ($0.01 - ${MAX_ORDER_VALUE:N0})");
                return;
            }

            decimal discountPercent = 0m;
            decimal shippingCost = CalculateBaseShipping(order);
            var appliedDiscounts = new List<string>();

            // 1. Membership-based discounts: Primary customer tier evaluation
            if (user.Membership == MembershipLevel.Premium)
            {
                discountPercent = SafeAddDiscount(discountPercent, 15, "Premium membership (15%)", appliedDiscounts);
                
                // 2. Premium high-value threshold: Escalating discounts for premium members
                if (baseTotal > 10000)
                {
                    discountPercent = SafeAddDiscount(discountPercent, 10, "Ultra high-value bonus (10%)", appliedDiscounts);
                    
                    // 3. Seasonal event multiplier: Premium seasonal benefits
                    if (order.ActiveEvent == SeasonalEvent.BlackFriday || order.ActiveEvent == SeasonalEvent.CyberMonday)
                    {
                        discountPercent = SafeAddDiscount(discountPercent, 8, "Premium seasonal bonus (8%)", appliedDiscounts);
                        
                        // 4. Corporate account benefits: B2B premium advantages
                        if (user.IsCorporateAccount)
                        {
                            discountPercent = SafeAddDiscount(discountPercent, 5, "Corporate account bonus (5%)", appliedDiscounts);
                            
                            // 5. Subscription service benefits: Recurring revenue incentives
                            if (user.HasActiveSubscription)
                            {
                                discountPercent = SafeAddDiscount(discountPercent, 3, "Subscription service bonus (3%)", appliedDiscounts);
                                
                                // 6. Loyalty tenure reward: Long-term premium customer benefits
                                if (user.YearsAsMember >= 5)
                                {
                                    discountPercent = SafeAddDiscount(discountPercent, 5, "Veteran premium member (5%)", appliedDiscounts);
                                    
                                    // 7. Lifetime spending tier: Ultimate premium benefits
                                    if (user.LifetimeSpent > 50000)
                                    {
                                        discountPercent = SafeAddDiscount(discountPercent, 7, "VIP status (7%)", appliedDiscounts);
                                        
                                        // 8. Express shipping optimization: Premium logistics benefits
                                        if (order.HasExpressShipping)
                                        {
                                            discountPercent = SafeAddDiscount(discountPercent, 2, "Express shipping loyalty bonus (2%)", appliedDiscounts);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (baseTotal > 5000)
                {
                    discountPercent = SafeAddDiscount(discountPercent, 5, "High-value bonus (5%)", appliedDiscounts);
                }
            }
            else if (user.Membership == MembershipLevel.Gold)
            {
                discountPercent = SafeAddDiscount(discountPercent, 12, "Gold membership (12%)", appliedDiscounts);
                
                // 2. Gold seasonal benefits: Mid-tier seasonal advantages
                if (order.ActiveEvent != SeasonalEvent.None)
                {
                    discountPercent = SafeAddDiscount(discountPercent, 6, "Gold seasonal bonus (6%)", appliedDiscounts);
                    
                    // 3. Gold volume threshold: Quantity-based gold benefits
                    if (order.Items.Count >= 15)
                    {
                        discountPercent = SafeAddDiscount(discountPercent, 4, "Gold bulk bonus (4%)", appliedDiscounts);
                        
                        // 4. Category diversity bonus: Multi-category gold rewards
                        if (order.HasMixedCategories())
                        {
                            discountPercent = SafeAddDiscount(discountPercent, 3, "Category diversity bonus (3%)", appliedDiscounts);
                            
                            // 5. Employee discount stacking: Staff gold benefits
                            if (user.IsEmployee)
                            {
                                discountPercent = SafeAddDiscount(discountPercent, 10, "Employee gold discount (10%)", appliedDiscounts);
                                
                                // 6. Pre-order benefits: Early access inventory rewards
                                if (order.IsPreOrder)
                                {
                                    discountPercent = SafeAddDiscount(discountPercent, 5, "Pre-order employee bonus (5%)", appliedDiscounts);
                                    
                                    // 7. Payment method optimization: Financial processing benefits
                                    if (order.PaymentMethod == PaymentMethod.BankTransfer || order.PaymentMethod == PaymentMethod.Cryptocurrency)
                                    {
                                        discountPercent = SafeAddDiscount(discountPercent, 3, "Alternative payment bonus (3%)", appliedDiscounts);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (user.Membership == MembershipLevel.Silver)
            {
                discountPercent = SafeAddDiscount(discountPercent, 8, "Silver membership (8%)", appliedDiscounts);
                
                // 2. Silver student benefits: Educational discounts
                if (user.IsStudent)
                {
                    discountPercent = SafeAddDiscount(discountPercent, 5, "Student silver bonus (5%)", appliedDiscounts);
                    
                    // 3. Back-to-school special: Seasonal student benefits
                    if (order.ActiveEvent == SeasonalEvent.BackToSchool)
                    {
                        discountPercent = SafeAddDiscount(discountPercent, 7, "Back-to-school bonus (7%)", appliedDiscounts);
                        
                        // 4. Student bulk purchase: Educational volume discounts
                        if (order.Items.Count >= 8)
                        {
                            discountPercent = SafeAddDiscount(discountPercent, 4, "Student bulk discount (4%)", appliedDiscounts);
                            
                            // 5. Student electronics focus: Technology education discounts
                            if (SafeGetCategoryPercentage(order, "Electronics") > 0.6m)
                            {
                                discountPercent = SafeAddDiscount(discountPercent, 6, "Student tech focus bonus (6%)", appliedDiscounts);
                                
                                // 6. Gift wrap service: Student presentation benefits
                                if (order.HasGiftWrap)
                                {
                                    discountPercent = SafeAddDiscount(discountPercent, 2, "Gift presentation bonus (2%)", appliedDiscounts);
                                    
                                    // 7. Express delivery educational: Time-sensitive learning benefits
                                    if (order.HasExpressShipping)
                                    {
                                        discountPercent = SafeAddDiscount(discountPercent, 3, "Express education bonus (3%)", appliedDiscounts);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (user.IsFirstTimeBuyer)
            {
                discountPercent = SafeAddDiscount(discountPercent, 10, "First-time buyer (10%)", appliedDiscounts);
                
                // 2. New customer seasonal welcome: Event-based new customer benefits
                if (order.ActiveEvent != SeasonalEvent.None)
                {
                    discountPercent = SafeAddDiscount(discountPercent, 5, "Seasonal welcome bonus (5%)", appliedDiscounts);
                    
                    // 3. New customer volume commitment: Encouraging larger first orders
                    if (order.Items.Count >= 5)
                    {
                        discountPercent = SafeAddDiscount(discountPercent, 4, "First-order volume bonus (4%)", appliedDiscounts);
                        
                        // 4. Premium payment method: Financial service onboarding
                        if (order.PaymentMethod == PaymentMethod.PayPal || order.PaymentMethod == PaymentMethod.CreditCard)
                        {
                            discountPercent = SafeAddDiscount(discountPercent, 3, "Premium payment newcomer bonus (3%)", appliedDiscounts);
                            
                            // 5. High-value first purchase: Premium new customer treatment
                            if (order.IsHighValueOrder())
                            {
                                discountPercent = SafeAddDiscount(discountPercent, 6, "High-value newcomer bonus (6%)", appliedDiscounts);
                                
                                // 6. Premium zone shipping: Geographic expansion incentives
                                if (order.ShippingRegion == RegionType.PremiumZone)
                                {
                                    discountPercent = SafeAddDiscount(discountPercent, 4, "Premium zone newcomer bonus (4%)", appliedDiscounts);
                                    
                                    // 7. Express shipping trial: Premium service introduction
                                    if (order.HasExpressShipping)
                                    {
                                        discountPercent = SafeAddDiscount(discountPercent, 3, "Express shipping trial bonus (3%)", appliedDiscounts);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // 1. Coupon validation and application: Secondary discount layer
            if (order.Coupon != null)
            {
                // 2. Valid coupon: Apply coupon benefits with membership multipliers
                if (order.Coupon.IsValid)
                {
                    // 3. Percentage discount coupon: Membership-enhanced coupon benefits
                    if (order.Coupon.Type == "percent")
                    {
                        decimal couponValue = Math.Max(0, Math.Min(50, order.Coupon.Value)); // Security: Cap coupon at 50%
                        
                        // 4. Membership coupon enhancement: Tier-based coupon boosts
                        if (user.Membership == MembershipLevel.Premium)
                        {
                            couponValue = Math.Min(50, couponValue * 1.3m); // 30% coupon boost for Premium, capped at 50%
                            appliedDiscounts.Add($"Premium-enhanced coupon {order.Coupon.Code} ({couponValue:F1}%)");
                            
                            // 5. Seasonal coupon stacking: Event-based premium coupon benefits
                            if (order.ActiveEvent == SeasonalEvent.BlackFriday)
                            {
                                couponValue = Math.Min(55, couponValue + 5); // Black Friday premium coupon boost, capped at 55%
                                appliedDiscounts.Add("Black Friday premium coupon boost (5%)");
                                
                                // 6. Corporate payment optimization: B2B financial processing benefits
                                if (user.IsCorporateAccount && order.PaymentMethod == PaymentMethod.BankTransfer)
                                {
                                    couponValue = Math.Min(60, couponValue * 1.15m); // 15% corporate payment multiplier, capped at 60%
                                    appliedDiscounts.Add($"Corporate payment multiplier (total: {couponValue:F1}%)");
                                    
                                    // 7. Bulk order corporate: Large-scale business benefits
                                    if (order.IsBulkOrder)
                                    {
                                        couponValue = Math.Min(65, couponValue + 2); // Bulk corporate bonus, capped at 65%
                                        appliedDiscounts.Add("Bulk corporate bonus (2%)");
                                    }
                                }
                            }
                        }
                        else if (user.Membership == MembershipLevel.Gold)
                        {
                            couponValue = Math.Min(40, couponValue * 1.2m); // 20% coupon boost for Gold, capped at 40%
                            appliedDiscounts.Add($"Gold-enhanced coupon {order.Coupon.Code} ({couponValue:F1}%)");
                        }
                        else
                        {
                            appliedDiscounts.Add($"Coupon {order.Coupon.Code} ({couponValue}%)");
                        }
                        
                        discountPercent = SafeAddDiscount(discountPercent, couponValue, "", appliedDiscounts, false);
                    }
                    // 3. Free shipping coupon: Enhanced shipping benefits
                    else if (order.Coupon.Type == "shipping")
                    {
                        if (order.IsDomestic || user.Membership == MembershipLevel.Premium)
                        {
                            shippingCost = 0;
                            appliedDiscounts.Add($"Free shipping coupon {order.Coupon.Code}");
                        }
                    }
                }
                // 2. Expired coupon: Handle invalid coupon state
                else if (order.Coupon.IsExpired)
                {
                    appliedDiscounts.Add($"Coupon {order.Coupon.Code} expired - no discount");
                    Console.WriteLine("Coupon expired. No discount applied.");
                }
            }

            // 1. Bulk purchase incentive: Volume-based discount with category considerations
            if (order.Items.Count >= 20)
            {
                discountPercent = SafeAddDiscount(discountPercent, 8, "Major bulk purchase (8%)", appliedDiscounts);
            }
            else if (order.Items.Count >= 10)
            {
                discountPercent = SafeAddDiscount(discountPercent, 5, "Bulk purchase (5%)", appliedDiscounts);
            }

            // Security: Final discount validation
            discountPercent = Math.Min(discountPercent, MAX_DISCOUNT_PERCENT);

            // Apply final calculations with category-specific rules
            var finalCalculation = ApplyCategorySpecificDiscounts(baseTotal, discountPercent, order);
            decimal finalPrice = Math.Max(MIN_FINAL_PRICE, finalCalculation.finalPrice + shippingCost);

            // Display results
            Console.WriteLine($"Base Total: ${baseTotal:F2}");
            Console.WriteLine($"Applied Discounts: {string.Join(", ", appliedDiscounts)}");
            Console.WriteLine($"Total Discount: {discountPercent:F1}% (Electronics capped at 15%)");
            Console.WriteLine($"Shipping Cost: ${shippingCost:F2}");
            Console.WriteLine($"Final Price: ${finalPrice:F2}");
        }

        /// <summary>
        /// Security: Safe discount addition with bounds checking
        /// </summary>
        private static decimal SafeAddDiscount(decimal currentDiscount, decimal additionalDiscount, 
            string description, List<string> appliedDiscounts, bool addDescription = true)
        {
            if (additionalDiscount <= 0) return currentDiscount;
            
            decimal newTotal = currentDiscount + additionalDiscount;
            if (newTotal > MAX_DISCOUNT_PERCENT)
            {
                additionalDiscount = MAX_DISCOUNT_PERCENT - currentDiscount;
                newTotal = MAX_DISCOUNT_PERCENT;
            }

            if (addDescription && !string.IsNullOrEmpty(description))
            {
                appliedDiscounts.Add(description);
            }

            return newTotal;
        }

        /// <summary>
        /// Security: Safe category percentage calculation with division by zero protection
        /// </summary>
        private static decimal SafeGetCategoryPercentage(Order order, string category)
        {
            decimal total = order.GetSubtotal();
            if (total <= 0) return 0;
            
            return order.GetSubtotalForCategory(category) / total;
        }

        /// <summary>
        /// Security: Validates order data to prevent malicious inputs
        /// </summary>
        private static bool IsValidOrder(Order order)
        {
            if (order.Items == null || order.Items.Count == 0)
                return false;

            foreach (var item in order.Items)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.Name) || 
                    string.IsNullOrWhiteSpace(item.Category) || item.Price < 0 || item.Price > 100000)
                    return false;
            }

            return true;
        }

        private static decimal CalculateBaseShipping(Order order)
        {
            return order.ShippingRegion switch
            {
                RegionType.Domestic => 10m,
                RegionType.International => 25m,
                RegionType.PremiumZone => 35m,
                _ => order.IsDomestic ? 10m : 25m
            };
        }

        private static (decimal finalPrice, decimal appliedDiscount) ApplyCategorySpecificDiscounts(decimal baseTotal, decimal discountPercent, Order order)
        {
            // 1. Category-specific discount application: Enhanced margin protection
            decimal electronicsSubtotal = order.GetSubtotalForCategory("Electronics");
            decimal clothingSubtotal = order.GetSubtotalForCategory("Clothing");
            decimal accessoriesSubtotal = order.GetSubtotalForCategory("Accessories");
            decimal otherSubtotal = baseTotal - electronicsSubtotal - clothingSubtotal - accessoriesSubtotal;

            // 2. Electronics discount cap: Limit electronics discount to 15% maximum
            decimal electronicsDiscount = Math.Min(discountPercent, 15);
            decimal discountedElectronics = electronicsSubtotal * (1 - electronicsDiscount / 100);

            // 2. Clothing discount cap: Seasonal fashion considerations
            decimal clothingDiscount = order.ActiveEvent == SeasonalEvent.BackToSchool ? 
                Math.Min(discountPercent, 25) : Math.Min(discountPercent, 20);
            decimal discountedClothing = clothingSubtotal * (1 - clothingDiscount / 100);

            // 2. Accessories full discount: No restrictions on accessories
            decimal discountedAccessories = accessoriesSubtotal * (1 - discountPercent / 100);

            // 2. Other categories: Apply full discount percentage
            decimal discountedOther = otherSubtotal * (1 - discountPercent / 100);

            decimal finalPrice = discountedElectronics + discountedClothing + discountedAccessories + discountedOther;
            return (finalPrice, baseTotal - finalPrice);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create test data collections
            var users = CreateTestUsers();
            var coupons = CreateTestCoupons();
            var orders = CreateTestOrders();

            // Test different pricing scenarios
            TestPricingScenarios(users, coupons, orders);

            // Run security tests to demonstrate security measures
            SecurityTest.RunSecurityTests();
        }

        static List<User> CreateTestUsers()
        {
            return new List<User>
            {
                // Basic users
                new User { Membership = MembershipLevel.Guest, IsFirstTimeBuyer = true, YearsAsMember = 0, LifetimeSpent = 0, HasActiveSubscription = false, IsStudent = false, IsEmployee = false, IsCorporateAccount = false },
                new User { Membership = MembershipLevel.Guest, IsFirstTimeBuyer = false, YearsAsMember = 0, LifetimeSpent = 200, HasActiveSubscription = false, IsStudent = true, IsEmployee = false, IsCorporateAccount = false },
                
                // Silver members
                new User { Membership = MembershipLevel.Silver, IsFirstTimeBuyer = false, YearsAsMember = 1, LifetimeSpent = 1500, HasActiveSubscription = false, IsStudent = true, IsEmployee = false, IsCorporateAccount = false },
                new User { Membership = MembershipLevel.Silver, IsFirstTimeBuyer = false, YearsAsMember = 2, LifetimeSpent = 3000, HasActiveSubscription = true, IsStudent = false, IsEmployee = false, IsCorporateAccount = false },
                
                // Gold members
                new User { Membership = MembershipLevel.Gold, IsFirstTimeBuyer = false, YearsAsMember = 3, LifetimeSpent = 8000, HasActiveSubscription = false, IsStudent = false, IsEmployee = true, IsCorporateAccount = false },
                new User { Membership = MembershipLevel.Gold, IsFirstTimeBuyer = false, YearsAsMember = 4, LifetimeSpent = 15000, HasActiveSubscription = true, IsStudent = false, IsEmployee = false, IsCorporateAccount = true },
                
                // Premium members
                new User { Membership = MembershipLevel.Premium, IsFirstTimeBuyer = false, YearsAsMember = 6, LifetimeSpent = 75000, HasActiveSubscription = true, IsStudent = false, IsEmployee = false, IsCorporateAccount = true },
                new User { Membership = MembershipLevel.Premium, IsFirstTimeBuyer = false, YearsAsMember = 8, LifetimeSpent = 120000, HasActiveSubscription = true, IsStudent = false, IsEmployee = true, IsCorporateAccount = false }
            };
        }

        static List<Coupon?> CreateTestCoupons()
        {
            return new List<Coupon?>
            {
                null, // No coupon
                new Coupon { Code = "SAVE15", IsValid = true, IsExpired = false, Type = "percent", Value = 15 },
                new Coupon { Code = "FLASHSALE25", IsValid = true, IsExpired = false, Type = "percent", Value = 25 },
                new Coupon { Code = "FREESHIP", IsValid = true, IsExpired = false, Type = "shipping", Value = 0 },
                new Coupon { Code = "EXPIRED20", IsValid = false, IsExpired = true, Type = "percent", Value = 20 }
            };
        }

        static List<Order> CreateTestOrders()
        {
            // Complex high-value order - triggers deep nesting
            var complexOrder = new Order
            {
                IsDomestic = true,
                ShippingRegion = RegionType.PremiumZone,
                ActiveEvent = SeasonalEvent.BlackFriday,
                PaymentMethod = PaymentMethod.BankTransfer,
                HasExpressShipping = true,
                IsPreOrder = true,
                IsBulkOrder = true,
                HasGiftWrap = false,
                OrderTime = DateTime.Now,
                Items = new List<Item>
                {
                    // Electronics (triggers electronics specialist logic)
                    new Item { Name = "Gaming Laptop", Category = "Electronics", Price = 3500 },
                    new Item { Name = "4K Monitor", Category = "Electronics", Price = 1200 },
                    new Item { Name = "Mechanical Keyboard", Category = "Electronics", Price = 300 },
                    new Item { Name = "Gaming Mouse", Category = "Electronics", Price = 150 },
                    new Item { Name = "VR Headset", Category = "Electronics", Price = 800 },
                    new Item { Name = "Tablet", Category = "Electronics", Price = 600 },
                    
                    // Clothing
                    new Item { Name = "Designer Jacket", Category = "Clothing", Price = 800 },
                    new Item { Name = "Premium Jeans", Category = "Clothing", Price = 200 },
                    new Item { Name = "Casual Shirt", Category = "Clothing", Price = 80 },
                    new Item { Name = "Winter Boots", Category = "Clothing", Price = 250 },
                    new Item { Name = "Formal Suit", Category = "Clothing", Price = 600 },
                    new Item { Name = "Sports Wear", Category = "Clothing", Price = 120 },
                    new Item { Name = "Evening Dress", Category = "Clothing", Price = 400 },
                    new Item { Name = "Casual Sneakers", Category = "Clothing", Price = 150 },
                    new Item { Name = "Winter Coat", Category = "Clothing", Price = 350 },
                    
                    // Accessories (3+ categories for diversity bonus)
                    new Item { Name = "Luxury Watch", Category = "Accessories", Price = 2000 },
                    new Item { Name = "Designer Bag", Category = "Accessories", Price = 500 },
                    new Item { Name = "Gold Necklace", Category = "Accessories", Price = 800 },
                    new Item { Name = "Sunglasses", Category = "Accessories", Price = 200 },
                    new Item { Name = "Leather Wallet", Category = "Accessories", Price = 100 },
                    new Item { Name = "Smart Ring", Category = "Accessories", Price = 300 }
                }
            };

            // Student order - triggers student-specific logic
            var studentOrder = new Order
            {
                IsDomestic = true,
                ShippingRegion = RegionType.Domestic,
                ActiveEvent = SeasonalEvent.BackToSchool,
                PaymentMethod = PaymentMethod.PayPal,
                HasExpressShipping = true,
                IsPreOrder = false,
                IsBulkOrder = false,
                HasGiftWrap = true,
                OrderTime = DateTime.Now,
                Items = new List<Item>
                {
                    // Electronics-focused for student tech bonus
                    new Item { Name = "Laptop", Category = "Electronics", Price = 1200 },
                    new Item { Name = "External Monitor", Category = "Electronics", Price = 300 },
                    new Item { Name = "Wireless Mouse", Category = "Electronics", Price = 50 },
                    new Item { Name = "Keyboard", Category = "Electronics", Price = 80 },
                    new Item { Name = "Webcam", Category = "Electronics", Price = 100 },
                    new Item { Name = "Headphones", Category = "Electronics", Price = 150 },
                    new Item { Name = "External Drive", Category = "Electronics", Price = 120 },
                    new Item { Name = "Tablet", Category = "Electronics", Price = 400 },
                    
                    // Some non-electronics
                    new Item { Name = "Backpack", Category = "Accessories", Price = 80 },
                    new Item { Name = "Notebook", Category = "Accessories", Price = 15 }
                }
            };

            // First-time buyer order - triggers newcomer logic
            var newcomerOrder = new Order
            {
                IsDomestic = false,
                ShippingRegion = RegionType.PremiumZone,
                ActiveEvent = SeasonalEvent.NewYear,
                PaymentMethod = PaymentMethod.CreditCard,
                HasExpressShipping = true,
                IsPreOrder = false,
                IsBulkOrder = false,
                HasGiftWrap = false,
                OrderTime = DateTime.Now,
                Items = new List<Item>
                {
                    new Item { Name = "Smartphone", Category = "Electronics", Price = 800 },
                    new Item { Name = "Case", Category = "Accessories", Price = 30 },
                    new Item { Name = "Charger", Category = "Electronics", Price = 50 },
                    new Item { Name = "Screen Protector", Category = "Accessories", Price = 20 },
                    new Item { Name = "Wireless Earbuds", Category = "Electronics", Price = 200 },
                    new Item { Name = "Power Bank", Category = "Electronics", Price = 60 }
                }
            };

            return new List<Order> { complexOrder, studentOrder, newcomerOrder };
        }

        static void TestPricingScenarios(List<User> users, List<Coupon?> coupons, List<Order> orders)
        {
            var scenarioCount = 1;
            
            // Test key complex scenarios instead of all combinations
            var keyScenarios = new[]
            {
                // Complex Premium VIP scenario - should hit nesting level 8
                (users.First(u => u.Membership == MembershipLevel.Premium && u.LifetimeSpent > 100000), 
                 coupons.First(c => c?.Code == "FLASHSALE25"), 
                 orders[0]), // Complex order
                
                // Gold employee scenario - should hit nesting level 7
                (users.First(u => u.Membership == MembershipLevel.Gold && u.IsEmployee), 
                 coupons.First(c => c?.Code == "SAVE15"), 
                 orders[0]), // Complex order with pre-order
                
                // Student Silver scenario - should hit nesting level 7
                (users.First(u => u.Membership == MembershipLevel.Silver && u.IsStudent), 
                 coupons.First(c => c?.Code == "SAVE15"), 
                 orders[1]), // Student order
                
                // First-time buyer complex scenario - should hit nesting level 7
                (users.First(u => u.IsFirstTimeBuyer), 
                 coupons.First(c => c?.Code == "FLASHSALE25"), 
                 orders[2]), // Newcomer order
                
                // Premium with expired coupon
                (users.First(u => u.Membership == MembershipLevel.Premium && u.LifetimeSpent > 100000), 
                 coupons.First(c => c?.Code == "EXPIRED20"), 
                 orders[0]),
                
                // No coupon scenarios
                (users.First(u => u.Membership == MembershipLevel.Gold && u.IsEmployee), 
                 null, 
                 orders[0])
            };

            foreach (var (user, coupon, order) in keyScenarios)
            {
                // Clone order and assign coupon to avoid modifying original
                var testOrder = new Order
                {
                    IsDomestic = order.IsDomestic,
                    ShippingRegion = order.ShippingRegion,
                    ActiveEvent = order.ActiveEvent,
                    PaymentMethod = order.PaymentMethod,
                    HasExpressShipping = order.HasExpressShipping,
                    IsPreOrder = order.IsPreOrder,
                    IsBulkOrder = order.IsBulkOrder,
                    HasGiftWrap = order.HasGiftWrap,
                    OrderTime = order.OrderTime,
                    Coupon = coupon,
                    Items = order.Items
                };

                Console.WriteLine($"\n=== COMPLEX SCENARIO {scenarioCount++} ===");
                Console.WriteLine($"User: {user.Membership} membership, {user.YearsAsMember} years, Lifetime: ${user.LifetimeSpent:F0}");
                Console.WriteLine($"      First-time: {user.IsFirstTimeBuyer}, Student: {user.IsStudent}, Employee: {user.IsEmployee}");
                Console.WriteLine($"      Corporate: {user.IsCorporateAccount}, Subscription: {user.HasActiveSubscription}");
                Console.WriteLine($"Order: {testOrder.Items.Count} items, Total: ${testOrder.GetSubtotal():F2}");
                Console.WriteLine($"       Event: {testOrder.ActiveEvent}, Payment: {testOrder.PaymentMethod}, Express: {testOrder.HasExpressShipping}");
                Console.WriteLine($"       Pre-order: {testOrder.IsPreOrder}, Bulk: {testOrder.IsBulkOrder}, Gift Wrap: {testOrder.HasGiftWrap}");
                Console.WriteLine($"       Region: {testOrder.ShippingRegion}");
                Console.WriteLine($"Electronics: ${testOrder.GetSubtotalForCategory("Electronics"):F2}, " +
                                $"Clothing: ${testOrder.GetSubtotalForCategory("Clothing"):F2}, " +
                                $"Accessories: ${testOrder.GetSubtotalForCategory("Accessories"):F2}");
                
                if (coupon != null)
                {
                    Console.WriteLine($"Coupon: {coupon.Code} ({coupon.Type}, {coupon.Value}%, Valid: {coupon.IsValid}, Expired: {coupon.IsExpired})");
                }
                else
                {
                    Console.WriteLine("Coupon: None");
                }
                
                Console.WriteLine("--- COMPLEX PRICING CALCULATION ---");
                PricingEngine.CalculateFinalPrice(user, testOrder);
                Console.WriteLine(new string('=', 60));
            }
        }
    }
}
