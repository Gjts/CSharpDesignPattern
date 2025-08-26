using _16Respon.Example.Web3Validation;

namespace _16Respon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 责任链模式 (Chain of Responsibility Pattern) ===");
            Console.WriteLine("将请求沿着处理链传递，直到有对象处理它\n");

            // 运行Web3验证示例
            Web3ValidationExample.Run();
        }
    }
}
