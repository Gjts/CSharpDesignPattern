using System.Text;

namespace _12Flyweight._02Example._01ProductCache
{
    // 商品基础信息（内部状态 - 共享）
    public class ProductBasicInfo
    {
        public string SkuId { get; }
        public string Name { get; }
        public string Category { get; }
        public string Brand { get; }
        public string ImageUrl { get; }
        public string Description { get; }
        public decimal BasePrice { get; }

        public ProductBasicInfo(string skuId, string name, string category, 
            string brand, string imageUrl, string description, decimal basePrice)
        {
            SkuId = skuId;
            Name = name;
            Category = category;
            Brand = brand;
            ImageUrl = imageUrl;
            Description = description;
            BasePrice = basePrice;
        }

        public void Display()
        {
            Console.WriteLine($"  商品: {Name}");
            Console.WriteLine($"  品牌: {Brand}");
            Console.WriteLine($"  分类: {Category}");
            Console.WriteLine($"  基础价格: ¥{BasePrice:F2}");
            Console.WriteLine($"  图片: {ImageUrl}");
        }
    }

    // 商品实例（享元对象）
    public class ProductInstance
    {
        private ProductBasicInfo basicInfo;  // 共享的内部状态
        
        // 外部状态 - 每个实例独有
        public required string WarehouseId { get; set; }
        public int Stock { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public ProductInstance(ProductBasicInfo basicInfo)
        {
            this.basicInfo = basicInfo;
            this.LastUpdateTime = DateTime.Now;
        }

        public void Display()
        {
            Console.WriteLine($"\n商品实例 [SKU: {basicInfo.SkuId}]");
            basicInfo.Display();
            Console.WriteLine($"  仓库: {WarehouseId}");
            Console.WriteLine($"  库存: {Stock}");
            Console.WriteLine($"  当前价格: ¥{CurrentPrice:F2}");
            Console.WriteLine($"  折扣: {Discount:P0}");
            Console.WriteLine($"  更新时间: {LastUpdateTime:yyyy-MM-dd HH:mm:ss}");
        }

        public decimal GetFinalPrice()
        {
            return CurrentPrice * (1 - Discount);
        }
    }

    // 商品缓存工厂（享元工厂）
    public class ProductCacheFactory
    {
        private static ProductCacheFactory? instance;
        private Dictionary<string, ProductBasicInfo> productCache;
        private int cacheHits = 0;
        private int cacheMisses = 0;

        private ProductCacheFactory()
        {
            productCache = new Dictionary<string, ProductBasicInfo>();
            InitializeCache();
        }

        public static ProductCacheFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductCacheFactory();
            }
            return instance;
        }

        private void InitializeCache()
        {
            Console.WriteLine("[商品缓存] 初始化缓存数据...");
            
            // 预加载一些热门商品
            AddToCache(new ProductBasicInfo(
                "SKU001", "iPhone 15 Pro", "手机", "Apple",
                "https://img.com/iphone15pro.jpg",
                "最新款苹果手机，钛金属机身",
                9999.00m
            ));

            AddToCache(new ProductBasicInfo(
                "SKU002", "MacBook Pro 14", "笔记本", "Apple",
                "https://img.com/macbookpro14.jpg",
                "M3 Pro芯片，专业级笔记本",
                16999.00m
            ));

            AddToCache(new ProductBasicInfo(
                "SKU003", "AirPods Pro", "耳机", "Apple",
                "https://img.com/airpodspro.jpg",
                "主动降噪无线耳机",
                1999.00m
            ));

            AddToCache(new ProductBasicInfo(
                "SKU004", "小米14 Pro", "手机", "Xiaomi",
                "https://img.com/mi14pro.jpg",
                "骁龙8Gen3，徕卡影像",
                4999.00m
            ));

            AddToCache(new ProductBasicInfo(
                "SKU005", "ThinkPad X1 Carbon", "笔记本", "Lenovo",
                "https://img.com/x1carbon.jpg",
                "商务轻薄本，军工品质",
                12999.00m
            ));

            Console.WriteLine($"[商品缓存] 初始化完成，缓存了 {productCache.Count} 个商品");
        }

        private void AddToCache(ProductBasicInfo product)
        {
            productCache[product.SkuId] = product;
        }

        public ProductBasicInfo? GetProduct(string skuId)
        {
            if (productCache.ContainsKey(skuId))
            {
                cacheHits++;
                Console.WriteLine($"[缓存命中] SKU: {skuId} (命中率: {GetCacheHitRate():P1})");
                return productCache[skuId];
            }
            else
            {
                cacheMisses++;
                Console.WriteLine($"[缓存未命中] SKU: {skuId}，从数据库加载...");
                
                // 模拟从数据库加载
                ProductBasicInfo? newProduct = LoadFromDatabase(skuId);
                if (newProduct != null)
                {
                    AddToCache(newProduct);
                    Console.WriteLine($"[缓存更新] 已将 SKU: {skuId} 加入缓存");
                }
                return newProduct;
            }
        }

        private ProductBasicInfo? LoadFromDatabase(string skuId)
        {
            // 模拟数据库查询延迟
            System.Threading.Thread.Sleep(100);
            
            // 模拟数据
            return new ProductBasicInfo(
                skuId,
                $"商品{skuId}",
                "其他",
                "通用品牌",
                $"https://img.com/{skuId}.jpg",
                "商品描述",
                999.00m
            );
        }

        public ProductInstance? CreateProductInstance(string skuId, string warehouseId, int stock)
        {
            ProductBasicInfo? basicInfo = GetProduct(skuId);
            if (basicInfo == null)
            {
                return null;
            }

            ProductInstance instance = new ProductInstance(basicInfo)
            {
                WarehouseId = warehouseId,
                Stock = stock,
                CurrentPrice = basicInfo.BasePrice,
                Discount = 0
            };

            return instance;
        }

        public void ShowCacheStatistics()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("商品缓存统计信息");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"缓存大小: {productCache.Count} 个商品");
            Console.WriteLine($"缓存命中: {cacheHits} 次");
            Console.WriteLine($"缓存未命中: {cacheMisses} 次");
            Console.WriteLine($"命中率: {GetCacheHitRate():P1}");
            Console.WriteLine($"内存节省: 约 {EstimateMemorySaved()} MB");
            Console.WriteLine(new string('=', 60));
        }

        private double GetCacheHitRate()
        {
            int total = cacheHits + cacheMisses;
            return total == 0 ? 0 : (double)cacheHits / total;
        }

        private double EstimateMemorySaved()
        {
            // 假设每个商品基础信息约占 1KB
            // 如果有100个实例共享同一个基础信息，节省99KB
            int estimatedInstances = (cacheHits + cacheMisses) * 10; // 假设平均每个SKU有10个实例
            double savedKB = productCache.Count * 1.0 * (estimatedInstances / productCache.Count - 1);
            return savedKB / 1024; // 转换为MB
        }

        public void ClearCache()
        {
            Console.WriteLine("[商品缓存] 清理缓存...");
            int count = productCache.Count;
            productCache.Clear();
            cacheHits = 0;
            cacheMisses = 0;
            Console.WriteLine($"[商品缓存] 已清理 {count} 个缓存项");
        }
    }

    // 电商平台商品管理器
    public class ECommercePlatform
    {
        private ProductCacheFactory cacheFactory;
        private List<ProductInstance> activeProducts;

        public ECommercePlatform()
        {
            cacheFactory = ProductCacheFactory.GetInstance();
            activeProducts = new List<ProductInstance>();
        }

        public void LoadProductsForWarehouse(string warehouseId)
        {
            Console.WriteLine($"\n加载仓库 {warehouseId} 的商品...");
            
            // 模拟加载多个商品实例，它们共享基础信息
            string[] skus = { "SKU001", "SKU002", "SKU003", "SKU001", "SKU001", "SKU004" };
            Random random = new Random();

            foreach (var sku in skus)
            {
                var instance = cacheFactory.CreateProductInstance(
                    sku, 
                    warehouseId, 
                    random.Next(10, 100)
                );
                
                if (instance != null)
                {
                    // 设置实例特有的属性
                    instance.CurrentPrice = instance.CurrentPrice * (decimal)(0.9 + random.NextDouble() * 0.2);
                    instance.Discount = (decimal)(random.NextDouble() * 0.3);
                    activeProducts.Add(instance);
                }
            }

            Console.WriteLine($"已加载 {activeProducts.Count} 个商品实例");
        }

        public void DisplayActiveProducts()
        {
            Console.WriteLine($"\n当前活跃商品实例 ({activeProducts.Count} 个):");
            foreach (var product in activeProducts)
            {
                product.Display();
                Console.WriteLine($"  最终价格: ¥{product.GetFinalPrice():F2}");
            }
        }

        public void ShowMemoryUsage()
        {
            cacheFactory.ShowCacheStatistics();
            
            // 展示内存优化效果
            Console.WriteLine("\n内存使用对比:");
            int uniqueSkus = activeProducts.Select(p => p.WarehouseId).Distinct().Count();
            Console.WriteLine($"  活跃商品实例: {activeProducts.Count} 个");
            Console.WriteLine($"  共享的基础信息: {uniqueSkus} 个");
            Console.WriteLine($"  节省的对象创建: {activeProducts.Count - uniqueSkus} 个");
        }
    }
}
