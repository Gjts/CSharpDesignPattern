using _13Proxy._02Example._02ImageLoader;
using _13Proxy._02Example._03SecurityProxy;

namespace _13Proxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 代理模式 (Proxy Pattern) ================================");
            Console.WriteLine("适用场景：需要为对象提供代理以控制对它的访问；需要在访问对象时执行额外操作");
            Console.WriteLine("特点：为其他对象提供一种代理以控制对这个对象的访问");
            Console.WriteLine("优点：职责清晰；高扩展性；智能化\n");

            Console.WriteLine("-------------------------------- 图片加载系统（虚拟代理） ----------------------------------");
            
            // 创建图片代理
            Console.WriteLine("1. 创建图片代理（延迟加载）：");
            IImage image1 = new ImageProxy("photo1.jpg");
            IImage image2 = new ImageProxy("photo2.jpg");
            IImage image3 = new ImageProxy("photo3.jpg");
            Console.WriteLine("   图片代理创建完成（未加载实际图片）");
            
            // 显示图片（触发加载）
            Console.WriteLine("\n2. 第一次显示图片：");
            image1.Display();
            
            Console.WriteLine("\n3. 第二次显示同一图片（使用缓存）：");
            image1.Display();
            
            Console.WriteLine("\n4. 显示其他图片：");
            image2.Display();

            Console.WriteLine("\n-------------------------------- 数据库访问系统（保护代理） ----------------------------------");
            
            // 创建数据库代理
            var dbProxy = new DatabaseProxy();
            
            // 管理员访问
            Console.WriteLine("1. 管理员访问：");
            dbProxy.SetUser("admin", "Admin");
            dbProxy.Query("SELECT * FROM users");
            dbProxy.Execute("DELETE FROM logs WHERE date < '2024-01-01'");
            
            // 普通用户访问
            Console.WriteLine("\n2. 普通用户访问：");
            dbProxy.SetUser("user001", "User");
            dbProxy.Query("SELECT * FROM products");
            dbProxy.Execute("UPDATE products SET price = 100");  // 将被拒绝
            
            // 访客访问
            Console.WriteLine("\n3. 访客访问：");
            dbProxy.SetUser("guest", "Guest");
            dbProxy.Query("SELECT * FROM public_info");
            dbProxy.Execute("INSERT INTO orders VALUES(...)");  // 将被拒绝
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 虚拟代理：延迟创建开销大的对象");
            Console.WriteLine("- 保护代理：控制对原始对象的访问权限");
            Console.WriteLine("- 代理和真实对象实现相同接口，客户端无需区分");
        }
    }
}
