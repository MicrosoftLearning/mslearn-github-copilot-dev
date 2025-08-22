using System.ComponentModel.DataAnnotations;

namespace ContosoOnlineStore.Configuration
{
    public class AppSettings
    {
        [Range(1, 1000)]
        public int MaxOrderItems { get; set; } = 50;

        [Range(100, 30000)]
        public int EmailTimeoutMs { get; set; } = 2000;

        public bool EnableDetailedLogging { get; set; } = true;

        public SecuritySettings SecuritySettings { get; set; } = new();

        public PerformanceSettings PerformanceSettings { get; set; } = new();
    }

    public class SecuritySettings
    {
        [Range(0.01, 100000.00)]
        public decimal MaxProductPrice { get; set; } = 10000.00m;

        [Range(0.01, 1000.00)]
        public decimal MinProductPrice { get; set; } = 0.01m;

        public bool AllowNegativeInventory { get; set; } = false;
    }

    public class PerformanceSettings
    {
        [Range(1, 1440)]
        public int CacheExpirationMinutes { get; set; } = 30;

        [Range(1000, 60000)]
        public int DatabaseTimeoutMs { get; set; } = 5000;

        [Range(1, 1000)]
        public int MaxConcurrentOrders { get; set; } = 100;
    }
}
