using _07Adapter._02Example.Payment.Business;
using _Adapter._02Example.DataFormat;

namespace _07Adapter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 适配器模式 (Adapter Pattern) ================================");
            Console.WriteLine("适用场景：当需要使用现有类，但其接口不符合需求时；需要整合第三方库或遗留系统时");
            Console.WriteLine("特点：将一个类的接口转换成客户期望的另一个接口，使原本接口不兼容的类可以一起工作");
            Console.WriteLine("优点：提高类的复用性；解耦目标类和适配者类；符合开闭原则\n");

            Console.WriteLine("-------------------------------- 支付系统集成 ----------------------------------");
            
            // 统一的支付接口
            IPaymentProcessor processor;
            
            Console.WriteLine("1. PayPal支付（需要适配）：");
            var paypalService = new PayPalService();
            processor = new PayPalAdapter(paypalService);
            processor.ProcessPayment(100.00m, "USD", "user@example.com");
            
            Console.WriteLine("\n2. Stripe支付（需要适配）：");
            var stripeService = new StripeService();
            processor = new StripeAdapter(stripeService);
            processor.ProcessPayment(200.00m, "EUR", "customer123");
            
            Console.WriteLine("\n3. 本地支付（原生接口）：");
            processor = new LocalPaymentProcessor();
            processor.ProcessPayment(300.00m, "CNY", "local_user");

            Console.WriteLine("\n-------------------------------- 数据格式转换 ----------------------------------");
            
            IDataParser parser;
            
            Console.WriteLine("1. XML数据解析（适配器）：");
            var xmlReader = new XmlDataReader();
            parser = new XmlDataAdapter(xmlReader);
            var xmlData = parser.ParseData("<user><name>张三</name></user>");
            Console.WriteLine($"   解析结果: {xmlData}");
            
            Console.WriteLine("\n2. CSV数据解析（适配器）：");
            var csvReader = new CsvDataReader();
            parser = new CsvDataAdapter(csvReader);
            var csvData = parser.ParseData("name,age,city\n李四,25,北京");
            Console.WriteLine($"   解析结果: {csvData}");
            
            Console.WriteLine("\n3. JSON数据解析（原生）：");
            parser = new JsonDataParser();
            var jsonData = parser.ParseData("{\"name\":\"王五\",\"age\":30}");
            Console.WriteLine($"   解析结果: {jsonData}");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 适配器使不兼容的接口能够协同工作");
            Console.WriteLine("- 可以选择对象适配器或类适配器实现方式");
            Console.WriteLine("- 常用于系统集成和第三方库的整合");
        }
    }
}
