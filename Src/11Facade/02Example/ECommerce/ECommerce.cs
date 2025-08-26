namespace _Facade._02Example.ECommerce
{
    // 子系统 - 库存系统
    public class InventorySystem
    {
        public bool CheckStock(string productId)
        {
            Console.WriteLine($"  检查商品 {productId} 库存...");
            return true; // 模拟库存充足
        }

        public void UpdateStock(string productId, int quantity)
        {
            Console.WriteLine($"  更新商品 {productId} 库存，减少 {quantity} 件");
        }
    }

    // 子系统 - 支付系统
    public class PaymentSystem
    {
        public bool ProcessPayment(string orderId, decimal amount)
        {
            Console.WriteLine($"  处理订单 {orderId} 的支付，金额: ￥{amount}");
            return true; // 模拟支付成功
        }

        public void RefundPayment(string orderId, decimal amount)
        {
            Console.WriteLine($"  退款订单 {orderId}，金额: ￥{amount}");
        }
    }

    // 子系统 - 物流系统
    public class ShippingSystem
    {
        public string ArrangeShipping(string orderId, string address)
        {
            Console.WriteLine($"  安排订单 {orderId} 配送到: {address}");
            return "SF" + new Random().Next(100000, 999999);
        }

        public void TrackShipping(string trackingNumber)
        {
            Console.WriteLine($"  查询物流单号: {trackingNumber}");
        }
    }

    // 子系统 - 通知系统
    public class NotificationSystem
    {
        public void SendEmail(string email, string message)
        {
            Console.WriteLine($"  发送邮件到 {email}: {message}");
        }

        public void SendSMS(string phone, string message)
        {
            Console.WriteLine($"  发送短信到 {phone}: {message}");
        }
    }

    // 外观类 - 电商订单外观
    public class OrderFacade
    {
        private InventorySystem inventory;
        private PaymentSystem payment;
        private ShippingSystem shipping;
        private NotificationSystem notification;

        public OrderFacade()
        {
            inventory = new InventorySystem();
            payment = new PaymentSystem();
            shipping = new ShippingSystem();
            notification = new NotificationSystem();
        }

        public bool PlaceOrder(string productId, int quantity, decimal price, string customerEmail, string address)
        {
            Console.WriteLine("\n开始处理订单...");
            
            // 检查库存
            if (!inventory.CheckStock(productId))
            {
                Console.WriteLine("  订单失败: 库存不足");
                return false;
            }

            string orderId = "ORD" + new Random().Next(100000, 999999);
            decimal totalAmount = price * quantity;

            // 处理支付
            if (!payment.ProcessPayment(orderId, totalAmount))
            {
                Console.WriteLine("  订单失败: 支付失败");
                return false;
            }

            // 更新库存
            inventory.UpdateStock(productId, quantity);

            // 安排配送
            string trackingNumber = shipping.ArrangeShipping(orderId, address);

            // 发送通知
            notification.SendEmail(customerEmail, $"订单 {orderId} 已确认，物流单号: {trackingNumber}");
            
            Console.WriteLine($"订单 {orderId} 处理成功！\n");
            return true;
        }

        public void CancelOrder(string orderId, string productId, int quantity, decimal amount, string customerEmail)
        {
            Console.WriteLine($"\n取消订单 {orderId}...");
            
            // 退款
            payment.RefundPayment(orderId, amount);
            
            // 恢复库存
            inventory.UpdateStock(productId, -quantity);
            
            // 发送通知
            notification.SendEmail(customerEmail, $"订单 {orderId} 已取消，退款将在3-5个工作日内到账");
            
            Console.WriteLine($"订单 {orderId} 取消成功！\n");
        }
    }
}