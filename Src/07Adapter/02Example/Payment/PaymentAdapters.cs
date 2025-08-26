namespace _07Adapter._02Example.Payment.Business
{
    // 统一的支付处理接口
    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount, string currency, string account);
    }

    // 第三方PayPal服务（需要适配）
    public class PayPalService
    {
        public void MakePayment(string email, double amount)
        {
            Console.WriteLine($"   [PayPal] 向 {email} 收款 ${amount}");
        }
    }

    // PayPal适配器
    public class PayPalAdapter : IPaymentProcessor
    {
        private readonly PayPalService _paypalService;

        public PayPalAdapter(PayPalService paypalService)
        {
            _paypalService = paypalService;
        }

        public void ProcessPayment(decimal amount, string currency, string account)
        {
            // 转换货币和金额格式
            double convertedAmount = Convert.ToDouble(amount);
            Console.WriteLine($"   适配器: 转换 {currency} {amount} 为 PayPal格式");
            _paypalService.MakePayment(account, convertedAmount);
        }
    }

    // 第三方Stripe服务（需要适配）
    public class StripeService
    {
        public void ChargeCard(string customerId, int amountInCents)
        {
            Console.WriteLine($"   [Stripe] 向客户 {customerId} 收费 {amountInCents} 分");
        }
    }

    // Stripe适配器
    public class StripeAdapter : IPaymentProcessor
    {
        private readonly StripeService _stripeService;

        public StripeAdapter(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        public void ProcessPayment(decimal amount, string currency, string account)
        {
            // 转换为分
            int amountInCents = (int)(amount * 100);
            Console.WriteLine($"   适配器: 转换 {currency} {amount} 为 Stripe格式（分）");
            _stripeService.ChargeCard(account, amountInCents);
        }
    }

    // 本地支付处理器（原生实现）
    public class LocalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount, string currency, string account)
        {
            Console.WriteLine($"   [本地支付] 处理 {currency} {amount} 支付给 {account}");
            Console.WriteLine($"   状态: 支付成功");
        }
    }
}
