namespace _19Mediator.Example.MicroserviceCoordinator
{
    public class MicroserviceExample
    {
        public static async Task Run()
        {
            Console.WriteLine("\n=== 微服务协调器示例 ===");
            
            // 创建服务协调器
            var coordinator = new ServiceCoordinator();
            
            // 注册微服务
            var userService = new UserService();
            var orderService = new OrderService();
            var paymentService = new PaymentService();
            var notificationService = new NotificationService();
            
            coordinator.RegisterService(userService);
            coordinator.RegisterService(orderService);
            coordinator.RegisterService(paymentService);
            coordinator.RegisterService(notificationService);
            
            Console.WriteLine("\n执行业务流程:");
            Console.WriteLine("------------------------");
            
            // 创建用户
            var userResult = await coordinator.SendRequestAsync<dynamic>(
                "UserService", 
                "CreateUser", 
                new { Id = 1, Name = "张三", Email = "zhangsan@example.com" }
            );
            
            Console.WriteLine($"\n用户创建结果: UserId = {userResult.UserId}");
            
            // 创建订单
            var orderResult = await coordinator.SendRequestAsync<dynamic>(
                "OrderService",
                "CreateOrder",
                new { OrderId = 1001, UserId = 1, Amount = 299.99m }
            );
            
            Console.WriteLine($"订单创建结果: OrderId = {orderResult.OrderId}, PaymentId = {orderResult.PaymentId}");
            
            // 显示系统指标
            coordinator.PrintMetrics();
        }
    }
}
