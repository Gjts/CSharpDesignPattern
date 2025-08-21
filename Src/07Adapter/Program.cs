using _07Adapter._01ImplementationMethod._02OutRegister.IServices;
using _07Adapter._01ImplementationMethod._02OutRegister.Services;
using _07Adapter._02Example.Payment.Business;

namespace _07Adapter
{
    internal class Program
    {
        public enum PaymentType
        {
            Alipay,
            WeChatPay,
            CreditCard,
            BankTransfer,
        }

        public enum ShippingType
        {
            Shipping,
            LogisticsShipping,
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1.创建一个适配器工厂");
            // 创建一个适配器工厂
            IAdapterFactory factory = new AdapterFactory();

            Console.WriteLine("\n2.注册支付宝支付适配器");
            // 注册支付宝支付适配器
            IAdaptationStrategy<decimal, bool> alipayPaymentStrategy = new AlipayPaymentAdapter();
            factory.RegisterStrategy<decimal, bool>(alipayPaymentStrategy);

            Console.WriteLine("\n3.注册信用卡支付适配器");
            // 注册信用卡支付适配器
            IAdaptationStrategy<double, bool> creditCardStrategy = new CreditCardPaymentAdapter();
            factory.RegisterStrategy<double, bool>(creditCardStrategy);

            Console.WriteLine("\n4.注册普通快递适配器");
            // 注册普通快递适配器
            IAdaptationStrategy<string, string> shippingStrategy = new ShippingAdapter();
            factory.RegisterStrategy<string, string, ShippingType>(shippingStrategy);

            Console.WriteLine("\n5.注册物流适配器");
            // 注册物流适配器
            IAdaptationStrategy<string, string> logisticsShippingStrategy = new LogisticsShippingAdapter();
            factory.RegisterStrategy<string, string>(logisticsShippingStrategy);

            // 使用工厂创建适配器并执行支付操作
            // 示例使用：支付宝支付
            Console.WriteLine("------ 支付宝支付示例 ------");
            IAdapter<decimal, bool>? aliAdapter = factory.CreateAdapter<decimal, bool>();
            if (aliAdapter != null)
            {
                // 模拟支付 1000 元
                bool aliPaymentResult = aliAdapter.Adapt(1000.00m);
                Console.WriteLine("支付宝支付结果：" + (aliPaymentResult ? "成功" : "失败"));
            }
            else
            {
                Console.WriteLine("支付宝适配器创建失败");
            }

            // 示例使用：信用卡支付
            Console.WriteLine("\n------ 信用卡支付示例 ------");
            IAdapter<double, bool>? cardAdapter = factory.CreateAdapter<double, bool>();
            if (cardAdapter != null)
            {
                // 模拟支付 800.50 元
                bool cardPaymentResult = cardAdapter.Adapt(800.50);
                Console.WriteLine("信用卡支付结果：" + (cardPaymentResult ? "成功" : "失败"));
            }
            else
            {
                Console.WriteLine("信用卡适配器创建失败");
            }

            // 使用工厂创建适配器并执行运货操作
            // 示例使用：普通运货
            Console.WriteLine("\n------ 普通运货示例 ------");
            IAdapter<string, string>? shippingAdapter = factory.CreateAdapter<string, string, ShippingType>(ShippingType.Shipping);
            if (shippingAdapter != null)
            {
                string shippingResult = shippingAdapter.Adapt("14号楼, 深圳");// 运货适配器执行运货操作
                Console.WriteLine("订单发货结果:" + shippingResult);
            }
            else
            {
                Console.WriteLine("普通运货适配器创建失败");
            }

            Console.WriteLine("\n------ 物流运货示例 ------");
            IAdapter<string, string>? logisticsShippingAdapter = factory.CreateAdapter<string, string, ShippingType>(ShippingType.LogisticsShipping);
            if (logisticsShippingAdapter != null)
            {
                string logisticsResult = logisticsShippingAdapter.Adapt("尖沙咀, 上海");// 运货适配器执行运货操作
                Console.WriteLine("物流订单发货结果:" + logisticsResult);
            }
            else
            {
                Console.WriteLine("物流运货适配器创建失败");
            }
            Console.ReadLine();
        }
    }
}