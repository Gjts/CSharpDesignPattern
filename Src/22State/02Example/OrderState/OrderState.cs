namespace _22State._02Example.OrderState
{
    // 订单状态接口
    public interface IOrderState
    {
        void Pay(OrderContext context);
        void Ship(OrderContext context);
        void Deliver(OrderContext context);
        void Cancel(OrderContext context);
        void Return(OrderContext context);
        string GetStatus();
    }

    // 订单上下文
    public class OrderContext
    {
        private IOrderState _currentState;
        public string OrderId { get; }
        public decimal Amount { get; }
        public DateTime CreateTime { get; }

        public OrderContext(string orderId, decimal amount)
        {
            OrderId = orderId;
            Amount = amount;
            CreateTime = DateTime.Now;
            _currentState = new PendingState();
            Console.WriteLine($"订单 {orderId} 创建成功，金额: ¥{amount}");
        }

        public void SetState(IOrderState state)
        {
            _currentState = state;
            Console.WriteLine($"  订单状态变更为: {state.GetStatus()}");
        }

        public void Pay() => _currentState.Pay(this);
        public void Ship() => _currentState.Ship(this);
        public void Deliver() => _currentState.Deliver(this);
        public void Cancel() => _currentState.Cancel(this);
        public void Return() => _currentState.Return(this);
        public string GetCurrentStatus() => _currentState.GetStatus();
    }

    // 待支付状态
    public class PendingState : IOrderState
    {
        public void Pay(OrderContext context)
        {
            Console.WriteLine("  正在处理支付...");
            context.SetState(new PaidState());
        }

        public void Ship(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单未支付，无法发货");
        }

        public void Deliver(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单未支付，无法配送");
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("  订单已取消");
            context.SetState(new CancelledState());
        }

        public void Return(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单未完成，无法退货");
        }

        public string GetStatus() => "待支付";
    }

    // 已支付状态
    public class PaidState : IOrderState
    {
        public void Pay(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已支付");
        }

        public void Ship(OrderContext context)
        {
            Console.WriteLine("  订单开始发货...");
            context.SetState(new ShippedState());
        }

        public void Deliver(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单未发货，无法配送");
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("  订单取消，款项将退回");
            context.SetState(new CancelledState());
        }

        public void Return(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单未完成，无法退货");
        }

        public string GetStatus() => "已支付";
    }

    // 已发货状态
    public class ShippedState : IOrderState
    {
        public void Pay(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已支付");
        }

        public void Ship(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已发货");
        }

        public void Deliver(OrderContext context)
        {
            Console.WriteLine("  订单配送完成");
            context.SetState(new DeliveredState());
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已发货，无法取消");
        }

        public void Return(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：请先确认收货");
        }

        public string GetStatus() => "配送中";
    }

    // 已送达状态
    public class DeliveredState : IOrderState
    {
        public void Pay(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已完成");
        }

        public void Ship(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已完成");
        }

        public void Deliver(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已送达");
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已完成，无法取消");
        }

        public void Return(OrderContext context)
        {
            Console.WriteLine("  发起退货申请");
            context.SetState(new ReturnedState());
        }

        public string GetStatus() => "已完成";
    }

    // 已取消状态
    public class CancelledState : IOrderState
    {
        public void Pay(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已取消");
        }

        public void Ship(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已取消");
        }

        public void Deliver(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已取消");
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已经是取消状态");
        }

        public void Return(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已取消");
        }

        public string GetStatus() => "已取消";
    }

    // 退货状态
    public class ReturnedState : IOrderState
    {
        public void Pay(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已退货");
        }

        public void Ship(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已退货");
        }

        public void Deliver(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已退货");
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已退货");
        }

        public void Return(OrderContext context)
        {
            Console.WriteLine("  ❌ 错误：订单已经是退货状态");
        }

        public string GetStatus() => "已退货";
    }
}
