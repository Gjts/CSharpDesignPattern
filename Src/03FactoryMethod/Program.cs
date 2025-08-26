using _FactoryMethod._02Example.LogAnalysis;
using _FactoryMethod._02Example.Order;

namespace _03FactoryMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 工厂方法模式 (Factory Method Pattern) ================================");
            Console.WriteLine("适用场景：当需要创建对象的逻辑较复杂，且希望将对象创建延迟到子类时");
            Console.WriteLine("特点：定义创建对象的接口，让子类决定实例化哪个类，使类的实例化延迟到子类");
            Console.WriteLine("优点：符合开闭原则，新增产品只需新增工厂类；符合单一职责原则，每个工厂只负责创建一种产品\n");

            Console.WriteLine("-------------------------------- 网站日志分析系统 ----------------------------------");
            
            // 使用不同的分析工厂
            LogAnalysisFactory factory;
            LogAnalysis logAnalysis;
            
            // 访问量统计分析
            factory = new AccessCountAnalysisFactory();
            logAnalysis = factory.CreateLogAnalysis();
            Console.Write("访问量分析: ");
            logAnalysis.Analyze();

            // 用户行为分析
            factory = new UserBehaviorAnalysisFactory();
            logAnalysis = factory.CreateLogAnalysis();
            Console.Write("用户行为分析: ");
            logAnalysis.Analyze();

            // 异常检测分析
            factory = new ExceptionDetectionAnalysisFactory();
            logAnalysis = factory.CreateLogAnalysis();
            Console.Write("异常检测分析: ");
            logAnalysis.Analyze();

            Console.WriteLine("\n-------------------------------- 电商订单处理系统 ----------------------------------");
            
            OrderFactory orderFactory;
            Order order;
            
            // 普通订单
            orderFactory = new NormalOrderFactory();
            order = orderFactory.CreateOrder();
            Console.Write("普通订单: ");
            order.Process();

            // 预售订单
            orderFactory = new PreSaleOrderFactory();
            order = orderFactory.CreateOrder();
            Console.Write("预售订单: ");
            order.Process();

            // 定制订单
            orderFactory = new CustomOrderFactory();
            order = orderFactory.CreateOrder();
            Console.Write("定制订单: ");
            order.Process();
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 每个具体工厂负责创建一种产品，职责单一");
            Console.WriteLine("- 新增产品类型只需新增对应的工厂类，无需修改现有代码");
            Console.WriteLine("- 适用于产品种类经常变化，需要灵活扩展的场景");
        }
    }
}
