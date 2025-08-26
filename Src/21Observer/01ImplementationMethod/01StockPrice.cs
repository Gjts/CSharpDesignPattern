namespace _21Observer
{
    // 观察者接口
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    // 主题接口
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    // 股票类（具体主题）
    public class Stock : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private decimal _price;
        
        public string Symbol { get; }
        
        public decimal Price
        {
            get => _price;
            set
            {
                decimal oldPrice = _price;
                _price = value;
                Console.WriteLine($"\n{Symbol} 价格变动: {oldPrice:C} -> {_price:C}");
                Notify();
            }
        }

        public Stock(string symbol, decimal price)
        {
            Symbol = symbol;
            _price = price;
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }

    // 投资者（具体观察者）
    public class Investor : IObserver
    {
        private string _name;

        public Investor(string name)
        {
            _name = name;
        }

        public void Update(ISubject subject)
        {
            if (subject is Stock stock)
            {
                Console.WriteLine($"  [{_name}] 收到通知: {stock.Symbol} 当前价格 {stock.Price:C}");
            }
        }
    }

    // 价格警报系统
    public class PriceAlertSystem : IObserver
    {
        public void Update(ISubject subject)
        {
            if (subject is Stock stock)
            {
                if (stock.Price > 155m)
                {
                    Console.WriteLine($"  [警报] {stock.Symbol} 价格超过155，建议卖出！");
                }
                else if (stock.Price < 145m)
                {
                    Console.WriteLine($"  [警报] {stock.Symbol} 价格低于145，建议买入！");
                }
            }
        }
    }

    // 订单状态枚举
    public enum OrderStatus
    {
        Created,
        Confirmed,
        Shipped,
        Delivered
    }

    // 订单类
    public class Order
    {
        private List<IOrderObserver> _observers = new List<IOrderObserver>();
        public string OrderId { get; }
        public OrderStatus Status { get; private set; }

        public Order(string orderId)
        {
            OrderId = orderId;
            Status = OrderStatus.Created;
        }

        public void Subscribe(IOrderObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IOrderObserver observer)
        {
            _observers.Remove(observer);
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            var oldStatus = Status;
            Status = newStatus;
            Console.WriteLine($"\n订单 {OrderId} 状态更新: {oldStatus} -> {Status}");
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.OnOrderStatusChanged(this);
            }
        }
    }

    // 订单观察者接口
    public interface IOrderObserver
    {
        void OnOrderStatusChanged(Order order);
    }

    // 客户
    public class Customer : IOrderObserver
    {
        private string _name;

        public Customer(string name)
        {
            _name = name;
        }

        public void OnOrderStatusChanged(Order order)
        {
            Console.WriteLine($"  [客户-{_name}] 订单 {order.OrderId} 状态: {order.Status}");
        }
    }

    // 仓库
    public class Warehouse : IOrderObserver
    {
        public void OnOrderStatusChanged(Order order)
        {
            if (order.Status == OrderStatus.Confirmed)
            {
                Console.WriteLine($"  [仓库] 准备发货: {order.OrderId}");
            }
        }
    }

    // 邮件服务
    public class EmailService : IOrderObserver
    {
        public void OnOrderStatusChanged(Order order)
        {
            Console.WriteLine($"  [邮件] 发送订单状态更新邮件: {order.OrderId} - {order.Status}");
        }
    }
}
