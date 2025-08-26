using _15Template.Example.AITraining;

namespace _15Template
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 模板方法模式 (Template Method Pattern) ===");
            Console.WriteLine("定义算法骨架，将某些步骤延迟到子类实现\n");

            // 运行AI训练示例
            AITrainingExample.Run();
        }
    }
}
