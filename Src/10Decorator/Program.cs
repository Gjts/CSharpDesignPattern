using _Decorator._02Example.Coffee;
using _Decorator._02Example.DataStream;

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
            Beverage espresso = new Espresso();
            Console.WriteLine($"   {espresso.GetDescription()} - ¥{espresso.GetCost():F2}");
            
            // 加牛奶
            Console.WriteLine("\n2. 拿铁（浓缩+牛奶）：");
            Beverage latte = new Espresso();
            latte = new Milk(latte);
            Console.WriteLine($"   {latte.GetDescription()} - ¥{latte.GetCost():F2}");
            
            // 摩卡（多重装饰）
            Console.WriteLine("\n3. 摩卡（浓缩+摩卡+牛奶+奶泡）：");
            Beverage mocha = new Espresso();
            mocha = new Mocha(mocha);
            mocha = new Milk(mocha);
            mocha = new Whip(mocha);
            Console.WriteLine($"   {mocha.GetDescription()} - ¥{mocha.GetCost():F2}");
            
            // 混合咖啡
            Console.WriteLine("\n4. 大杯混合咖啡（混合咖啡+豆浆）：");
            Beverage houseBlend = new HouseBlend();
            houseBlend = new Soy(houseBlend);
            Console.WriteLine($"   {houseBlend.GetDescription()} - ¥{houseBlend.GetCost():F2}");

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
