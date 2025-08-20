using System.Collections.Generic;
using System.Text.Json;

namespace _13Proxy._02Example._01RemoteService
{
    // API响应数据模型
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    // 商品数据模型
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public DateTime LastUpdated { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} - ¥{Price:F2} (库存: {Stock})";
        }
    }

    // 订单数据模型
    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<string> ProductIds { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
    }

    // 远程服务接口
    public interface IProductService
    {
        Product GetProduct(string productId);
        List<Product> GetProductsByCategory(string category);
        bool UpdateStock(string productId, int quantity);
        Order CreateOrder(string customerId, List<string> productIds);
    }

    // 真实的远程服务（模拟）
    public class RemoteProductService : IProductService
    {
        private Dictionary<string, Product> products;
        private Random random = new Random();

        public RemoteProductService()
        {
            InitializeProducts();
        }

        private void InitializeProducts()
        {
            products = new Dictionary<string, Product>
            {
                ["P001"] = new Product { Id = "P001", Name = "iPhone 15", Price = 8999, Stock = 100, Category = "手机" },
                ["P002"] = new Product { Id = "P002", Name = "MacBook Pro", Price = 16999, Stock = 50, Category = "电脑" },
                ["P003"] = new Product { Id = "P003", Name = "AirPods Pro", Price = 1999, Stock = 200, Category = "耳机" },
                ["P004"] = new Product { Id = "P004", Name = "iPad Pro", Price = 9299, Stock = 80, Category = "平板" },
                ["P005"] = new Product { Id = "P005", Name = "Apple Watch", Price = 3199, Stock = 150, Category = "手表" }
            };
        }

        public Product GetProduct(string productId)
        {
            Console.WriteLine($"[远程服务] 查询商品: {productId}");
            
            // 模拟网络延迟
            System.Threading.Thread.Sleep(500);
            
            if (products.ContainsKey(productId))
            {
                var product = products[productId];
                product.LastUpdated = DateTime.Now;
                Console.WriteLine($"[远程服务] 返回商品数据: {product}");
                return product;
            }
            
            Console.WriteLine($"[远程服务] 商品不存在: {productId}");
            return null;
        }

        public List<Product> GetProductsByCategory(string category)
        {
            Console.WriteLine($"[远程服务] 查询分类商品: {category}");
            
            // 模拟网络延迟
            System.Threading.Thread.Sleep(800);
            
            var result = products.Values.Where(p => p.Category == category).ToList();
            Console.WriteLine($"[远程服务] 返回 {result.Count} 个商品");
            return result;
        }

        public bool UpdateStock(string productId, int quantity)
        {
            Console.WriteLine($"[远程服务] 更新库存: {productId}, 数量: {quantity}");
            
            // 模拟网络延迟
            System.Threading.Thread.Sleep(300);
            
            if (products.ContainsKey(productId))
            {
                products[productId].Stock += quantity;
                Console.WriteLine($"[远程服务] 库存更新成功，当前库存: {products[productId].Stock}");
                return true;
            }
            
            Console.WriteLine($"[远程服务] 更新失败，商品不存在");
            return false;
        }

        public Order CreateOrder(string customerId, List<string> productIds)
        {
            Console.WriteLine($"[远程服务] 创建订单，客户: {customerId}");
            
            // 模拟网络延迟
            System.Threading.Thread.Sleep(1000);
            
            decimal totalAmount = 0;
            foreach (var productId in productIds)
            {
                if (products.ContainsKey(productId))
                {
                    totalAmount += products[productId].Price;
                }
            }

            var order = new Order
            {
                OrderId = $"ORD{DateTime.Now:yyyyMMddHHmmss}{random.Next(1000, 9999)}",
                CustomerId = customerId,
                ProductIds = productIds,
                TotalAmount = totalAmount,
                Status = "已创建",
                CreateTime = DateTime.Now
            };

            Console.WriteLine($"[远程服务] 订单创建成功: {order.OrderId}");
            return order;
        }
    }

    // 带缓存的远程服务代理
    public class CachedProductServiceProxy : IProductService
    {
        private RemoteProductService remoteService;
        private Dictionary<string, CacheEntry<Product>> productCache;
        private Dictionary<string, CacheEntry<List<Product>>> categoryCache;
        private TimeSpan cacheExpiration = TimeSpan.FromMinutes(5);
        private int cacheHits = 0;
        private int cacheMisses = 0;

        private class CacheEntry<T>
        {
            public T Data { get; set; }
            public DateTime ExpireTime { get; set; }
            
            public bool IsExpired => DateTime.Now > ExpireTime;
        }

        public CachedProductServiceProxy()
        {
            remoteService = new RemoteProductService();
            productCache = new Dictionary<string, CacheEntry<Product>>();
            categoryCache = new Dictionary<string, CacheEntry<List<Product>>>();
        }

        public Product GetProduct(string productId)
        {
            Console.WriteLine($"\n[代理] 获取商品: {productId}");
            
            // 检查缓存
            if (productCache.ContainsKey(productId) && !productCache[productId].IsExpired)
            {
                cacheHits++;
                Console.WriteLine($"[代理] ✓ 缓存命中 (命中率: {GetCacheHitRate():P1})");
                return productCache[productId].Data;
            }

            // 缓存未命中或已过期
            cacheMisses++;
            Console.WriteLine($"[代理] ✗ 缓存未命中，调用远程服务...");
            
            var product = remoteService.GetProduct(productId);
            
            if (product != null)
            {
                // 更新缓存
                productCache[productId] = new CacheEntry<Product>
                {
                    Data = product,
                    ExpireTime = DateTime.Now.Add(cacheExpiration)
                };
                Console.WriteLine($"[代理] 已更新缓存，过期时间: {productCache[productId].ExpireTime:HH:mm:ss}");
            }
            
            return product;
        }

        public List<Product> GetProductsByCategory(string category)
        {
            Console.WriteLine($"\n[代理] 获取分类商品: {category}");
            
            // 检查缓存
            if (categoryCache.ContainsKey(category) && !categoryCache[category].IsExpired)
            {
                cacheHits++;
                Console.WriteLine($"[代理] ✓ 缓存命中 (命中率: {GetCacheHitRate():P1})");
                return categoryCache[category].Data;
            }

            // 缓存未命中或已过期
            cacheMisses++;
            Console.WriteLine($"[代理] ✗ 缓存未命中，调用远程服务...");
            
            var products = remoteService.GetProductsByCategory(category);
            
            // 更新缓存
            categoryCache[category] = new CacheEntry<List<Product>>
            {
                Data = products,
                ExpireTime = DateTime.Now.Add(cacheExpiration)
            };
            Console.WriteLine($"[代理] 已更新缓存，过期时间: {categoryCache[category].ExpireTime:HH:mm:ss}");
            
            return products;
        }

        public bool UpdateStock(string productId, int quantity)
        {
            Console.WriteLine($"\n[代理] 更新库存: {productId}");
            
            // 直接调用远程服务（写操作不使用缓存）
            var result = remoteService.UpdateStock(productId, quantity);
            
            if (result)
            {
                // 清除相关缓存
                if (productCache.ContainsKey(productId))
                {
                    productCache.Remove(productId);
                    Console.WriteLine($"[代理] 已清除商品 {productId} 的缓存");
                }
                
                // 清除所有分类缓存（因为商品库存变化可能影响分类查询结果）
                categoryCache.Clear();
                Console.WriteLine("[代理] 已清除所有分类缓存");
            }
            
            return result;
        }

        public Order CreateOrder(string customerId, List<string> productIds)
        {
            Console.WriteLine($"\n[代理] 创建订单");
            
            // 预先验证商品是否存在（使用缓存）
            foreach (var productId in productIds)
            {
                var product = GetProduct(productId);
                if (product == null)
                {
                    Console.WriteLine($"[代理] 订单创建失败：商品 {productId} 不存在");
                    return null;
                }
            }
            
            // 调用远程服务创建订单
            return remoteService.CreateOrder(customerId, productIds);
        }

        private double GetCacheHitRate()
        {
            int total = cacheHits + cacheMisses;
            return total == 0 ? 0 : (double)cacheHits / total;
        }

        public void ShowCacheStatistics()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("缓存统计信息");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"商品缓存项: {productCache.Count}");
            Console.WriteLine($"分类缓存项: {categoryCache.Count}");
            Console.WriteLine($"缓存命中: {cacheHits}");
            Console.WriteLine($"缓存未命中: {cacheMisses}");
            Console.WriteLine($"命中率: {GetCacheHitRate():P1}");
            Console.WriteLine($"缓存过期时间: {cacheExpiration.TotalMinutes} 分钟");
            Console.WriteLine(new string('=', 60));
        }

        public void ClearCache()
        {
            productCache.Clear();
            categoryCache.Clear();
            Console.WriteLine("[代理] 缓存已清空");
        }
    }

    // 带重试机制的代理
    public class RetryableProductServiceProxy : IProductService
    {
        private IProductService innerService;
        private int maxRetries = 3;
        private TimeSpan retryDelay = TimeSpan.FromSeconds(1);

        public RetryableProductServiceProxy(IProductService service)
        {
            innerService = service;
        }

        public Product GetProduct(string productId)
        {
            return ExecuteWithRetry(() => innerService.GetProduct(productId), "GetProduct");
        }

        public List<Product> GetProductsByCategory(string category)
        {
            return ExecuteWithRetry(() => innerService.GetProductsByCategory(category), "GetProductsByCategory");
        }

        public bool UpdateStock(string productId, int quantity)
        {
            return ExecuteWithRetry(() => innerService.UpdateStock(productId, quantity), "UpdateStock");
        }

        public Order CreateOrder(string customerId, List<string> productIds)
        {
            return ExecuteWithRetry(() => innerService.CreateOrder(customerId, productIds), "CreateOrder");
        }

        private T ExecuteWithRetry<T>(Func<T> operation, string operationName)
        {
            int attempt = 0;
            while (attempt < maxRetries)
            {
                try
                {
                    attempt++;
                    Console.WriteLine($"[重试代理] 执行 {operationName} (尝试 {attempt}/{maxRetries})");
                    return operation();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[重试代理] 操作失败: {ex.Message}");
                    
                    if (attempt < maxRetries)
                    {
                        Console.WriteLine($"[重试代理] 等待 {retryDelay.TotalSeconds} 秒后重试...");
                        System.Threading.Thread.Sleep(retryDelay);
                    }
                    else
                    {
                        Console.WriteLine($"[重试代理] 已达到最大重试次数，操作失败");
                        throw;
                    }
                }
            }
            
            return default(T);
        }
    }
}
