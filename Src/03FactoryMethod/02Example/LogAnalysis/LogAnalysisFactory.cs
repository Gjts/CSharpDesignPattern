namespace _FactoryMethod._02Example.LogAnalysis
{
    // 抽象工厂类
    public abstract class LogAnalysisFactory
    {
        public abstract LogAnalysis CreateLogAnalysis();
    }

    // 具体工厂类：访问量统计
    public class AccessCountAnalysisFactory : LogAnalysisFactory
    {
        public override LogAnalysis CreateLogAnalysis()
        {
            return new AccessCountAnalysis();
        }
    }

    // 具体工厂类：用户行为分析
    public class UserBehaviorAnalysisFactory : LogAnalysisFactory
    {
        public override LogAnalysis CreateLogAnalysis()
        {
            return new UserBehaviorAnalysis();
        }
    }

    // 具体工厂类：异常检测
    public class ExceptionDetectionAnalysisFactory : LogAnalysisFactory
    {
        public override LogAnalysis CreateLogAnalysis()
        {
            return new ExceptionDetectionAnalysis();
        }
    }
}
