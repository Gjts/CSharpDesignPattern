using _14Interpreter.Example.WMSRuleEngine;

namespace _14Interpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 解释器模式 (Interpreter Pattern) ===");
            Console.WriteLine("用于构建语言解释器，定义语法规则并解释执行\n");

            // 运行WMS规则引擎示例
            WMSRuleEngineExample.Run();
        }
    }
}
