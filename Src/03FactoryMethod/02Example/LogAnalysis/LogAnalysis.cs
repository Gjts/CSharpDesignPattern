namespace _FactoryMethod._02Example.LogAnalysis
{
    // 抽象日志分析类
    public abstract class LogAnalysis
    {
        public abstract void Analyze();
    }

    // 具体日志分析类：访问量统计
    public class AccessCountAnalysis : LogAnalysis
    {
        public override void Analyze()
        {
            Console.WriteLine("访问量统计");
        }
    }

    // 具体日志分析类：用户行为分析
    public class UserBehaviorAnalysis : LogAnalysis
    {
        public override void Analyze()
        {
            Console.WriteLine("用户行为分析");
        }
    }

    // 具体日志分析类：异常检测
    public class ExceptionDetectionAnalysis : LogAnalysis
    {
        public override void Analyze()
        {
            Console.WriteLine("异常检测");
        }
    }
}
