namespace _23Strategy._02Example.PaymentStrategy
{
    // 支付策略接口
    public interface IPaymentStrategy
    {
        string Name { get; }
        bool ProcessPayment(decimal amount, string account);
        decimal CalculateFee(decimal amount);
    }

    // 支付上下文
    public class PaymentContext
    {
        private IPaymentStrategy? _strategy;
        public string OrderId { get; }
        public decimal Amount { get; }

        public PaymentContext(string orderId, decimal amount)
        {
            OrderId = orderId;
            Amount = amount;
        }

        public void SetPaymentStrategy(IPaymentStrategy strategy)
        {
            _strategy = strategy;
            Console.WriteLine($"  选择支付方式: {strategy.Name}");
        }

        public bool ExecutePayment(string account)
        {
            if (_strategy == null)
            {
                Console.WriteLine("  ❌ 请先选择支付方式");
                return false;
            }

            decimal fee = _strategy.CalculateFee(Amount);
            decimal totalAmount = Amount + fee;
            
            Console.WriteLine($"  订单金额: ¥{Amount:F2}");
            Console.WriteLine($"  手续费: ¥{fee:F2}");
            Console.WriteLine($"  实付金额: ¥{totalAmount:F2}");
            
            return _strategy.ProcessPayment(totalAmount, account);
        }
    }

    // 具体策略 - 支付宝支付
    public class AlipayStrategy : IPaymentStrategy
    {
        public string Name => "支付宝";

        public bool ProcessPayment(decimal amount, string account)
        {
            Console.WriteLine($"  正在通过支付宝处理支付...");
            Console.WriteLine($"  支付宝账号: {account}");
            Console.WriteLine($"  扣款金额: ¥{amount:F2}");
            
            // 模拟支付处理
            Thread.Sleep(500);
            
            Console.WriteLine($"  ✅ 支付宝支付成功！");
            return true;
        }

        public decimal CalculateFee(decimal amount)
        {
            // 支付宝手续费：0.6%
            return amount * 0.006m;
        }
    }

    // 具体策略 - 微信支付
    public class WeChatPayStrategy : IPaymentStrategy
    {
        public string Name => "微信支付";

        public bool ProcessPayment(decimal amount, string account)
        {
            Console.WriteLine($"  正在通过微信支付处理...");
            Console.WriteLine($"  微信账号: {account}");
            Console.WriteLine($"  扣款金额: ¥{amount:F2}");
            
            // 模拟支付处理
            Thread.Sleep(500);
            
            Console.WriteLine($"  ✅ 微信支付成功！");
            return true;
        }

        public decimal CalculateFee(decimal amount)
        {
            // 微信手续费：0.6%
            return amount * 0.006m;
        }
    }

    // 具体策略 - 信用卡支付
    public class CreditCardStrategy : IPaymentStrategy
    {
        public string Name => "信用卡";

        public bool ProcessPayment(decimal amount, string account)
        {
            Console.WriteLine($"  正在通过信用卡处理支付...");
            Console.WriteLine($"  卡号: {MaskCardNumber(account)}");
            Console.WriteLine($"  扣款金额: ¥{amount:F2}");
            
            // 模拟支付处理
            Thread.Sleep(1000);
            
            Console.WriteLine($"  ✅ 信用卡支付成功！");
            return true;
        }

        public decimal CalculateFee(decimal amount)
        {
            // 信用卡手续费：2%
            return amount * 0.02m;
        }

        private string MaskCardNumber(string cardNumber)
        {
            if (cardNumber.Length > 8)
            {
                return cardNumber.Substring(0, 4) + "****" + cardNumber.Substring(cardNumber.Length - 4);
            }
            return cardNumber;
        }
    }

    // 具体策略 - 银行转账
    public class BankTransferStrategy : IPaymentStrategy
    {
        public string Name => "银行转账";

        public bool ProcessPayment(decimal amount, string account)
        {
            Console.WriteLine($"  正在通过银行转账处理...");
            Console.WriteLine($"  银行账号: {account}");
            Console.WriteLine($"  转账金额: ¥{amount:F2}");
            
            // 模拟支付处理
            Thread.Sleep(1500);
            
            Console.WriteLine($"  ✅ 银行转账成功！");
            return true;
        }

        public decimal CalculateFee(decimal amount)
        {
            // 银行转账手续费：固定5元
            return 5.00m;
        }
    }

    // 具体策略 - 加密货币支付
    public class CryptocurrencyStrategy : IPaymentStrategy
    {
        public string Name => "加密货币";

        public bool ProcessPayment(decimal amount, string account)
        {
            Console.WriteLine($"  正在通过加密货币处理支付...");
            Console.WriteLine($"  钱包地址: {account}");
            decimal btcAmount = amount / 280000m; // 假设1 BTC = 280000 CNY
            Console.WriteLine($"  BTC金额: {btcAmount:F8} BTC");
            
            // 模拟区块链确认
            Thread.Sleep(2000);
            
            Console.WriteLine($"  ✅ 加密货币支付已确认！");
            return true;
        }

        public decimal CalculateFee(decimal amount)
        {
            // 加密货币网络费：0.1%
            return amount * 0.001m;
        }
    }
}
