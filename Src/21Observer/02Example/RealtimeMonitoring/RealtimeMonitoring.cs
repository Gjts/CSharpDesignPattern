namespace _21Observer.Example.RealtimeMonitoring
{
    // 监控指标类型
    public enum MetricType
    {
        CPU,
        Memory,
        Disk,
        Network,
        Database,
        API
    }

    // 监控数据
    public class MetricData
    {
        public MetricType Type { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public DateTime Timestamp { get; set; }
        public string ServerName { get; set; }
        public Dictionary<string, object> Metadata { get; set; }

        public MetricData()
        {
            Timestamp = DateTime.Now;
            Metadata = new Dictionary<string, object>();
        }
    }

    // 警报级别
    public enum AlertLevel
    {
        Info,
        Warning,
        Critical,
        Emergency
    }

    // 监控主题接口
    public interface IMonitoringSubject
    {
        void Attach(IMonitorObserver observer);
        void Detach(IMonitorObserver observer);
        void NotifyObservers(MetricData data);
    }

    // 监控观察者接口
    public interface IMonitorObserver
    {
        void Update(MetricData data);
        string ObserverName { get; }
    }

    // 服务器监控系统（主题）
    public class ServerMonitor : IMonitoringSubject
    {
        private List<IMonitorObserver> _observers = new();
        private Dictionary<MetricType, double> _thresholds = new();
        private string _serverName;
        private Random _random = new Random();

        public ServerMonitor(string serverName)
        {
            _serverName = serverName;
            SetDefaultThresholds();
        }

        private void SetDefaultThresholds()
        {
            _thresholds[MetricType.CPU] = 80;      // 80%
            _thresholds[MetricType.Memory] = 85;   // 85%
            _thresholds[MetricType.Disk] = 90;     // 90%
            _thresholds[MetricType.Network] = 1000; // 1000 Mbps
            _thresholds[MetricType.Database] = 500; // 500 connections
            _thresholds[MetricType.API] = 1000;    // 1000 ms response time
        }

        public void Attach(IMonitorObserver observer)
        {
            _observers.Add(observer);
            Console.WriteLine($"[{_serverName}] 添加观察者: {observer.ObserverName}");
        }

        public void Detach(IMonitorObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine($"[{_serverName}] 移除观察者: {observer.ObserverName}");
        }

        public void NotifyObservers(MetricData data)
        {
            foreach (var observer in _observers)
            {
                observer.Update(data);
            }
        }

        public void CollectMetric(MetricType type)
        {
            var data = new MetricData
            {
                Type = type,
                ServerName = _serverName
            };

            // 模拟数据收集
            switch (type)
            {
                case MetricType.CPU:
                    data.Value = 30 + _random.NextDouble() * 70;
                    data.Unit = "%";
                    break;
                case MetricType.Memory:
                    data.Value = 40 + _random.NextDouble() * 55;
                    data.Unit = "%";
                    break;
                case MetricType.Disk:
                    data.Value = 50 + _random.NextDouble() * 45;
                    data.Unit = "%";
                    break;
                case MetricType.Network:
                    data.Value = _random.NextDouble() * 1500;
                    data.Unit = "Mbps";
                    break;
                case MetricType.Database:
                    data.Value = _random.Next(100, 600);
                    data.Unit = "connections";
                    break;
                case MetricType.API:
                    data.Value = 50 + _random.NextDouble() * 1500;
                    data.Unit = "ms";
                    break;
            }

            // 添加元数据
            if (data.Value > _thresholds[type])
            {
                data.Metadata["threshold_exceeded"] = true;
                data.Metadata["threshold"] = _thresholds[type];
            }

            Console.WriteLine($"\n[{_serverName}] 收集指标 {type}: {data.Value:F2} {data.Unit}");
            NotifyObservers(data);
        }

        public void SimulateLoad()
        {
            Console.WriteLine($"\n模拟 {_serverName} 负载...");
            CollectMetric(MetricType.CPU);
            CollectMetric(MetricType.Memory);
            CollectMetric(MetricType.Network);
            CollectMetric(MetricType.API);
        }
    }

    // 警报系统（观察者）
    public class AlertSystem : IMonitorObserver
    {
        public string ObserverName => "警报系统";
        private Dictionary<MetricType, AlertLevel> _alertLevels = new();

        public AlertSystem()
        {
            ConfigureAlertLevels();
        }

        private void ConfigureAlertLevels()
        {
            _alertLevels[MetricType.CPU] = AlertLevel.Warning;
            _alertLevels[MetricType.Memory] = AlertLevel.Warning;
            _alertLevels[MetricType.Disk] = AlertLevel.Critical;
            _alertLevels[MetricType.Network] = AlertLevel.Info;
            _alertLevels[MetricType.Database] = AlertLevel.Critical;
            _alertLevels[MetricType.API] = AlertLevel.Warning;
        }

        public void Update(MetricData data)
        {
            if (data.Metadata.ContainsKey("threshold_exceeded"))
            {
                var level = _alertLevels[data.Type];
                var icon = GetAlertIcon(level);
                
                Console.WriteLine($"  {icon} [{ObserverName}] {level} 警报: {data.ServerName} - {data.Type} = {data.Value:F2}{data.Unit}");
                
                if (level >= AlertLevel.Critical)
                {
                    SendEmergencyNotification(data);
                }
            }
        }

        private string GetAlertIcon(AlertLevel level)
        {
            return level switch
            {
                AlertLevel.Info => "ℹ️",
                AlertLevel.Warning => "⚠️",
                AlertLevel.Critical => "🔴",
                AlertLevel.Emergency => "🚨",
                _ => "📊"
            };
        }

        private void SendEmergencyNotification(MetricData data)
        {
            Console.WriteLine($"    📱 发送紧急通知到运维团队: {data.Type} 超过阈值!");
        }
    }

    // 日志记录器（观察者）
    public class MetricLogger : IMonitorObserver
    {
        public string ObserverName => "日志记录器";
        private string _logFile;
        private List<string> _logs = new();

        public MetricLogger(string logFile)
        {
            _logFile = logFile;
        }

        public void Update(MetricData data)
        {
            var logEntry = $"{data.Timestamp:yyyy-MM-dd HH:mm:ss} | {data.ServerName} | {data.Type} | {data.Value:F2} {data.Unit}";
            _logs.Add(logEntry);
            Console.WriteLine($"  📝 [{ObserverName}] 记录: {logEntry}");
        }

        public void SaveLogs()
        {
            Console.WriteLine($"\n[{ObserverName}] 保存日志到 {_logFile}");
            Console.WriteLine($"  共 {_logs.Count} 条记录");
        }
    }

    // 仪表板（观察者）
    public class Dashboard : IMonitorObserver
    {
        public string ObserverName => "实时仪表板";
        private Dictionary<string, Dictionary<MetricType, double>> _currentMetrics = new();

        public void Update(MetricData data)
        {
            if (!_currentMetrics.ContainsKey(data.ServerName))
            {
                _currentMetrics[data.ServerName] = new Dictionary<MetricType, double>();
            }

            _currentMetrics[data.ServerName][data.Type] = data.Value;
            
            Console.WriteLine($"  📊 [{ObserverName}] 更新显示: {data.ServerName}.{data.Type} = {data.Value:F2}{data.Unit}");
            
            // 如果是关键指标，更新图表
            if (data.Type == MetricType.CPU || data.Type == MetricType.Memory)
            {
                UpdateChart(data);
            }
        }

        private void UpdateChart(MetricData data)
        {
            var bars = GetProgressBar(data.Value, 100);
            Console.WriteLine($"      {data.Type}: {bars} {data.Value:F1}%");
        }

        private string GetProgressBar(double value, double max)
        {
            int filled = (int)(value / max * 20);
            return "[" + new string('█', filled) + new string('░', 20 - filled) + "]";
        }

        public void ShowSummary()
        {
            Console.WriteLine($"\n[{ObserverName}] 系统概览:");
            foreach (var server in _currentMetrics)
            {
                Console.WriteLine($"  服务器: {server.Key}");
                foreach (var metric in server.Value)
                {
                    Console.WriteLine($"    {metric.Key}: {metric.Value:F2}");
                }
            }
        }
    }

    // 自动扩容系统（观察者）
    public class AutoScaler : IMonitorObserver
    {
        public string ObserverName => "自动扩容系统";
        private int _instanceCount = 1;
        private Dictionary<string, int> _scalingHistory = new();

        public void Update(MetricData data)
        {
            // 只关注CPU和内存
            if (data.Type == MetricType.CPU || data.Type == MetricType.Memory)
            {
                if (data.Value > 80)
                {
                    ScaleUp(data.ServerName);
                }
                else if (data.Value < 30 && _instanceCount > 1)
                {
                    ScaleDown(data.ServerName);
                }
            }
        }

        private void ScaleUp(string serverName)
        {
            _instanceCount++;
            Console.WriteLine($"  ⬆️ [{ObserverName}] 扩容: {serverName} 实例数增加到 {_instanceCount}");
            
            if (!_scalingHistory.ContainsKey(serverName))
            {
                _scalingHistory[serverName] = 0;
            }
            _scalingHistory[serverName]++;
        }

        private void ScaleDown(string serverName)
        {
            _instanceCount--;
            Console.WriteLine($"  ⬇️ [{ObserverName}] 缩容: {serverName} 实例数减少到 {_instanceCount}");
        }
    }
}

