using _13Proxy._01ImplementationMethod;
using _13Proxy._02Example._01RemoteService;
using _13Proxy._02Example._02ImageLoader;
using _13Proxy._02Example._03SecurityProxy;

namespace _13Proxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 代理模式 Proxy Pattern ===\n");

            // 基础实现
            Console.WriteLine("1. 基础实现：");
            Console.WriteLine("\n保护代理示例:");
            ISubject proxy = new Proxy();
            proxy.Request();
            
            Console.WriteLine("\n虚拟代理示例:");
            IExpensiveObject virtualProxy = new VirtualProxy();
            Console.WriteLine("代理已创建，但对象未初始化");
            Console.WriteLine("第一次调用:");
            virtualProxy.Process();
            Console.WriteLine("第二次调用:");
            virtualProxy.Process();

            // 远程服务代理示例
            Console.WriteLine("\n2. 远程服务代理示例（带缓存）：");
            Console.WriteLine(new string('-', 60));
            
            CachedProductServiceProxy serviceProxy = new CachedProductServiceProxy();
            
            // 第一次查询（缓存未命中）
            var product1 = serviceProxy.GetProduct("P001");
            
            // 第二次查询同一商品（缓存命中）
            var product2 = serviceProxy.GetProduct("P001");
            
            // 查询分类商品
            var phones = serviceProxy.GetProductsByCategory("手机");
            
            // 更新库存（会清除缓存）
            serviceProxy.UpdateStock("P001", 10);
            
            // 再次查询（缓存已清除，需要重新加载）
            var product3 = serviceProxy.GetProduct("P001");
            
            // 显示缓存统计
            serviceProxy.ShowCacheStatistics();

            // 图片加载代理示例
            Console.WriteLine("\n3. 图片延迟加载代理示例：");
            Console.WriteLine(new string('-', 60));
            
            // 创建图片库（使用代理）
            ImageGallery gallery = new ImageGallery(useProxy: true);
            
            // 添加图片（只创建代理，不加载实际图片）
            Console.WriteLine("\n添加图片到图片库:");
            gallery.AddImage("photo1.jpg");
            gallery.AddImage("photo2.png");
            gallery.AddImage("photo3.gif");
            
            // 显示图片库信息（不会触发加载）
            gallery.ShowGalleryInfo();
            
            // 显示特定图片（触发延迟加载）
            gallery.DisplayImage(0);
            
            // 再次显示同一图片（使用缓存）
            gallery.DisplayImage(0);
            
            // 预加载一些图片
            SmartImageProxy.PreloadImages("banner.jpg", "logo.png");
            
            // 显示缓存状态
            SmartImageProxy.ShowCacheStatus();

            // 安全代理示例
            Console.WriteLine("\n4. 安全代理示例：");
            Console.WriteLine(new string('-', 60));
            
            // 创建文档
            Document doc = new Document("DOC001", "机密文档", "这是机密内容", "管理员");
            
            // 创建不同权限的用户
            var guestUser = new UserContext 
            { 
                UserId = "U001", 
                UserName = "访客", 
                Role = UserRole.Guest,
                Permissions = new List<string>()
            };
            
            var normalUser = new UserContext 
            { 
                UserId = "U002", 
                UserName = "普通用户", 
                Role = UserRole.User,
                Permissions = new List<string>()
            };
            
            var adminUser = new UserContext 
            { 
                UserId = "U003", 
                UserName = "管理员", 
                Role = UserRole.Admin,
                Permissions = new List<string>()
            };
            
            // 访客访问文档
            Console.WriteLine("\n访客尝试访问文档:");
            var guestProxy = new SecureDocumentProxy(doc, guestUser);
            try
            {
                string content = guestProxy.Read(); // 成功
                Console.WriteLine($"  读取内容: {content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  错误: {ex.Message}");
            }
            
            try
            {
                guestProxy.Write("新内容"); // 失败
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  错误: {ex.Message}");
            }
            
            // 管理员访问文档
            Console.WriteLine("\n管理员访问文档:");
            var adminProxy = new SecureDocumentProxy(doc, adminUser);
            try
            {
                adminProxy.Write("管理员修改的内容"); // 成功
                adminProxy.Delete(); // 成功
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  错误: {ex.Message}");
            }
            
            // 显示审计日志
            adminProxy.ShowAuditLog();
            
            // 数据库安全代理示例
            Console.WriteLine("\n数据库安全代理示例:");
            Console.WriteLine(new string('-', 60));
            
            Database db = new Database();
            var dbProxy = new SecureDatabaseProxy(db, normalUser);
            
            try
            {
                // 普通用户执行查询
                var results = dbProxy.Query("SELECT * FROM orders");
                Console.WriteLine($"查询结果: {results.Count} 条记录");
                
                // 尝试执行危险操作
                dbProxy.Execute("DROP TABLE users"); // 会被拒绝
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作被拒绝: {ex.Message}");
            }

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("代理模式的优势：");
            Console.WriteLine("1. 远程代理：隐藏对象位于不同地址空间的事实");
            Console.WriteLine("2. 虚拟代理：延迟创建开销大的对象");
            Console.WriteLine("3. 保护代理：控制对原始对象的访问权限");
            Console.WriteLine("4. 缓存代理：为开销大的运算结果提供缓存");
            Console.WriteLine(new string('=', 60));
            Console.ReadLine();
        }
    }
}