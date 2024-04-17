using _07Adapter._01ImplementationMethod._02OutRegister.IServices;

namespace _07Adapter._02Example.Payment.Business
{
    // 运货处理适配器
    public class ShippingAdapter : IAdaptationStrategy<string, string>
    {
        public string Adapt(string address)
        {
            // 在真实场景中，这里会连接物流公司接口，进行订单发货处理
            // 包括订单处理、揽收包裹、运输配送、签收等一系列物流流程
            Console.WriteLine($"收到物流发货地址：{address}");
            Console.WriteLine("正在处理订单，预计3个工作日内发货");
            return "物流发货成功";
        }
    }
}
