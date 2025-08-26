using _19Mediator.Example.MicroserviceCoordinator;

namespace _19Mediator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("================================ 中介者模式 (Mediator Pattern) ================================");
            Console.WriteLine("适用场景：当多个对象之间存在复杂的交互关系，导致依赖关系混乱且难以维护时");
            Console.WriteLine("特点：用一个中介对象来封装一系列对象的交互，使各对象不需要显式地相互引用");
            Console.WriteLine("优点：降低了对象之间的耦合度，可以独立地改变和复用各个对象\n");

            Console.WriteLine("-------------------------------- 微服务协调器示例 ----------------------------------");
            
            // 创建服务协调器（中介者）
            var coordinator = new ServiceCoordinator();
            
            // 创建各个微服务
            var userService = new UserService();
            var orderService = new OrderService();
            var paymentService = new PaymentService();
            var notificationService = new NotificationService();
            
            // 注册服务到协调器
            Console.WriteLine("1. 注册微服务：");
            coordinator.RegisterService(userService);
            coordinator.RegisterService(orderService);
            coordinator.RegisterService(paymentService);
            coordinator.RegisterService(notificationService);
            
            Console.WriteLine("\n2. 执行业务流程：");
            Console.WriteLine("--------------------------------");
            
            // 场景1：创建用户
            Console.WriteLine("\n场景1：创建新用户");
            var userResult = await coordinator.SendRequestAsync<dynamic>(
                "UserService", 
                "CreateUser", 
                new { Id = 1, Name = "张三", Email = "zhangsan@example.com" }
            );
            Console.WriteLine($"用户创建成功: UserId = {userResult.UserId}");
            
            // 场景2：创建订单（会触发一系列服务交互）
            Console.WriteLine("\n场景2：创建订单（触发支付和通知）");
            var orderResult = await coordinator.SendRequestAsync<dynamic>(
                "OrderService",
                "CreateOrder",
                new { OrderId = 1001, UserId = 1, Amount = 299.99m }
            );
            Console.WriteLine($"订单创建成功: OrderId = {orderResult.OrderId}, PaymentId = {orderResult.PaymentId}");
            
            // 显示系统指标
            Console.WriteLine("\n3. 系统运行指标：");
            coordinator.PrintMetrics();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 各个微服务之间没有直接依赖，都通过协调器进行通信");
            Console.WriteLine("- 协调器负责消息路由、事件分发和服务发现");
            Console.WriteLine("- 新增服务只需注册到协调器，不影响现有服务");
        }
    }
}
