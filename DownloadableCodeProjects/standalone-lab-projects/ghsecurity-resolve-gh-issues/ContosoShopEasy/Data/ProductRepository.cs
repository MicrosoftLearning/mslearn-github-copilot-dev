using ContosoShopEasy.Models;

namespace ContosoShopEasy.Data
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>();
        private static List<Category> _categories = new List<Category>();
        private static int _nextProductId = 1;

        static ProductRepository()
        {
            InitializeCategories();
            InitializeProducts();
        }

        private static void InitializeCategories()
        {
            _categories.AddRange(new List<Category>
            {
                new Category(1, "Electronics", "Electronic devices and gadgets"),
                new Category(2, "Computers & Laptops", "Desktop computers, laptops, and accessories", 1),
                new Category(3, "Smartphones & Tablets", "Mobile devices and tablets", 1),
                new Category(4, "Audio & Headphones", "Headphones, speakers, and audio equipment", 1),
                new Category(5, "Home & Garden", "Home improvement and garden supplies"),
                new Category(6, "Clothing & Fashion", "Apparel and fashion accessories"),
                new Category(7, "Books & Media", "Books, movies, music, and digital media"),
                new Category(8, "Sports & Recreation", "Sports equipment and recreational items"),
                new Category(9, "Health & Beauty", "Health, beauty, and personal care products"),
                new Category(10, "Automotive", "Car accessories and automotive supplies")
            });
        }

        private static void InitializeProducts()
        {
            _products.AddRange(new List<Product>
            {
                // Electronics - Computers & Laptops
                new Product(1, "MacBook Pro 16-inch", "Apple MacBook Pro with M2 Pro chip, 16GB RAM, 512GB SSD", 2499.99m, 2, "Apple", "MBP16-M2P-512", 15, "/images/macbook-pro-16.jpg", 4.7, 245),
                new Product(2, "Dell XPS 13", "Dell XPS 13 laptop with Intel i7, 16GB RAM, 1TB SSD, Windows 11", 1899.99m, 2, "Dell", "XPS13-I7-1TB", 8, "/images/dell-xps-13.jpg", 4.5, 189),
                new Product(3, "Gaming Desktop PC", "Custom gaming PC with RTX 4070, AMD Ryzen 7, 32GB RAM, 2TB NVMe SSD", 2299.99m, 2, "CustomBuild", "GAMING-RTX4070", 5, "/images/gaming-pc.jpg", 4.8, 67),
                new Product(4, "HP EliteBook 840", "Business laptop with Intel i5, 16GB RAM, 512GB SSD, Windows 11 Pro", 1299.99m, 2, "HP", "EB840-I5-512", 12, "/images/hp-elitebook.jpg", 4.3, 98),

                // Electronics - Smartphones & Tablets
                new Product(5, "iPhone 15 Pro", "Apple iPhone 15 Pro with A17 Pro chip, 256GB storage, ProRAW camera", 1199.99m, 3, "Apple", "IP15P-256GB", 25, "/images/iphone-15-pro.jpg", 4.6, 412),
                new Product(6, "Samsung Galaxy S24 Ultra", "Samsung Galaxy S24 Ultra with S Pen, 512GB storage, 200MP camera", 1399.99m, 3, "Samsung", "GS24U-512GB", 18, "/images/galaxy-s24-ultra.jpg", 4.5, 298),
                new Product(7, "iPad Air 11-inch", "Apple iPad Air with M2 chip, 256GB storage, WiFi + Cellular", 899.99m, 3, "Apple", "IPAD-AIR-256", 22, "/images/ipad-air-11.jpg", 4.7, 156),
                new Product(8, "Google Pixel 8 Pro", "Google Pixel 8 Pro with AI features, 256GB storage, advanced camera system", 999.99m, 3, "Google", "PIX8P-256GB", 14, "/images/pixel-8-pro.jpg", 4.4, 203),

                // Electronics - Audio & Headphones
                new Product(9, "Sony WH-1000XM5", "Sony wireless noise-canceling headphones with 30-hour battery life", 399.99m, 4, "Sony", "WH1000XM5", 35, "/images/sony-wh1000xm5.jpg", 4.8, 1247),
                new Product(10, "AirPods Pro 2nd Gen", "Apple AirPods Pro with active noise cancellation and spatial audio", 249.99m, 4, "Apple", "APP-2ND-GEN", 45, "/images/airpods-pro-2.jpg", 4.6, 892),
                new Product(11, "Bose QuietComfort 45", "Bose wireless noise-canceling headphones with 24-hour battery", 329.99m, 4, "Bose", "QC45-BLK", 28, "/images/bose-qc45.jpg", 4.5, 654),
                new Product(12, "JBL Charge 5", "JBL portable Bluetooth speaker with powerbank feature, IP67 waterproof", 179.99m, 4, "JBL", "CHARGE5-BLU", 52, "/images/jbl-charge5.jpg", 4.3, 423),

                // Home & Garden
                new Product(13, "Dyson V15 Detect", "Dyson cordless vacuum cleaner with laser dust detection", 749.99m, 5, "Dyson", "V15-DETECT", 19, "/images/dyson-v15.jpg", 4.7, 234),
                new Product(14, "Ninja Foodi Air Fryer", "8-quart air fryer with multiple cooking functions", 199.99m, 5, "Ninja", "AF101-8QT", 31, "/images/ninja-foodi.jpg", 4.4, 567),
                new Product(15, "Ring Video Doorbell Pro 2", "Smart doorbell with 1536p video, 3D motion detection", 279.99m, 5, "Ring", "RVD-PRO2", 24, "/images/ring-doorbell-pro2.jpg", 4.2, 189),
                new Product(16, "Instant Pot Duo 7-in-1", "Electric pressure cooker with 7 cooking functions, 6-quart", 99.99m, 5, "Instant Pot", "DUO-6QT", 47, "/images/instant-pot-duo.jpg", 4.6, 2341),

                // Clothing & Fashion
                new Product(17, "Levi's 511 Slim Jeans", "Classic slim-fit jeans in dark indigo wash, various sizes", 79.99m, 6, "Levi's", "511-SLIM-INDIGO", 85, "/images/levis-511.jpg", 4.3, 456),
                new Product(18, "Nike Air Max 270", "Nike Air Max 270 running shoes with Max Air unit", 149.99m, 6, "Nike", "AM270-BLK-WHT", 67, "/images/nike-air-max-270.jpg", 4.5, 789),
                new Product(19, "North Face Venture 2 Jacket", "Waterproof rain jacket for outdoor activities", 99.99m, 6, "The North Face", "VENTURE2-BLK", 43, "/images/northface-venture2.jpg", 4.4, 234),
                new Product(20, "Ray-Ban Aviator Sunglasses", "Classic aviator sunglasses with polarized lenses", 179.99m, 6, "Ray-Ban", "RB3025-GOLD", 38, "/images/rayban-aviator.jpg", 4.7, 567),

                // Books & Media
                new Product(21, "The Psychology of Money", "Morgan Housel's bestselling book on financial psychology", 16.99m, 7, "Harriman House", "PSYCH-MONEY", 156, "/images/psychology-of-money.jpg", 4.8, 3421),
                new Product(22, "Atomic Habits", "James Clear's guide to building good habits and breaking bad ones", 18.99m, 7, "Avery", "ATOMIC-HABITS", 203, "/images/atomic-habits.jpg", 4.7, 5632),
                new Product(23, "PlayStation 5", "Sony PlayStation 5 gaming console with DualSense controller", 499.99m, 7, "Sony", "PS5-CONSOLE", 8, "/images/playstation-5.jpg", 4.6, 1234),
                new Product(24, "Nintendo Switch OLED", "Nintendo Switch OLED model with enhanced screen and audio", 349.99m, 7, "Nintendo", "SW-OLED-WHT", 23, "/images/nintendo-switch-oled.jpg", 4.5, 987),

                // Sports & Recreation
                new Product(25, "Peloton Bike+", "Peloton exercise bike with rotating touchscreen and auto-resistance", 2495.99m, 8, "Peloton", "BIKE-PLUS", 3, "/images/peloton-bike-plus.jpg", 4.4, 456),
                new Product(26, "YETI Rambler 30oz", "Insulated stainless steel tumbler with MagSlider lid", 39.99m, 8, "YETI", "RAMBLER-30OZ", 127, "/images/yeti-rambler-30.jpg", 4.8, 234),
                new Product(27, "Wilson Pro Staff Tennis Racket", "Professional tennis racket used by tour players", 249.99m, 8, "Wilson", "PROSTAFF-97", 15, "/images/wilson-prostaff.jpg", 4.6, 89),
                new Product(28, "Fitbit Charge 6", "Advanced fitness tracker with GPS and heart rate monitoring", 199.99m, 8, "Fitbit", "CHARGE6-BLK", 34, "/images/fitbit-charge6.jpg", 4.3, 567),

                // Health & Beauty
                new Product(29, "Dyson Supersonic Hair Dryer", "Professional hair dryer with intelligent heat control", 429.99m, 9, "Dyson", "SUPERSONIC-FUC", 18, "/images/dyson-supersonic.jpg", 4.7, 892),
                new Product(30, "Olaplex No. 3 Hair Treatment", "At-home hair perfector treatment for all hair types", 28.99m, 9, "Olaplex", "NO3-TREATMENT", 89, "/images/olaplex-no3.jpg", 4.6, 1567),
                new Product(31, "Cetaphil Gentle Skin Cleanser", "Gentle face and body cleanser for sensitive skin, 16 fl oz", 13.99m, 9, "Cetaphil", "GENTLE-CLEANSER", 145, "/images/cetaphil-cleanser.jpg", 4.5, 2341),
                new Product(32, "Philips Sonicare DiamondClean", "Electric toothbrush with 5 brushing modes and travel case", 249.99m, 9, "Philips", "SONICARE-DC", 26, "/images/sonicare-diamondclean.jpg", 4.4, 678),

                // Automotive
                new Product(33, "Garmin DriveSmart 66", "6.95-inch GPS navigator with voice-activated navigation", 249.99m, 10, "Garmin", "DRIVESMART-66", 21, "/images/garmin-drivesmart66.jpg", 4.3, 234),
                new Product(34, "Chemical Guys Complete Car Care Kit", "16-piece car washing and detailing kit", 149.99m, 10, "Chemical Guys", "CARCARE-KIT16", 38, "/images/chemical-guys-kit.jpg", 4.5, 456),
                new Product(35, "AUKEY Car Charger", "Dual-port USB car charger with fast charging technology", 19.99m, 10, "AUKEY", "CC-S1-BLK", 167, "/images/aukey-car-charger.jpg", 4.2, 1234),

                // Additional popular products
                new Product(36, "Amazon Echo Dot 5th Gen", "Smart speaker with Alexa voice control", 49.99m, 1, "Amazon", "ECHO-DOT-5", 78, "/images/echo-dot-5.jpg", 4.4, 3456),
                new Product(37, "Kindle Paperwhite", "Waterproof e-reader with 6.8-inch display and adjustable warm light", 149.99m, 7, "Amazon", "KINDLE-PW-11", 45, "/images/kindle-paperwhite.jpg", 4.6, 2890),
                new Product(38, "Apple Watch Series 9", "Advanced smartwatch with health monitoring and fitness tracking", 429.99m, 3, "Apple", "AW-S9-45MM", 29, "/images/apple-watch-s9.jpg", 4.5, 1678),
                new Product(39, "Stanley Adventure Quencher", "40oz stainless steel tumbler with handle and straw", 44.99m, 8, "Stanley", "ADV-QUENCH-40", 156, "/images/stanley-quencher.jpg", 4.7, 567),
                new Product(40, "Anker PowerCore 10000", "Portable charger with high-speed charging technology", 29.99m, 1, "Anker", "POWERCORE-10K", 234, "/images/anker-powercore.jpg", 4.4, 2345)
            });

            _nextProductId = _products.Count + 1;
        }

        public List<Product> GetAllProducts()
        {
            return _products.ToList();
        }

        public Product? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId && p.IsActive).ToList();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return new List<Product>();

            searchTerm = searchTerm.ToLower();
            return _products.Where(p => p.IsActive &&
                (p.Name.ToLower().Contains(searchTerm) ||
                 p.Description.ToLower().Contains(searchTerm) ||
                 p.Brand.ToLower().Contains(searchTerm)))
                .ToList();
        }

        public void AddProduct(Product product)
        {
            product.Id = _nextProductId++;
            _products.Add(product);
        }

        public bool UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                var index = _products.IndexOf(existingProduct);
                _products[index] = product;
                return true;
            }
            return false;
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                product.IsActive = false;
                return true;
            }
            return false;
        }

        public List<Category> GetAllCategories()
        {
            return _categories.ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public int GetNextProductId()
        {
            return _nextProductId;
        }
    }
}