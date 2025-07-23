using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommercePricing
{
    public enum MembershipLevel { Guest, Silver, Premium }

    public class User
    {
        public MembershipLevel Membership { get; set; }
        public bool IsFirstTimeBuyer { get; set; }
    }

    public class Coupon
    {
        public string Code { get; set; }
        public bool IsValid { get; set; }
        public bool IsExpired { get; set; }
        public string Type { get; set; } // "percent" or "shipping"
        public decimal Value { get; set; } // e.g., 10 for 10% off
    }

    public class Item
    {
        public string Name { get; set; }
        public string Category { get; set; } // e.g., "Electronics", "Clothing"
        public decimal Price { get; set; }
    }

    public class Order
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public bool IsDomestic { get; set; }
        public Coupon Coupon { get; set; }

        public decimal GetSubtotal() => Items.Sum(i => i.Price);
        public decimal GetSubtotalForCategory(string category) =>
            Items.Where(i => i.Category == category).Sum(i => i.Price);
    }

    public class PricingEngine
    {
        public static void CalculateFinalPrice(User user, Order order)
        {
            decimal baseTotal = order.GetSubtotal();
            decimal discountPercent = 0m;
            decimal shippingCost = order.IsDomestic ? 10m : 25m;

            // Membership-based discounts
            if (user.Membership == MembershipLevel.Premium)
            {
                discountPercent += 10;
                if (baseTotal > 5000)
                    discountPercent += 5;
            }
            else if (user.Membership == MembershipLevel.Silver)
            {
                discountPercent += 5;
            }
            else if (user.IsFirstTimeBuyer)
            {
                discountPercent += 5;
            }

            // Coupon logic
            if (order.Coupon != null)
            {
                if (order.Coupon.IsValid)
                {
                    if (order.Coupon.Type == "percent")
                    {
                        discountPercent += order.Coupon.Value;
                    }
                    else if (order.Coupon.Type == "shipping" && order.IsDomestic)
                    {
                        shippingCost = 0;
                    }
                }
                else if (order.Coupon.IsExpired)
                {
                    Console.WriteLine("Coupon expired. No discount applied.");
                }
            }

            // Bulk purchase discount
            if (order.Items.Count >= 10)
            {
                discountPercent += 5;
            }

            // Apply discount with category cap
            decimal electronicsSubtotal = order.GetSubtotalForCategory("Electronics");
            decimal otherSubtotal = baseTotal - electronicsSubtotal;

            decimal electronicsDiscount = Math.Min(discountPercent, 10);
            decimal discountedElectronics = electronicsSubtotal * (1 - electronicsDiscount / 100);
            decimal discountedOther = otherSubtotal * (1 - discountPercent / 100);

            decimal finalPrice = discountedElectronics + discountedOther + shippingCost;

            Console.WriteLine($"Base Total: ${baseTotal:F2}");
            Console.WriteLine($"Discount Applied: {discountPercent}% (Electronics capped at 10%)");
            Console.WriteLine($"Shipping Cost: ${shippingCost:F2}");
            Console.WriteLine($"Final Price: ${finalPrice:F2}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Membership = MembershipLevel.Premium,
                IsFirstTimeBuyer = false
            };

            var coupon = new Coupon
            {
                Code = "SAVE10",
                IsValid = true,
                IsExpired = false,
                Type = "percent",
                Value = 10
            };

            var order = new Order
            {
                IsDomestic = true,
                Coupon = coupon,
                Items = new List<Item>
                {
                    new Item { Name = "Laptop", Category = "Electronics", Price = 1500 },
                    new Item { Name = "Headphones", Category = "Electronics", Price = 200 },
                    new Item { Name = "Shoes", Category = "Clothing", Price = 120 },
                    new Item { Name = "Jacket", Category = "Clothing", Price = 180 },
                    new Item { Name = "Watch", Category = "Accessories", Price = 250 },
                    new Item { Name = "Backpack", Category = "Accessories", Price = 90 },
                    new Item { Name = "T-shirt", Category = "Clothing", Price = 30 },
                    new Item { Name = "Socks", Category = "Clothing", Price = 20 },
                    new Item { Name = "Tablet", Category = "Electronics", Price = 800 },
                    new Item { Name = "Charger", Category = "Electronics", Price = 50 }
                }
            };

            PricingEngine.CalculateFinalPrice(user, order);
        }
    }
}
