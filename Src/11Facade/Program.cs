namespace _11Facade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("=== 外观模式 Facade Pattern ===\n");

            // 1. 基础实现
            System.Console.WriteLine("1. 基础实现：");
            var facade = new _11Facade._01ImplementationMethod.Facade();
            facade.SimpleOperation1();
            facade.SimpleOperation2();
            facade.ComplexOperation();

            // 2. 电商订单示例
            System.Console.WriteLine("\n2. 电商订单示例：");
            System.Console.WriteLine(new string('-', 60));
            var orderFacade = new _11Facade._02Example._01ECommerceOrder.ECommerceOrderFacade();

            var order = new _11Facade._02Example._01ECommerceOrder.OrderInfo
            {
                CustomerId = "CUST10001",
                CustomerEmail = "user@example.com",
                CustomerPhone = "+86-13800000000",
                ShippingAddress = "深圳市南山区科技园",
                CouponCode = "SAVE20",
                PaymentMethod = "Alipay"
            };
            order.Items = new System.Collections.Generic.List<_11Facade._02Example._01ECommerceOrder.OrderItem>
            {
                new _11Facade._02Example._01ECommerceOrder.OrderItem { SkuId = "SKU001", ProductName = "旗舰手机", Quantity = 1, UnitPrice = 5999m },
                new _11Facade._02Example._01ECommerceOrder.OrderItem { SkuId = "SKU003", ProductName = "蓝牙耳机", Quantity = 2, UnitPrice = 199m }
            };

            bool orderResult = orderFacade.PlaceOrder(order);
            System.Console.WriteLine($"\n订单处理结果: {(orderResult ? "成功" : "失败")}, 状态: {order.Status}");

            // 3. 视频转换示例
            System.Console.WriteLine("\n3. 视频转换示例：");
            System.Console.WriteLine(new string('-', 60));
            var converter = new _11Facade._02Example._02VideoConverter.VideoConverterFacade();

            var video = new _11Facade._02Example._02VideoConverter.VideoFile("sample.mov", "MOV", 120, 500_000_000);
            converter.ConvertToMP4(video, "output/sample.mp4");

            var config = new _11Facade._02Example._02VideoConverter.ConversionConfig
            {
                OutputFormat = "MP4",
                Quality = "高清",
                Resolution = "1920x1080",
                FrameRate = 60,
                IncludeSubtitles = true,
                SubtitleFile = "captions.srt",
                ApplyFilters = true,
                Watermark = "© Demo"
            };
            converter.ConvertWithConfig(video, config, "output/sample_hd.mp4");

            // 批量转换示例（可选）
            var videos = new System.Collections.Generic.List<_11Facade._02Example._02VideoConverter.VideoFile>
            {
                new _11Facade._02Example._02VideoConverter.VideoFile("holiday1.mp4", "MP4", 90, 300_000_000),
                new _11Facade._02Example._02VideoConverter.VideoFile("holiday2.avi", "AVI", 150, 700_000_000)
            };
            converter.BatchConvert(videos, "MP4", "output");

            System.Console.WriteLine("\n" + new string('=', 60));
            System.Console.WriteLine("演示结束");
            System.Console.ReadLine();
        }
    }
}