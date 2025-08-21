using _12Flyweight._01ImplementationMethod;
using _12Flyweight._02Example._01ProductCache;
using _12Flyweight._02Example._02ConnectionPool;

namespace _12Flyweight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 享元模式 Flyweight Pattern ===\n");

            // 基础实现
            Console.WriteLine("1. 基础实现：");
            FlyweightFactory factory = new FlyweightFactory();
            
            // 获取享元对象
            Flyweight fx = factory.GetFlyweight("X");
            fx.Operation("第一次调用");
            
            Flyweight fy = factory.GetFlyweight("Y");
            fy.Operation("第二次调用");
            
            Flyweight fz = factory.GetFlyweight("Z");
            fz.Operation("第三次调用");
            
            // 复用已有对象
            Flyweight fx2 = factory.GetFlyweight("X");
            fx2.Operation("第四次调用");
            
            // 创建非共享对象
            UnsharedConcreteFlyweight fu = new UnsharedConcreteFlyweight("U");
            fu.Operation("非共享对象");
            
            Console.WriteLine();
            factory.ListFlyweights();

            // 电商商品缓存示例
            Console.WriteLine("\n2. 电商商品缓存系统示例：");
            Console.WriteLine(new string('-', 60));
            
            ECommercePlatform platform = new ECommercePlatform();
            
            // 加载北京仓的商品
            platform.LoadProductsForWarehouse("北京仓");
            
            // 加载上海仓的商品（会复用已缓存的商品基础信息）
            platform.LoadProductsForWarehouse("上海仓");
            
            // 显示活跃商品
            platform.DisplayActiveProducts();
            
            // 显示内存使用情况
            platform.ShowMemoryUsage();

            // 数据库连接池示例
            Console.WriteLine("\n3. 数据库连接池示例：");
            Console.WriteLine(new string('-', 60));
            
            DatabaseManager dbManager = new DatabaseManager();
            
            // 单用户操作
            Console.WriteLine("\n单用户操作演示:");
            dbManager.ExecuteQuery("admin", "主库", "SELECT * FROM users WHERE id = 1");
            dbManager.ExecuteQuery("admin", "从库", "SELECT * FROM products LIMIT 10");
            dbManager.ExecuteQuery("admin", "缓存", "GET user:1:profile");
            
            // 多用户并发操作
            Console.WriteLine("\n多用户并发操作演示:");
            dbManager.SimulateMultipleUsers();
            
            // 显示连接池统计
            dbManager.ShowStatistics();
            
            // 清理过期连接
            dbManager.Cleanup();
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("享元模式的优势：");
            Console.WriteLine("1. 减少内存使用：通过共享相似对象减少内存占用");
            Console.WriteLine("2. 提高性能：避免重复创建相同的对象");
            Console.WriteLine("3. 适用场景：大量相似对象、对象创建成本高、内存受限环境");
            Console.WriteLine(new string('=', 60));
            Console.ReadLine();
        }
    }
}