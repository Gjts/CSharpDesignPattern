namespace _08Bridge._02Example
{
    // 支付方式接口（实现部分）
    public interface IPaymentMethod
    {
        bool ProcessPayment(decimal amount, string accountInfo);
        bool ValidateAccount(string accountInfo);
        string GetPaymentMethodName();
    }

    // 具体支付方式：支付宝
    public class AlipayMethod : IPaymentMethod
    {
        public bool ProcessPayment(decimal amount, string accountInfo)
        {
            Console.WriteLine($"[支付宝] 处理支付...");
            Console.WriteLine($"  账号: {accountInfo}");
            Console.WriteLine($"  金额: ¥{amount:F2}");
            Console.WriteLine($"  ✓ 支付成功");
            return true;
        }

        public bool ValidateAccount(string accountInfo)
        {
            Console.WriteLine($"[支付宝] 验证账号: {accountInfo}");
            return accountInfo.Contains("@") || accountInfo.Length == 11;
        }

        public string GetPaymentMethodName()
        {
            return "支付宝";
        }
    }

    // 具体支付方式：微信支付
    public class WeChatPayMethod : IPaymentMethod
    {
        public bool ProcessPayment(decimal amount, string accountInfo)
        {
            Console.WriteLine($"[微信支付] 处理支付...");
            Console.WriteLine($"  微信号: {accountInfo}");
            Console.WriteLine($"  金额: ¥{amount:F2}");
            Console.WriteLine($"  生成支付二维码...");
            Console.WriteLine($"  ✓ 支付成功");
            return true;
        }

        public bool ValidateAccount(string accountInfo)
        {
            Console.WriteLine($"[微信支付] 验证微信号: {accountInfo}");
            return !string.IsNullOrEmpty(accountInfo);
        }

        public string GetPaymentMethodName()
        {
            return "微信支付";
        }
    }

    // 具体支付方式：银行卡
    public class BankCardMethod : IPaymentMethod
    {
        public bool ProcessPayment(decimal amount, string accountInfo)
        {
            Console.WriteLine($"[银行卡] 处理支付...");
            Console.WriteLine($"  卡号: {MaskCardNumber(accountInfo)}");
            Console.WriteLine($"  金额: ¥{amount:F2}");
            Console.WriteLine($"  验证CVV码...");
            Console.WriteLine($"  发送短信验证码...");
            Console.WriteLine($"  ✓ 支付成功");
            return true;
        }

        public bool ValidateAccount(string accountInfo)
        {
            Console.WriteLine($"[银行卡] 验证卡号: {MaskCardNumber(accountInfo)}");
            return accountInfo.Length >= 16 && accountInfo.All(char.IsDigit);
        }

        private string MaskCardNumber(string cardNumber)
        {
            if (cardNumber.Length < 8) return cardNumber;
            return cardNumber.Substring(0, 4) + "****" + cardNumber.Substring(cardNumber.Length - 4);
        }

        public string GetPaymentMethodName()
        {
            return "银行卡";
        }
    }

    // 具体支付方式：PayPal
    public class PayPalMethod : IPaymentMethod
    {
        public bool ProcessPayment(decimal amount, string accountInfo)
        {
            Console.WriteLine($"[PayPal] Processing payment...");
            Console.WriteLine($"  Email: {accountInfo}");
            Console.WriteLine($"  Amount: ${amount:F2}");
            Console.WriteLine($"  Redirecting to PayPal...");
            Console.WriteLine($"  ✓ Payment successful");
            return true;
        }

        public bool ValidateAccount(string accountInfo)
        {
            Console.WriteLine($"[PayPal] Validating email: {accountInfo}");
            return accountInfo.Contains("@") && accountInfo.Contains(".");
        }

        public string GetPaymentMethodName()
        {
            return "PayPal";
        }
    }

    // 支付渠道抽象类（抽象部分）
    public abstract class PaymentChannel
    {
        protected IPaymentMethod paymentMethod;
        protected string channelName;

        public PaymentChannel(IPaymentMethod paymentMethod, string channelName)
        {
            this.paymentMethod = paymentMethod;
            this.channelName = channelName;
        }

        public abstract bool MakePayment(decimal amount, string accountInfo);
        public abstract void ShowPaymentInfo();
    }

    // 电商平台支付渠道
    public class ECommercePaymentChannel : PaymentChannel
    {
        private decimal platformFee = 0.02m; // 平台费率2%

        public ECommercePaymentChannel(IPaymentMethod paymentMethod) 
            : base(paymentMethod, "电商平台")
        {
        }

        public override bool MakePayment(decimal amount, string accountInfo)
        {
            Console.WriteLine($"\n=== {channelName}支付渠道 ===");
            ShowPaymentInfo();
            
            // 验证账号
            if (!paymentMethod.ValidateAccount(accountInfo))
            {
                Console.WriteLine("✗ 账号验证失败");
                return false;
            }

            // 计算平台费用
            decimal platformCharge = amount * platformFee;
            decimal totalAmount = amount + platformCharge;
            
            Console.WriteLine($"订单金额: ¥{amount:F2}");
            Console.WriteLine($"平台服务费: ¥{platformCharge:F2}");
            Console.WriteLine($"实付金额: ¥{totalAmount:F2}");
            
            // 处理支付
            bool result = paymentMethod.ProcessPayment(totalAmount, accountInfo);
            
            if (result)
            {
                Console.WriteLine("发送订单确认邮件...");
                Console.WriteLine("更新库存...");
            }
            
            return result;
        }

        public override void ShowPaymentInfo()
        {
            Console.WriteLine($"支付方式: {paymentMethod.GetPaymentMethodName()}");
            Console.WriteLine($"渠道类型: {channelName}");
            Console.WriteLine($"平台费率: {platformFee:P0}");
        }
    }

    // 移动端支付渠道
    public class MobilePaymentChannel : PaymentChannel
    {
        private bool useBiometric = true;

        public MobilePaymentChannel(IPaymentMethod paymentMethod) 
            : base(paymentMethod, "移动端")
        {
        }

        public override bool MakePayment(decimal amount, string accountInfo)
        {
            Console.WriteLine($"\n=== {channelName}支付渠道 ===");
            ShowPaymentInfo();
            
            // 生物识别验证
            if (useBiometric)
            {
                Console.WriteLine("进行生物识别验证...");
                Console.WriteLine("  ✓ 指纹验证成功");
            }
            
            // 验证账号
            if (!paymentMethod.ValidateAccount(accountInfo))
            {
                Console.WriteLine("✗ 账号验证失败");
                return false;
            }
            
            Console.WriteLine($"支付金额: ¥{amount:F2}");
            
            // 处理支付
            bool result = paymentMethod.ProcessPayment(amount, accountInfo);
            
            if (result)
            {
                Console.WriteLine("发送支付成功推送通知...");
                Console.WriteLine("记录支付日志...");
            }
            
            return result;
        }

        public override void ShowPaymentInfo()
        {
            Console.WriteLine($"支付方式: {paymentMethod.GetPaymentMethodName()}");
            Console.WriteLine($"渠道类型: {channelName}");
            Console.WriteLine($"生物识别: {(useBiometric ? "已启用" : "已禁用")}");
        }
    }

    // POS机支付渠道
    public class POSPaymentChannel : PaymentChannel
    {
        private string terminalId;
        private string merchantId;

        public POSPaymentChannel(IPaymentMethod paymentMethod, string terminalId, string merchantId) 
            : base(paymentMethod, "POS机")
        {
            this.terminalId = terminalId;
            this.merchantId = merchantId;
        }

        public override bool MakePayment(decimal amount, string accountInfo)
        {
            Console.WriteLine($"\n=== {channelName}支付渠道 ===");
            ShowPaymentInfo();
            
            Console.WriteLine($"终端号: {terminalId}");
            Console.WriteLine($"商户号: {merchantId}");
            
            // 验证账号
            if (!paymentMethod.ValidateAccount(accountInfo))
            {
                Console.WriteLine("✗ 账号验证失败");
                return false;
            }
            
            Console.WriteLine($"交易金额: ¥{amount:F2}");
            Console.WriteLine("打印小票...");
            
            // 处理支付
            bool result = paymentMethod.ProcessPayment(amount, accountInfo);
            
            if (result)
            {
                Console.WriteLine("交易成功，打印凭证...");
                Console.WriteLine("上传交易记录到服务器...");
            }
            
            return result;
        }

        public override void ShowPaymentInfo()
        {
            Console.WriteLine($"支付方式: {paymentMethod.GetPaymentMethodName()}");
            Console.WriteLine($"渠道类型: {channelName}");
        }
    }
}