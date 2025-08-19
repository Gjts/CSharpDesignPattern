using _08Bridge._01ImplementationMethod;
using _08Bridge._02Example;

namespace _08Bridge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 桥接模式 Bridge Pattern ===\n");

            // 基础实现
            Console.WriteLine("1. 基础实现：");
            IImplementor implementorA = new ConcreteImplementorA();
            Abstraction abstraction = new RefinedAbstraction(implementorA);
            abstraction.Operation();

            IImplementor implementorB = new ConcreteImplementorB();
            abstraction = new RefinedAbstraction(implementorB);
            abstraction.Operation();

            Console.WriteLine("\n2. 支付系统示例：");
            Console.WriteLine("演示不同支付渠道与支付方式的组合");
            Console.WriteLine(new string('-', 60));
            
            // 电商平台 + 支付宝
            IPaymentMethod alipay = new AlipayMethod();
            PaymentChannel ecommerce = new ECommercePaymentChannel(alipay);
            ecommerce.MakePayment(1288.00m, "user@example.com");
            
            // 移动端 + 微信支付
            IPaymentMethod wechat = new WeChatPayMethod();
            PaymentChannel mobile = new MobilePaymentChannel(wechat);
            mobile.MakePayment(68.50m, "wx_user_12345");
            
            // POS机 + 银行卡
            IPaymentMethod bankCard = new BankCardMethod();
            PaymentChannel pos = new POSPaymentChannel(bankCard, "POS001", "MERCHANT888");
            pos.MakePayment(520.00m, "6222021234567890123");
            
            // 电商平台 + PayPal（国际支付）
            IPaymentMethod paypal = new PayPalMethod();
            PaymentChannel international = new ECommercePaymentChannel(paypal);
            international.MakePayment(99.99m, "international@buyer.com");
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("桥接模式优势：支付渠道和支付方式可以独立变化和扩展");
        }
    }
}