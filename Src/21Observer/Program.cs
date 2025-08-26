namespace _21Observer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 观察者模式 (Observer Pattern) ===\n");

            Console.WriteLine("示例1：股票价格监控");
            Console.WriteLine("------------------------");
            var stock = new Stock("AAPL", 150.00m);
            
            var investor1 = new Investor("张三");
            var investor2 = new Investor("李四");
            var alertSystem = new PriceAlertSystem();
            
            stock.Attach(investor1);
            stock.Attach(investor2);
            stock.Attach(alertSystem);
            
            stock.Price = 155.00m;
            stock.Price = 145.00m;
            
            stock.Detach(investor2);
            stock.Price = 160.00m;

            Console.WriteLine("\n示例2：订单状态更新");
            Console.WriteLine("------------------------");
            var order = new Order("ORD-001");
            
            var customer = new Customer("王五");
            var warehouse = new Warehouse();
            var emailService = new EmailService();
            
            order.Subscribe(customer);
            order.Subscribe(warehouse);
            order.Subscribe(emailService);
            
            order.UpdateStatus(OrderStatus.Confirmed);
            order.UpdateStatus(OrderStatus.Shipped);
            order.UpdateStatus(OrderStatus.Delivered);
        }
    }
}
