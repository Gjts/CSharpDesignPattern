namespace _21Observer.Example.RealtimeMonitoring
{
    public class MonitoringExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== 实时监控系统示例 ===");
            
            // 创建服务器监控
            var webServer = new ServerMonitor("Web-Server-01");
            var apiServer = new ServerMonitor("API-Server-01");
            
            // 创建观察者
            var alertSystem = new AlertSystem();
            var logger = new MetricLogger("metrics.log");
            var dashboard = new Dashboard();
            var autoScaler = new AutoScaler();
            
            // 注册观察者
            webServer.Attach(alertSystem);
            webServer.Attach(logger);
            webServer.Attach(dashboard);
            webServer.Attach(autoScaler);
            
            apiServer.Attach(alertSystem);
            apiServer.Attach(logger);
            apiServer.Attach(dashboard);
            
            // 模拟监控
            Console.WriteLine("\n开始监控:");
            Console.WriteLine("------------------------");
            
            // 第一轮监控
            webServer.SimulateLoad();
            apiServer.SimulateLoad();
            
            // 等待一下
            Thread.Sleep(1000);
            
            // 第二轮监控
            Console.WriteLine("\n第二轮监控:");
            webServer.CollectMetric(MetricType.Database);
            apiServer.CollectMetric(MetricType.Disk);
            
            // 显示仪表板汇总
            dashboard.ShowSummary();
            
            // 保存日志
            logger.SaveLogs();
            
            // 移除一个观察者
            Console.WriteLine("\n移除自动扩容系统:");
            webServer.Detach(autoScaler);
            
            // 再次监控
            Console.WriteLine("\n第三轮监控（无自动扩容）:");
            webServer.CollectMetric(MetricType.CPU);
        }
    }
}

