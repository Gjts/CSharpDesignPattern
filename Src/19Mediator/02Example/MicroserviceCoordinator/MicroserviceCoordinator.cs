namespace _19Mediator.Example.MicroserviceCoordinator
{
    // 微服务协调器接口
    public interface IServiceCoordinator
    {
        void RegisterService(IMicroservice service);
        Task<T> SendRequestAsync<T>(string serviceName, string operation, object data);
        void PublishEvent(string eventName, object eventData);
        void SubscribeToEvent(string eventName, string serviceName);
    }

    // 微服务接口
    public interface IMicroservice
    {
        string ServiceName { get; }
        Task<object> HandleRequestAsync(string operation, object data);
        void HandleEvent(string eventName, object eventData);
        void SetCoordinator(IServiceCoordinator coordinator);
    }

    // 服务协调器实现
    public class ServiceCoordinator : IServiceCoordinator
    {
        private Dictionary<string, IMicroservice> _services = new();
        private Dictionary<string, List<string>> _eventSubscriptions = new();
        private List<string> _requestLog = new();

        public void RegisterService(IMicroservice service)
        {
            _services[service.ServiceName] = service;
            service.SetCoordinator(this);
            Console.WriteLine($"[协调器] 注册服务: {service.ServiceName}");
        }

        public async Task<T> SendRequestAsync<T>(string serviceName, string operation, object data)
        {
            Console.WriteLine($"[协调器] 路由请求: {operation} -> {serviceName}");
            _requestLog.Add($"{DateTime.Now:HH:mm:ss} - {operation} -> {serviceName}");

            if (_services.ContainsKey(serviceName))
            {
                var result = await _services[serviceName].HandleRequestAsync(operation, data);
                return (T)result;
            }

            throw new InvalidOperationException($"服务 {serviceName} 未注册");
        }

        public void PublishEvent(string eventName, object eventData)
        {
            Console.WriteLine($"[协调器] 发布事件: {eventName}");

            if (_eventSubscriptions.ContainsKey(eventName))
            {
                foreach (var serviceName in _eventSubscriptions[eventName])
                {
                    if (_services.ContainsKey(serviceName))
                    {
                        _services[serviceName].HandleEvent(eventName, eventData);
                    }
                }
            }
        }

        public void SubscribeToEvent(string eventName, string serviceName)
        {
            if (!_eventSubscriptions.ContainsKey(eventName))
            {
                _eventSubscriptions[eventName] = new List<string>();
            }

            _eventSubscriptions[eventName].Add(serviceName);
            Console.WriteLine($"[协调器] {serviceName} 订阅事件: {eventName}");
        }

        public void PrintMetrics()
        {
            Console.WriteLine("\n[协调器] 系统指标:");
            Console.WriteLine($"  注册服务数: {_services.Count}");
            Console.WriteLine($"  事件订阅数: {_eventSubscriptions.Count}");
            Console.WriteLine($"  请求处理数: {_requestLog.Count}");
        }
    }

    // 用户服务
    public class UserService : IMicroservice
    {
        private IServiceCoordinator? _coordinator;
        private Dictionary<int, User> _users = new();

        public string ServiceName => "UserService";

        public void SetCoordinator(IServiceCoordinator coordinator)
        {
            _coordinator = coordinator;
            _coordinator.SubscribeToEvent("OrderCreated", ServiceName);
        }

        public async Task<object> HandleRequestAsync(string operation, object data)
        {
            Console.WriteLine($"  [用户服务] 处理请求: {operation}");

            switch (operation)
            {
                case "CreateUser":
                    var userData = (dynamic)data;
                    var user = new User { Id = userData.Id, Name = userData.Name, Email = userData.Email };
                    _users[user.Id] = user;
                    
                    // 发布用户创建事件
                    _coordinator?.PublishEvent("UserCreated", new { UserId = user.Id, UserName = user.Name });
                    
                    return await Task.FromResult(new { Success = true, UserId = user.Id });

                case "GetUser":
                    var userId = (int)data;
                    if (_users.ContainsKey(userId))
                    {
                        return await Task.FromResult(_users[userId]);
                    }
                    return await Task.FromResult<object>(null);

                default:
                    return await Task.FromResult(new { Error = "Unknown operation" });
            }
        }

        public void HandleEvent(string eventName, object eventData)
        {
            Console.WriteLine($"  [用户服务] 处理事件: {eventName}");
            
            if (eventName == "OrderCreated")
            {
                var orderData = (dynamic)eventData;
                Console.WriteLine($"    用户 {orderData.UserId} 创建了订单 {orderData.OrderId}");
            }
        }

        private class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }

    // 订单服务
    public class OrderService : IMicroservice
    {
        private IServiceCoordinator? _coordinator;
        private List<Order> _orders = new();

        public string ServiceName => "OrderService";

        public void SetCoordinator(IServiceCoordinator coordinator)
        {
            _coordinator = coordinator;
            _coordinator.SubscribeToEvent("PaymentCompleted", ServiceName);
        }

        public async Task<object> HandleRequestAsync(string operation, object data)
        {
            Console.WriteLine($"  [订单服务] 处理请求: {operation}");

            switch (operation)
            {
                case "CreateOrder":
                    var orderData = (dynamic)data;
                    var order = new Order 
                    { 
                        Id = orderData.OrderId,
                        UserId = orderData.UserId,
                        Amount = orderData.Amount,
                        Status = "Pending"
                    };
                    _orders.Add(order);
                    
                    // 发布订单创建事件
                    _coordinator?.PublishEvent("OrderCreated", new { OrderId = order.Id, UserId = order.UserId });
                    
                    // 调用支付服务
                    var paymentResult = await _coordinator.SendRequestAsync<dynamic>(
                        "PaymentService", 
                        "ProcessPayment", 
                        new { OrderId = order.Id, Amount = order.Amount }
                    );
                    
                    return new { Success = true, OrderId = order.Id, PaymentId = paymentResult.PaymentId };

                default:
                    return await Task.FromResult(new { Error = "Unknown operation" });
            }
        }

        public void HandleEvent(string eventName, object eventData)
        {
            Console.WriteLine($"  [订单服务] 处理事件: {eventName}");
            
            if (eventName == "PaymentCompleted")
            {
                var paymentData = (dynamic)eventData;
                var order = _orders.FirstOrDefault(o => o.Id == paymentData.OrderId);
                if (order != null)
                {
                    order.Status = "Completed";
                    Console.WriteLine($"    订单 {order.Id} 支付完成");
                }
            }
        }

        private class Order
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public decimal Amount { get; set; }
            public string Status { get; set; }
        }
    }

    // 支付服务
    public class PaymentService : IMicroservice
    {
        private IServiceCoordinator? _coordinator;

        public string ServiceName => "PaymentService";

        public void SetCoordinator(IServiceCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        public async Task<object> HandleRequestAsync(string operation, object data)
        {
            Console.WriteLine($"  [支付服务] 处理请求: {operation}");

            if (operation == "ProcessPayment")
            {
                var paymentData = (dynamic)data;
                
                // 模拟支付处理
                await Task.Delay(100);
                
                var paymentId = Guid.NewGuid().ToString().Substring(0, 8);
                Console.WriteLine($"    处理支付: 订单 {paymentData.OrderId}, 金额 {paymentData.Amount}");
                
                // 发布支付完成事件
                _coordinator?.PublishEvent("PaymentCompleted", new { OrderId = paymentData.OrderId, PaymentId = paymentId });
                
                return new { Success = true, PaymentId = paymentId };
            }

            return await Task.FromResult(new { Error = "Unknown operation" });
        }

        public void HandleEvent(string eventName, object eventData)
        {
            Console.WriteLine($"  [支付服务] 处理事件: {eventName}");
        }
    }

    // 通知服务
    public class NotificationService : IMicroservice
    {
        private IServiceCoordinator? _coordinator;

        public string ServiceName => "NotificationService";

        public void SetCoordinator(IServiceCoordinator coordinator)
        {
            _coordinator = coordinator;
            // 订阅所有事件
            _coordinator.SubscribeToEvent("UserCreated", ServiceName);
            _coordinator.SubscribeToEvent("OrderCreated", ServiceName);
            _coordinator.SubscribeToEvent("PaymentCompleted", ServiceName);
        }

        public async Task<object> HandleRequestAsync(string operation, object data)
        {
            return await Task.FromResult(new { Success = true });
        }

        public void HandleEvent(string eventName, object eventData)
        {
            Console.WriteLine($"  [通知服务] 发送通知: {eventName}");
            
            switch (eventName)
            {
                case "UserCreated":
                    Console.WriteLine("    📧 发送欢迎邮件");
                    break;
                case "OrderCreated":
                    Console.WriteLine("    📱 发送订单确认短信");
                    break;
                case "PaymentCompleted":
                    Console.WriteLine("    💳 发送支付成功通知");
                    break;
            }
        }
    }
}
