using _07Adapter._01ImplementationMethod._02OutRegister.IServices;

namespace _07Adapter._02Example.Payment.Business
{
    // 定义支付适配器工厂
    public class CreditCardPaymentAdapter : IAdaptationStrategy<double, bool>
    {
        public bool Adapt(double amount)
        {
            // 在真实场景中，这里会连接信用卡支付接口，进行支付处理
            // 处理过程中会包含验证信用卡信息、授权和支付等流程
            // 以当前的时间作为交易号，用于标识每笔交易
            string transactionId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            Console.WriteLine($"发起信用卡支付：{amount} 元，交易号：{transactionId}");
            Console.WriteLine("验证信用卡信息...");
            Console.WriteLine("信用卡授权中...");
            bool paymentSuccess = new Random().Next(1, 101) <= 90;// 90% 的概率模拟支付成功
            if (paymentSuccess)
            {
                Console.WriteLine($"信用卡支付 {amount} 元成功，交易号：{transactionId}");
            }
            else
            {
                Console.WriteLine($"信用卡支付 {amount} 元失败，请检查支付信息");
            }
            return paymentSuccess;
        }
    }
}
