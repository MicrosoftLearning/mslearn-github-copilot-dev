// Configuration.cs - Application configuration settings
namespace EcommerceApp.Configuration;

public static class AppConfig
{
    // Shipping configuration
    public static decimal BaseShippingRate { get; } = 5.99m;
    public static decimal WeightBasedRatePerPound { get; } = 1.25m;
    public static decimal FreeShippingThreshold { get; } = 50.00m;
    public static decimal ReturnProcessingFee { get; } = 2.99m;

    // Security configuration
    public static int MaxIdLength { get; } = 20;
    public static int MinIdLength { get; } = 6;

    // Business rules
    public static int MaxReturnDays { get; } = 30;
    public static decimal MaxRefundAmount { get; } = 1000.00m;
}
