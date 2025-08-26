using _10Decorator._02Example;

namespace _10Decorator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 装饰器模式 (Decorator Pattern) ================================");
            Console.WriteLine("适用场景：需要动态地给对象添加功能；需要为对象添加的功能可以动态撤销");
            Console.WriteLine("特点：动态地给对象添加一些额外的职责，比生成子类更加灵活");
            Console.WriteLine("优点：装饰类和被装饰类可以独立发展；动态扩展功能；可以多次装饰\n");

            Console.WriteLine("-------------------------------- 咖啡店订单系统 ----------------------------------");
            
            // 基础咖啡
            Console.WriteLine("1. 基础浓缩咖啡：");
            ICoffee espresso = new Espresso();
            Console.WriteLine($"   {espresso.GetDescription()} - ¥{espresso.GetCost():F2}");
            
            // 加牛奶
            Console.WriteLine("\n2. 拿铁（浓缩+牛奶）：");
            ICoffee latte = new Espresso();
            latte = new MilkDecorator(latte);
            Console.WriteLine($"   {latte.GetDescription()} - ¥{latte.GetCost():F2}");
            
            // 摩卡（多重装饰）
            Console.WriteLine("\n3. 摩卡（浓缩+巧克力+牛奶+奶泡）：");
            ICoffee mocha = new Espresso();
            mocha = new ChocolateDecorator(mocha);
            mocha = new MilkDecorator(mocha);
            mocha = new WhipCreamDecorator(mocha);
            Console.WriteLine($"   {mocha.GetDescription()} - ¥{mocha.GetCost():F2}");
            
            // 美式咖啡
            Console.WriteLine("\n4. 大杯美式（美式+加大）：");
            ICoffee americano = new Americano();
            americano = new SizeDecorator(americano, "大杯");
            Console.WriteLine($"   {americano.GetDescription()} - ¥{americano.GetCost():F2}");

            Console.WriteLine("\n-------------------------------- 数据流处理系统 ----------------------------------");
            
            // 基础数据流
            Console.WriteLine("1. 原始数据流：");
            IDataStream stream = new FileDataStream("data.txt");
            stream.Write("Hello World!");
            var data = stream.Read();
            Console.WriteLine($"   读取: {data}");
            
            // 加密数据流
            Console.WriteLine("\n2. 加密数据流：");
            stream = new FileDataStream("encrypted.txt");
            stream = new EncryptionDecorator(stream);
            stream.Write("Secret Message");
            data = stream.Read();
            Console.WriteLine($"   读取: {data}");
            
            // 压缩+加密数据流
            Console.WriteLine("\n3. 压缩加密数据流：");
            stream = new FileDataStream("compressed.txt");
            stream = new CompressionDecorator(stream);
            stream = new EncryptionDecorator(stream);
            stream.Write("Large Secret Data");
            data = stream.Read();
            Console.WriteLine($"   读取: {data}");
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 装饰器和被装饰对象有相同的接口");
            Console.WriteLine("- 可以用多个装饰器包装对象");
            Console.WriteLine("- 装饰器可以在被装饰对象的行为前后加上自己的行为");
        }
    }
}
