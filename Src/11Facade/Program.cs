using _Facade._02Example.HomeTheater;
using _Facade._02Example.ECommerce;

namespace _11Facade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 外观模式 (Facade Pattern) ================================");
            Console.WriteLine("适用场景：需要为复杂子系统提供简单接口；需要将子系统与客户端解耦");
            Console.WriteLine("特点：为子系统中的一组接口提供一个一致的界面，定义了一个高层接口");
            Console.WriteLine("优点：简化了客户端调用；减少了客户端与子系统的依赖；提高了子系统的独立性\n");

            Console.WriteLine("-------------------------------- 家庭影院系统 ----------------------------------");
            
            // 创建家庭影院外观
            var homeTheater = new HomeTheaterFacade();
            
            Console.WriteLine("1. 看电影：");
            homeTheater.WatchMovie("复仇者联盟");
            
            Console.WriteLine("\n2. 结束电影：");
            homeTheater.EndMovie();
            
            Console.WriteLine("\n3. 听音乐：");
            homeTheater.ListenToMusic("放松音乐合集");
            
            Console.WriteLine("\n4. 关闭系统：");
            homeTheater.ShutDown();

            Console.WriteLine("\n-------------------------------- 电商下单系统 ----------------------------------");
            
            // 创建电商外观
            var ecommerce = new ECommerceFacade();
            
            Console.WriteLine("1. 处理订单 #001：");
            var success = ecommerce.PlaceOrder("user123", "iPhone 15", 2, "北京市朝阳区");
            Console.WriteLine($"   订单状态: {(success ? "成功" : "失败")}");
            
            Console.WriteLine("\n2. 处理订单 #002（库存不足）：");
            success = ecommerce.PlaceOrder("user456", "MacBook Pro", 100, "上海市浦东新区");
            Console.WriteLine($"   订单状态: {(success ? "成功" : "失败")}");
            
            Console.WriteLine("\n3. 查询订单状态：");
            var status = ecommerce.GetOrderStatus("ORD-001");
            Console.WriteLine($"   订单 ORD-001: {status}");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 外观类封装了复杂的子系统调用");
            Console.WriteLine("- 客户端只需要与外观类交互，不需要了解子系统细节");
            Console.WriteLine("- 子系统类可以独立变化，不影响客户端");
        }
    }
}
