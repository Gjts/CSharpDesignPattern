using _18Iterator.Example.BlockchainIterator;

namespace _18Iterator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 迭代器模式 (Iterator Pattern) ===");
            Console.WriteLine("提供顺序访问聚合对象元素的方法，不暴露内部结构\n");

            // 运行区块链迭代器示例
            BlockchainIteratorExample.Run();
        }
    }
}
