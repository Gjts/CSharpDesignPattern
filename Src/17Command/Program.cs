using _17Command.Example.WMSOperations;

namespace _17Command
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 命令模式 (Command Pattern) ===");
            Console.WriteLine("将请求封装成对象，支持撤销、重做、队列等操作\n");

            // 运行WMS操作示例
            WMSOperationsExample.Run();
        }
    }
}
