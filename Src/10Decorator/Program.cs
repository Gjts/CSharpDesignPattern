using _10Decorator._01ImplementationMethod;
using _10Decorator._02Example;

namespace _10Decorator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 装饰器模式 Decorator Pattern ===\n");

            // 基础实现
            Console.WriteLine("1. 基础实现：");
            ConcreteComponent component = new ConcreteComponent();
            
            ConcreteDecoratorA decoratorA = new ConcreteDecoratorA();
            ConcreteDecoratorB decoratorB = new ConcreteDecoratorB();
            
            decoratorA.SetComponent(component);
            decoratorB.SetComponent(decoratorA);
            
            decoratorB.Operation();

            // 电商订单处理示例
            Console.WriteLine("\n2. 电商订单处理示例：");
            
            // 创建测试订单
            Order order1 = new Order
            {
                OrderId = "ORD202401001",
                CustomerId = "CUST123456",
                TotalAmount = 2580.00m,
                Items = new List<string> { "iPhone 15", "AirPods Pro" }
            };

            Console.WriteLine($"\n处理普通订单 - 订单号: {order1.OrderId}");
            Console.WriteLine("=" + new string('=', 50));
            
            // 构建订单处理链
            IOrderProcessor processor = new BasicOrderProcessor();
            processor = new InventoryCheckDecorator(processor);
            processor = new PaymentValidationDecorator(processor);
            processor = new CouponProcessorDecorator(processor, 0.15m); // 15%折扣
            processor = new LogisticsAllocationDecorator(processor);
            processor = new NotificationDecorator(processor);
            
            Console.WriteLine($"处理流程: {processor.GetDescription()}\n");
            Order processedOrder1 = processor.Process(order1);
            Console.WriteLine($"\n最终状态: {processedOrder1.Status}");

            // 创建高风险订单
            Console.WriteLine("\n" + new string('-', 60));
            Order order2 = new Order
            {
                OrderId = "ORD202401002",
                CustomerId = "NEW789012",  // 新客户
                TotalAmount = 15888.00m,    // 高金额
                Items = new List<string> { "MacBook Pro", "iPad Pro", "Apple Watch" },
                CreateTime = new DateTime(2024, 1, 1, 2, 30, 0)  // 凌晨订单
            };

            Console.WriteLine($"\n处理高风险订单 - 订单号: {order2.OrderId}");
            Console.WriteLine("=" + new string('=', 50));
            
            // 构建包含风控的订单处理链
            IOrderProcessor riskProcessor = new BasicOrderProcessor();
            riskProcessor = new RiskControlDecorator(riskProcessor);  // 先进行风控检查
            riskProcessor = new InventoryCheckDecorator(riskProcessor);
            riskProcessor = new PaymentValidationDecorator(riskProcessor);
            riskProcessor = new LogisticsAllocationDecorator(riskProcessor);
            riskProcessor = new NotificationDecorator(riskProcessor);
            
            Console.WriteLine($"处理流程: {riskProcessor.GetDescription()}\n");
            Order processedOrder2 = riskProcessor.Process(order2);
            Console.WriteLine($"\n最终状态: {processedOrder2.Status}");
            
            // 显示订单扩展信息
            Console.WriteLine("\n订单扩展信息:");
            foreach (var info in processedOrder2.ExtendedInfo)
            {
                Console.WriteLine($"  {info.Key}: {info.Value}");
            }
        }
    }
}