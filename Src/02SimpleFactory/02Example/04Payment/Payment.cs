namespace _SimpleFactory._02Example._04Payment
{
    // 支付方式枚举
    public enum PaymentType
    {
        CreditCard,
        Alipay,
        WeChatPay
    }

    // 支付接口
    public interface IPayment
    {
        void ProcessPayment(decimal amount, string orderId);
    }

    // 信用卡支付
    public class CreditCardPayment : IPayment
    {
        public void ProcessPayment(decimal amount, string orderId)
        {
            Console.WriteLine($"[信用卡支付]");
            Console.WriteLine($"  订单号: {orderId}");
            Console.WriteLine($"  金额: ￥{amount:F2}");
            Console.WriteLine($"  手续费: {amount * 0.02m:F2} (2%)");
            Console.WriteLine($"  状态: 支付成功");
        }
    }

    // 支付宝支付
    public class AlipayPayment : IPayment
    {
        public void ProcessPayment(decimal amount, string orderId)
        {
            Console.WriteLine($"[支付宝支付]");
            Console.WriteLine($"  订单号: {orderId}");
            Console.WriteLine($"  金额: ￥{amount:F2}");
            Console.WriteLine($"  手续费: {amount * 0.006m:F2} (0.6%)");
            Console.WriteLine($"  状态: 支付成功");
        }
    }

    // 微信支付
    public class WeChatPayPayment : IPayment
    {
        public void ProcessPayment(decimal amount, string orderId)
        {
            Console.WriteLine($"[微信支付]");
            Console.WriteLine($"  订单号: {orderId}");
            Console.WriteLine($"  金额: ￥{amount:F2}");
            Console.WriteLine($"  手续费: {amount * 0.006m:F2} (0.6%)");
            Console.WriteLine($"  状态: 支付成功");
        }
    }

    // 支付工厂
    public static class PaymentFactory
    {
        public static IPayment CreatePayment(PaymentType type)
        {
            switch (type)
            {
                case PaymentType.CreditCard:
                    return new CreditCardPayment();
                case PaymentType.Alipay:
                    return new AlipayPayment();
                case PaymentType.WeChatPay:
                    return new WeChatPayPayment();
                default:
                    throw new ArgumentException($"Unknown payment type: {type}");
            }
        }
    }
}
