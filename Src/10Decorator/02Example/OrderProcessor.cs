namespace _10Decorator._02Example
{
    // 订单类
    public class Order
    {
        public required string OrderId { get; set; }
        public required string CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public required List<string> Items { get; set; }
        public DateTime CreateTime { get; set; }
        public required string Status { get; set; }
        public required Dictionary<string, object> ExtendedInfo { get; set; }

        public Order()
        {
            Items = new List<string>();
            ExtendedInfo = new Dictionary<string, object>();
            CreateTime = DateTime.Now;
            Status = "待处理";
        }
    }

    // 订单处理接口
    public interface IOrderProcessor
    {
        Order Process(Order order);
        string GetDescription();
    }

    // 基础订单处理器
    public class BasicOrderProcessor : IOrderProcessor
    {
        public Order Process(Order order)
        {
            Console.WriteLine($"[基础处理] 处理订单: {order.OrderId}");
            order.Status = "处理中";
            return order;
        }

        public string GetDescription()
        {
            return "基础订单处理";
        }
    }

    // 订单处理装饰器抽象类
    public abstract class OrderProcessorDecorator : IOrderProcessor
    {
        protected IOrderProcessor processor;

        public OrderProcessorDecorator(IOrderProcessor processor)
        {
            this.processor = processor;
        }

        public virtual Order Process(Order order)
        {
            return processor.Process(order);
        }

        public virtual string GetDescription()
        {
            return processor.GetDescription();
        }
    }

    // 库存检查装饰器
    public class InventoryCheckDecorator : OrderProcessorDecorator
    {
        public InventoryCheckDecorator(IOrderProcessor processor) : base(processor)
        {
        }

        public override Order Process(Order order)
        {
            Console.WriteLine($"[库存检查] 检查订单 {order.OrderId} 的商品库存...");
            
            // 模拟库存检查
            bool hasStock = CheckInventory(order);
            if (hasStock)
            {
                Console.WriteLine("  ✓ 库存充足");
                order.ExtendedInfo["InventoryChecked"] = true;
            }
            else
            {
                Console.WriteLine("  ✗ 库存不足");
                order.ExtendedInfo["InventoryChecked"] = false;
                order.Status = "库存不足";
            }
            
            return base.Process(order);
        }

        private bool CheckInventory(Order order)
        {
            // 模拟库存检查逻辑
            return order.Items.Count > 0;
        }

        public override string GetDescription()
        {
            return base.GetDescription() + " -> 库存检查";
        }
    }

    // 支付验证装饰器
    public class PaymentValidationDecorator : OrderProcessorDecorator
    {
        public PaymentValidationDecorator(IOrderProcessor processor) : base(processor)
        {
        }

        public override Order Process(Order order)
        {
            Console.WriteLine($"[支付验证] 验证订单 {order.OrderId} 的支付信息...");
            
            // 模拟支付验证
            bool isValid = ValidatePayment(order);
            if (isValid)
            {
                Console.WriteLine($"  ✓ 支付金额: ¥{order.TotalAmount}");
                order.ExtendedInfo["PaymentValidated"] = true;
            }
            else
            {
                Console.WriteLine("  ✗ 支付验证失败");
                order.ExtendedInfo["PaymentValidated"] = false;
                order.Status = "支付失败";
            }
            
            return base.Process(order);
        }

        private bool ValidatePayment(Order order)
        {
            // 模拟支付验证逻辑
            return order.TotalAmount > 0;
        }

        public override string GetDescription()
        {
            return base.GetDescription() + " -> 支付验证";
        }
    }

    // 优惠券处理装饰器
    public class CouponProcessorDecorator : OrderProcessorDecorator
    {
        private decimal discountRate;

        public CouponProcessorDecorator(IOrderProcessor processor, decimal discountRate = 0.1m) 
            : base(processor)
        {
            this.discountRate = discountRate;
        }

        public override Order Process(Order order)
        {
            Console.WriteLine($"[优惠券处理] 应用优惠券到订单 {order.OrderId}...");
            
            decimal originalAmount = order.TotalAmount;
            decimal discount = originalAmount * discountRate;
            order.TotalAmount = originalAmount - discount;
            
            Console.WriteLine($"  原价: ¥{originalAmount:F2}");
            Console.WriteLine($"  折扣: ¥{discount:F2} ({discountRate:P0})");
            Console.WriteLine($"  实付: ¥{order.TotalAmount:F2}");
            
            order.ExtendedInfo["CouponApplied"] = true;
            order.ExtendedInfo["DiscountAmount"] = discount;
            
            return base.Process(order);
        }

        public override string GetDescription()
        {
            return base.GetDescription() + " -> 优惠券处理";
        }
    }

    // 物流分配装饰器
    public class LogisticsAllocationDecorator : OrderProcessorDecorator
    {
        public LogisticsAllocationDecorator(IOrderProcessor processor) : base(processor)
        {
        }

        public override Order Process(Order order)
        {
            Console.WriteLine($"[物流分配] 为订单 {order.OrderId} 分配物流...");
            
            string warehouse = AllocateWarehouse(order);
            string carrier = SelectCarrier(order);
            
            Console.WriteLine($"  仓库: {warehouse}");
            Console.WriteLine($"  承运商: {carrier}");
            
            order.ExtendedInfo["Warehouse"] = warehouse;
            order.ExtendedInfo["Carrier"] = carrier;
            order.ExtendedInfo["LogisticsAllocated"] = true;
            
            return base.Process(order);
        }

        private string AllocateWarehouse(Order order)
        {
            // 模拟仓库分配逻辑
            string[] warehouses = { "北京仓", "上海仓", "广州仓", "深圳仓" };
            return warehouses[new Random().Next(warehouses.Length)];
        }

        private string SelectCarrier(Order order)
        {
            // 模拟承运商选择逻辑
            if (order.TotalAmount > 500)
                return "顺丰速运";
            else if (order.TotalAmount > 200)
                return "京东物流";
            else
                return "中通快递";
        }

        public override string GetDescription()
        {
            return base.GetDescription() + " -> 物流分配";
        }
    }

    // 风控检查装饰器
    public class RiskControlDecorator : OrderProcessorDecorator
    {
        public RiskControlDecorator(IOrderProcessor processor) : base(processor)
        {
        }

        public override Order Process(Order order)
        {
            Console.WriteLine($"[风控检查] 对订单 {order.OrderId} 进行风险评估...");
            
            int riskScore = CalculateRiskScore(order);
            string riskLevel = GetRiskLevel(riskScore);
            
            Console.WriteLine($"  风险分数: {riskScore}");
            Console.WriteLine($"  风险等级: {riskLevel}");
            
            order.ExtendedInfo["RiskScore"] = riskScore;
            order.ExtendedInfo["RiskLevel"] = riskLevel;
            
            if (riskLevel == "高风险")
            {
                Console.WriteLine("  ⚠ 订单被标记为高风险，需要人工审核");
                order.Status = "待审核";
                order.ExtendedInfo["RequireManualReview"] = true;
            }
            
            return base.Process(order);
        }

        private int CalculateRiskScore(Order order)
        {
            // 模拟风险评分逻辑
            int score = 0;
            
            // 金额风险
            if (order.TotalAmount > 10000) score += 30;
            else if (order.TotalAmount > 5000) score += 20;
            else if (order.TotalAmount > 1000) score += 10;
            
            // 新客户风险
            if (order.CustomerId.StartsWith("NEW")) score += 20;
            
            // 时间风险（深夜订单）
            if (order.CreateTime.Hour < 6 || order.CreateTime.Hour > 23) score += 15;
            
            return score;
        }

        private string GetRiskLevel(int score)
        {
            if (score >= 50) return "高风险";
            if (score >= 30) return "中风险";
            return "低风险";
        }

        public override string GetDescription()
        {
            return base.GetDescription() + " -> 风控检查";
        }
    }

    // 消息通知装饰器
    public class NotificationDecorator : OrderProcessorDecorator
    {
        public NotificationDecorator(IOrderProcessor processor) : base(processor)
        {
        }

        public override Order Process(Order order)
        {
            Order processedOrder = base.Process(order);
            
            Console.WriteLine($"[消息通知] 发送订单 {order.OrderId} 的通知...");
            
            // 发送短信通知
            SendSMS(order);
            
            // 发送邮件通知
            SendEmail(order);
            
            // 发送APP推送
            SendPushNotification(order);
            
            order.ExtendedInfo["NotificationSent"] = true;
            
            return processedOrder;
        }

        private void SendSMS(Order order)
        {
            Console.WriteLine($"  📱 短信: 您的订单 {order.OrderId} 已确认");
        }

        private void SendEmail(Order order)
        {
            Console.WriteLine($"  📧 邮件: 订单确认邮件已发送");
        }

        private void SendPushNotification(Order order)
        {
            Console.WriteLine($"  📲 推送: APP推送通知已发送");
        }

        public override string GetDescription()
        {
            return base.GetDescription() + " -> 消息通知";
        }
    }
}