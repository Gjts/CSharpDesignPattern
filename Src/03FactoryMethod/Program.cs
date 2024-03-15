using _FactoryMethod._02Example.LogAnalysis;
using _FactoryMethod._02Example.Order;

namespace _03FactoryMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------- 网站访问日志分析 ----------------------------------");
            LogAnalysisFactory factory = new AccessCountAnalysisFactory();
            LogAnalysis logAnalysis = factory.CreateLogAnalysis();
            logAnalysis.Analyze();

            factory = new UserBehaviorAnalysisFactory();
            logAnalysis = factory.CreateLogAnalysis();
            logAnalysis.Analyze();

            factory = new ExceptionDetectionAnalysisFactory();
            logAnalysis = factory.CreateLogAnalysis();
            logAnalysis.Analyze();

            Console.WriteLine("-------------------------------- 订单处理 ----------------------------------");
            OrderFactory factoryOrder = new NormalOrderFactory();
            Order order = factoryOrder.CreateOrder();
            order.Process();

            factoryOrder = new PreSaleOrderFactory();
            order = factoryOrder.CreateOrder();
            order.Process();

            factoryOrder = new CustomOrderFactory();
            order = factoryOrder.CreateOrder();
            order.Process();
        }
    }
}