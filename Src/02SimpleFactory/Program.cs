using _SimpleFactory._02Example.Logistics;
using _SimpleFactory._02Example.Payment;

namespace _02SimpleFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 简单工厂模式 (Simple Factory Pattern) ================================");
            Console.WriteLine("适用场景：当需要根据参数创建不同类型的对象，且对象创建逻辑相对简单时");
            Console.WriteLine("特点：由一个工厂类根据传入的参数决定创建哪一种产品类的实例");
            Console.WriteLine("优点：客户端无需知道具体产品类名，只需知道参数即可；便于扩展，增加产品只需修改工厂类\n");

            Console.WriteLine("-------------------------------- 物流运输示例 ----------------------------------");
            
            // 创建不同的运输方式
            var transport1 = TransportFactory.CreateTransport(TransportType.Truck);
            transport1.Deliver("北京", "上海", "电子产品100件");
            
            var transport2 = TransportFactory.CreateTransport(TransportType.Ship);
            transport2.Deliver("上海", "洛杉矶", "集装箱货物");
            
            var transport3 = TransportFactory.CreateTransport(TransportType.Airplane);
            transport3.Deliver("北京", "纽约", "紧急文件");

            Console.WriteLine("\n-------------------------------- 支付方式示例 ----------------------------------");
            
            // 创建不同的支付方式
            var payment1 = PaymentFactory.CreatePayment(PaymentType.CreditCard);
            payment1.ProcessPayment(1000.00m, "ORDER-001");
            
            var payment2 = PaymentFactory.CreatePayment(PaymentType.Alipay);
            payment2.ProcessPayment(500.00m, "ORDER-002");
            
            var payment3 = PaymentFactory.CreatePayment(PaymentType.WeChatPay);
            payment3.ProcessPayment(288.88m, "ORDER-003");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 工厂类封装了对象创建逻辑，客户端只需传入类型参数");
            Console.WriteLine("- 新增产品类型只需修改工厂类，符合开闭原则");
            Console.WriteLine("- 适用于产品种类相对较少且稳定的场景");
        }
    }
}
