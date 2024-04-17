using _07Adapter._01ImplementationMethod._02OutRegister.IServices;

namespace _07Adapter._02Example.Payment.Business
{
    public class LogisticsShippingAdapter : IAdaptationStrategy<string, string>
    {
        public string Adapt(string address)
        {
            // 在真实场景中，这里会调用物流公司接口，进行订单发货处理
            // 生成订单、打包商品、委派快递派送等流程
            Console.WriteLine($"收到发货地址：{address}");
            Console.WriteLine("正在处理订单，预计2个工作日内发货");
            return "发货成功";
        }
    }
}
