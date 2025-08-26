using _21Observer.Example.RealtimeMonitoring;

namespace _21Observer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================ 观察者模式 (Observer Pattern) ================================");
            Console.WriteLine("适用场景：当一个对象的状态变化需要通知多个依赖对象时，如事件处理、模型-视图架构、消息订阅等");
            Console.WriteLine("特点：定义对象间的一对多依赖关系，当一个对象状态改变时，所有依赖者都会收到通知并自动更新");
            Console.WriteLine("优点：实现了观察者和主题的松耦合，支持广播通信，符合开闭原则\n");

            Console.WriteLine("-------------------------------- 实时服务器监控系统 ----------------------------------");
            
            // 创建服务器监控器（主题）
            var webServer = new ServerMonitor("Web-Server-01");
            var apiServer = new ServerMonitor("API-Server-01");
            
            // 创建各种观察者
            var alertSystem = new AlertSystem();
            var logger = new MetricLogger("metrics.log");
            var dashboard = new Dashboard();
            var autoScaler = new AutoScaler();
            
            Console.WriteLine("1. 注册监控观察者：");
            Console.WriteLine("--------------------------------");
            
            // Web服务器注册所有观察者
            webServer.Attach(alertSystem);
            webServer.Attach(logger);
            webServer.Attach(dashboard);
            webServer.Attach(autoScaler);
            
            // API服务器注册部分观察者
            apiServer.Attach(alertSystem);
            apiServer.Attach(logger);
            apiServer.Attach(dashboard);
            
            Console.WriteLine("\n2. 第一轮监控（收集指标）：");
            Console.WriteLine("--------------------------------");
            webServer.SimulateLoad();
            
            Console.WriteLine("\n3. 第二轮监控（API服务器）：");
            Console.WriteLine("--------------------------------");
            apiServer.SimulateLoad();
            
            // 模拟特定指标监控
            Console.WriteLine("\n4. 监控特定指标：");
            Console.WriteLine("--------------------------------");
            webServer.CollectMetric(MetricType.Database);
            apiServer.CollectMetric(MetricType.Disk);
            
            // 显示仪表板汇总
            Console.WriteLine("\n5. 仪表板数据汇总：");
            Console.WriteLine("--------------------------------");
            dashboard.ShowSummary();
            
            // 保存日志
            logger.SaveLogs();
            
            // 演示动态移除观察者
            Console.WriteLine("\n6. 动态调整观察者：");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("移除自动扩容系统...");
            webServer.Detach(autoScaler);
            
            Console.WriteLine("\n再次监控（无自动扩容）：");
            webServer.CollectMetric(MetricType.CPU);
            
            Console.WriteLine("\n说明：");
            Console.WriteLine("- 服务器监控器是主题，各种系统（警报、日志、仪表板等）是观察者");
            Console.WriteLine("- 当服务器指标变化时，所有注册的观察者都会收到通知");
            Console.WriteLine("- 观察者可以动态添加或移除，不影响主题和其他观察者");
            Console.WriteLine("- 每个观察者根据自己的职责处理通知（警报、记录、显示、扩容等）");
        }
    }
}
