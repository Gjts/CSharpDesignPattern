using _07Adapter._01ImplementationMethod._02OutRegister.IServices;

namespace _07Adapter._02Example.Payment.Business
{
    // 定义支付适配器工厂
    public class AlipayPaymentAdapter : IAdaptationStrategy<decimal, bool>
    {
        public bool Adapt(decimal amount)
        {
            // 在真实场景中，这里会连接支付宝支付接口，进行支付处理
            // 包括生成支付二维码、用户扫码支付、异步通知等支付流程
            Console.WriteLine($"生成支付宝支付二维码：{amount} 元");
            Console.WriteLine("请用户扫描二维码进行支付...");
            bool paymentSuccess = new Random().Next(1, 101) <= 95;// 95% 的概率模拟支付成功
            if (paymentSuccess)
            {
                Console.WriteLine($"支付宝支付 {amount} 元成功");
            }
            else
            {
                Console.WriteLine($"支付宝支付 {amount} 元失败，请稍后重试");
            }
            return paymentSuccess;
        }
    }
}
