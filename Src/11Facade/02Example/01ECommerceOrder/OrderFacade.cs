namespace _11Facade._02Example._01ECommerceOrder
{
    // 商品库存子系统
    public class InventorySystem
    {
        private Dictionary<string, int> inventory = new Dictionary<string, int>
        {
            { "SKU001", 100 },
            { "SKU002", 50 },
            { "SKU003", 200 },
            { "SKU004", 30 },
            { "SKU005", 0 }
        };

        public bool CheckStock(string skuId, int quantity)
        {
            Console.WriteLine($"[库存系统] 检查商品 {skuId} 的库存...");
            if (inventory.ContainsKey(skuId))
            {
                bool hasStock = inventory[skuId] >= quantity;
                Console.WriteLine($"  商品 {skuId}: 库存 {inventory[skuId]}, 需求 {quantity}, {(hasStock ? "✓ 库存充足" : "✗ 库存不足")}");
                return hasStock;
            }
            Console.WriteLine($"  ✗ 商品 {skuId} 不存在");
            return false;
        }

        public void ReserveStock(string skuId, int quantity)
        {
            Console.WriteLine($"[库存系统] 锁定商品 {skuId} 数量 {quantity}");
            if (inventory.ContainsKey(skuId))
            {
                inventory[skuId] -= quantity;
                Console.WriteLine($"  ✓ 库存已锁定，剩余库存: {inventory[skuId]}");
            }
        }

        public void ReleaseStock(string skuId, int quantity)
        {
            Console.WriteLine($"[库存系统] 释放商品 {skuId} 数量 {quantity}");
            if (inventory.ContainsKey(skuId))
            {
                inventory[skuId] += quantity;
                Console.WriteLine($"  ✓ 库存已释放，当前库存: {inventory[skuId]}");
            }
        }
    }

    // 支付子系统
    public class PaymentSystem
    {
        public class PaymentResult
        {
            public bool Success { get; set; }
            public string TransactionId { get; set; }
            public string Message { get; set; }
        }

        public PaymentResult ProcessPayment(string orderId, decimal amount, string paymentMethod)
        {
            Console.WriteLine($"[支付系统] 处理订单 {orderId} 的支付...");
            Console.WriteLine($"  支付方式: {paymentMethod}");
            Console.WriteLine($"  支付金额: ¥{amount:F2}");
            
            // 模拟支付处理
            Random random = new Random();
            bool success = random.Next(100) > 10; // 90%成功率
            
            if (success)
            {
                string transactionId = $"TXN{DateTime.Now:yyyyMMddHHmmss}{random.Next(1000, 9999)}";
                Console.WriteLine($"  ✓ 支付成功，交易号: {transactionId}");
                return new PaymentResult 
                { 
                    Success = true, 
                    TransactionId = transactionId,
                    Message = "支付成功"
                };
            }
            else
            {
                Console.WriteLine("  ✗ 支付失败");
                return new PaymentResult 
                { 
                    Success = false, 
                    TransactionId = null,
                    Message = "支付失败，请重试"
                };
            }
        }

        public void RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine($"[支付系统] 处理退款...");
            Console.WriteLine($"  原交易号: {transactionId}");
            Console.WriteLine($"  退款金额: ¥{amount:F2}");
            Console.WriteLine($"  ✓ 退款已发起，预计1-3个工作日到账");
        }
    }

    // 物流子系统
    public class LogisticsSystem
    {
        private List<string> warehouses = new List<string> { "北京仓", "上海仓", "广州仓", "成都仓" };
        private List<string> carriers = new List<string> { "顺丰速运", "京东物流", "圆通快递", "中通快递" };

        public string AllocateWarehouse(string address)
        {
            Console.WriteLine($"[物流系统] 为地址 '{address}' 分配仓库...");
            Random random = new Random();
            string warehouse = warehouses[random.Next(warehouses.Count)];
            Console.WriteLine($"  ✓ 已分配: {warehouse}");
            return warehouse;
        }

        public string AssignCarrier(decimal orderAmount)
        {
            Console.WriteLine($"[物流系统] 根据订单金额 ¥{orderAmount:F2} 选择承运商...");
            string carrier;
            if (orderAmount > 500)
                carrier = carriers[0]; // 顺丰
            else if (orderAmount > 200)
                carrier = carriers[1]; // 京东
            else
                carrier = carriers[new Random().Next(2, carriers.Count)]; // 其他
            
            Console.WriteLine($"  ✓ 已选择: {carrier}");
            return carrier;
        }

        public string CreateShipment(string orderId, string warehouse, string carrier, string address)
        {
            Console.WriteLine($"[物流系统] 创建运单...");
            string trackingNumber = $"SF{DateTime.Now:yyyyMMdd}{new Random().Next(100000, 999999)}";
            Console.WriteLine($"  订单号: {orderId}");
            Console.WriteLine($"  发货仓: {warehouse}");
            Console.WriteLine($"  承运商: {carrier}");
            Console.WriteLine($"  收货地址: {address}");
            Console.WriteLine($"  ✓ 运单号: {trackingNumber}");
            return trackingNumber;
        }
    }

    // 通知子系统
    public class NotificationSystem
    {
        public void SendOrderConfirmation(string orderId, string email, string phone)
        {
            Console.WriteLine($"[通知系统] 发送订单确认通知...");
            Console.WriteLine($"  📧 邮件已发送至: {email}");
            Console.WriteLine($"  📱 短信已发送至: {phone}");
        }

        public void SendShipmentNotification(string orderId, string trackingNumber, string email, string phone)
        {
            Console.WriteLine($"[通知系统] 发送发货通知...");
            Console.WriteLine($"  订单 {orderId} 已发货");
            Console.WriteLine($"  运单号: {trackingNumber}");
            Console.WriteLine($"  📧 邮件通知已发送");
            Console.WriteLine($"  📱 短信通知已发送");
        }

        public void SendPaymentFailureNotification(string orderId, string reason)
        {
            Console.WriteLine($"[通知系统] 发送支付失败通知...");
            Console.WriteLine($"  订单 {orderId} 支付失败");
            Console.WriteLine($"  失败原因: {reason}");
        }
    }

    // 优惠券子系统
    public class CouponSystem
    {
        private Dictionary<string, decimal> coupons = new Dictionary<string, decimal>
        {
            { "SAVE10", 0.1m },
            { "SAVE20", 0.2m },
            { "VIP30", 0.3m },
            { "NEW50", 0.5m }
        };

        public decimal ApplyCoupon(string couponCode, decimal originalAmount)
        {
            Console.WriteLine($"[优惠券系统] 应用优惠券 {couponCode}...");
            if (coupons.ContainsKey(couponCode))
            {
                decimal discount = originalAmount * coupons[couponCode];
                decimal finalAmount = originalAmount - discount;
                Console.WriteLine($"  原价: ¥{originalAmount:F2}");
                Console.WriteLine($"  折扣: -{coupons[couponCode]:P0} (¥{discount:F2})");
                Console.WriteLine($"  ✓ 实付: ¥{finalAmount:F2}");
                return finalAmount;
            }
            else
            {
                Console.WriteLine($"  ✗ 优惠券无效");
                return originalAmount;
            }
        }

        public bool ValidateCoupon(string couponCode)
        {
            return coupons.ContainsKey(couponCode);
        }
    }

    // 订单信息
    public class OrderInfo
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderItem> Items { get; set; }
        public string CouponCode { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }

        public OrderInfo()
        {
            Items = new List<OrderItem>();
            OrderId = $"ORD{DateTime.Now:yyyyMMddHHmmss}";
            Status = "待处理";
        }
    }

    public class OrderItem
    {
        public string SkuId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // 电商订单外观类
    public class ECommerceOrderFacade
    {
        private InventorySystem inventory;
        private PaymentSystem payment;
        private LogisticsSystem logistics;
        private NotificationSystem notification;
        private CouponSystem coupon;

        public ECommerceOrderFacade()
        {
            inventory = new InventorySystem();
            payment = new PaymentSystem();
            logistics = new LogisticsSystem();
            notification = new NotificationSystem();
            coupon = new CouponSystem();
        }

        // 简化的下单接口
        public bool PlaceOrder(OrderInfo order)
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine($"开始处理订单: {order.OrderId}");
            Console.WriteLine(new string('=', 60));

            try
            {
                // 步骤1: 检查库存
                Console.WriteLine("\n步骤1: 检查库存");
                foreach (var item in order.Items)
                {
                    if (!inventory.CheckStock(item.SkuId, item.Quantity))
                    {
                        Console.WriteLine($"\n✗ 订单处理失败: 商品 {item.SkuId} 库存不足");
                        order.Status = "库存不足";
                        return false;
                    }
                }

                // 步骤2: 锁定库存
                Console.WriteLine("\n步骤2: 锁定库存");
                foreach (var item in order.Items)
                {
                    inventory.ReserveStock(item.SkuId, item.Quantity);
                }

                // 步骤3: 计算价格并应用优惠券
                Console.WriteLine("\n步骤3: 计算价格");
                decimal originalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);
                decimal finalAmount = originalAmount;
                
                if (!string.IsNullOrEmpty(order.CouponCode))
                {
                    finalAmount = coupon.ApplyCoupon(order.CouponCode, originalAmount);
                }
                order.TotalAmount = finalAmount;

                // 步骤4: 处理支付
                Console.WriteLine("\n步骤4: 处理支付");
                var paymentResult = payment.ProcessPayment(order.OrderId, finalAmount, order.PaymentMethod);
                
                if (!paymentResult.Success)
                {
                    // 支付失败，释放库存
                    Console.WriteLine("\n支付失败，释放库存");
                    foreach (var item in order.Items)
                    {
                        inventory.ReleaseStock(item.SkuId, item.Quantity);
                    }
                    notification.SendPaymentFailureNotification(order.OrderId, paymentResult.Message);
                    order.Status = "支付失败";
                    return false;
                }

                // 步骤5: 分配仓库和物流
                Console.WriteLine("\n步骤5: 分配物流");
                string warehouse = logistics.AllocateWarehouse(order.ShippingAddress);
                string carrier = logistics.AssignCarrier(finalAmount);
                string trackingNumber = logistics.CreateShipment(order.OrderId, warehouse, carrier, order.ShippingAddress);
                order.TrackingNumber = trackingNumber;

                // 步骤6: 发送通知
                Console.WriteLine("\n步骤6: 发送通知");
                notification.SendOrderConfirmation(order.OrderId, order.CustomerEmail, order.CustomerPhone);
                notification.SendShipmentNotification(order.OrderId, trackingNumber, order.CustomerEmail, order.CustomerPhone);

                order.Status = "已完成";
                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine($"✓ 订单 {order.OrderId} 处理成功！");
                Console.WriteLine($"  总金额: ¥{finalAmount:F2}");
                Console.WriteLine($"  运单号: {trackingNumber}");
                Console.WriteLine(new string('=', 60));
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ 订单处理异常: {ex.Message}");
                order.Status = "处理异常";
                return false;
            }
        }

        // 取消订单
        public bool CancelOrder(OrderInfo order, string reason)
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine($"取消订单: {order.OrderId}");
            Console.WriteLine($"取消原因: {reason}");
            Console.WriteLine(new string('=', 60));

            // 释放库存
            foreach (var item in order.Items)
            {
                inventory.ReleaseStock(item.SkuId, item.Quantity);
            }

            // 如果已支付，处理退款
            if (order.Status == "已完成" && order.TotalAmount > 0)
            {
                payment.RefundPayment(order.OrderId, order.TotalAmount);
            }

            // 发送取消通知
            Console.WriteLine($"[通知系统] 订单 {order.OrderId} 已取消");
            
            order.Status = "已取消";
            return true;
        }
    }
}
